using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LM;

namespace KIM
{
    public class AttackableFish : Fish, IAttackable
    {
        
        public enum State { Idle = 0, Move, Hit, Attack, Die }
        StateMachine<State, AttackableFish> stateMachine;

        Coroutine moveDirRoutine;
        SphereCollider recogRangeTrigger;

        GameObject player;
        Diver diver;

        bool isAttackable = false;

        protected override void Awake()
        {
            recogRangeTrigger = GetComponent<SphereCollider>();
            recogRangeTrigger.radius = data.PlayerRecognitionRange;
            base.Awake();
            moveDirRoutine = StartCoroutine(MoveDirRoutine());

            stateMachine = new StateMachine<State, AttackableFish>(this);
            stateMachine.AddState(State.Idle,   new IdleState(this, stateMachine));
            stateMachine.AddState(State.Move,   new MoveState(this, stateMachine));
            stateMachine.AddState(State.Hit,    new HitState(this, stateMachine));
            stateMachine.AddState(State.Attack, new AttackState(this, stateMachine));
            stateMachine.AddState(State.Die,    new DieState(this, stateMachine));
        }
        private void Start()
        {
            player = GameObject.Find("XR Origin (XR Rig)");
            diver = player.GetComponentInChildren<Diver>();
            stateMachine.SetUp(State.Idle);
        }
        private void Update()
        {
            stateMachine.Update();
            transform.rotation = Quaternion.LookRotation(moveDir);
        }

        #region FishState
        private abstract class AttackableFishState : StateBase<State, AttackableFish>
        {
            protected GameObject gameObject => owner.gameObject;
            protected Transform transform => owner.transform;
            protected FishData data => owner.data;
            protected int curHp => owner.curHp;
            protected float curLength => owner.curLength;
            protected float curWeight => owner.curWeight;
            protected Vector3 moveDir => owner.moveDir;

            protected AttackableFishState(AttackableFish owner, StateMachine<State, AttackableFish> stateMachine) : base(owner, stateMachine)
            {
                
            }
        }

        private class IdleState : AttackableFishState
        {
            public IdleState(AttackableFish owner, StateMachine<State, AttackableFish> stateMachine) : base(owner, stateMachine)
            {

            }

            public override void Enter()
            {
                owner.isHittable = true;
            }

            public override void Exit()
            {

            }

            public override void Setup()
            {

            }

            public override void Transition()
            {

            }

            public override void Update()
            {
                stateMachine.ChangeState(State.Move);
            }
        }
        private class MoveState : AttackableFishState
        {
            public MoveState(AttackableFish owner, StateMachine<State, AttackableFish> stateMachine) : base(owner, stateMachine)
            {

            }

            public override void Enter()
            {

            }

            public override void Exit()
            {

            }

            public override void Setup()
            {

            }

            public override void Transition()
            {
                // ignore raycast 레이어와 만나면
                //if (Physics.SphereCast(transform.position, 0, Vector3.zero, out hit, data.PlayerRecognitionRange, 1 << 2))
                //{
                //    Debug.Log("Detected Diver");
                //    owner.diver = hit.transform.gameObject.GetComponent<Diver>();
                //    stateMachine.ChangeState(State.Attack);
                //}
            }

            public override void Update()
            {
                if (owner.WallDetect() && !owner.isDirChangeActive)
                {
                    owner.ChangeMoveDir();
                }
                transform.Translate(moveDir * owner.data.MoveSpeed * Time.deltaTime, Space.World);
            }
        }
        private class HitState : AttackableFishState
        {
            public HitState(AttackableFish owner, StateMachine<State, AttackableFish> stateMachine) : base(owner, stateMachine)
            {

            }

            public override void Enter()
            {
                owner.isHittable = false;
                owner.curHp -= owner.curHitDamage;
            }

            public override void Exit()
            {

            }

            public override void Setup()
            {

            }

            public override void Transition()
            {
                if (owner.curHp <= 0)
                {
                    stateMachine.ChangeState(State.Die);
                }
                else if (owner.curHp > 0)
                {
                    stateMachine.ChangeState(State.Attack);
                }
            }

            public override void Update()
            {
                
            }
            IEnumerator HitRoutine()
            {

                yield return new WaitForSeconds(3f);
            }
        }
        private class AttackState : AttackableFishState
        {
            Vector3 attackMoveDir;
            public AttackState(AttackableFish owner, StateMachine<State, AttackableFish> stateMachine) : base(owner, stateMachine)
            {

            }

            public override void Enter()
            {
            }

            public override void Exit()
            {

            }

            public override void Setup()
            {

            }

            public override void Transition()
            {
            }

            public override void Update()
            {
                attackMoveDir = (owner.player.transform.position - transform.position).normalized;
                // escapeSpeed 보단 extraSpeed를 공용으로 사용해야했다
                transform.Translate(attackMoveDir * owner.data.EscapeSpeed * Time.deltaTime, Space.World);
            }

        }
        private class DieState : AttackableFishState
        {
            public DieState(AttackableFish owner, StateMachine<State, AttackableFish> stateMachine) : base(owner, stateMachine)
            {

            }

            public override void Enter()
            {
                owner.Die();
            }

            public override void Exit()
            {

            }

            public override void Setup()
            {

            }

            public override void Transition()
            {

            }

            public override void Update()
            {

            }
        }

        #endregion


        IEnumerator MoveDirRoutine()
        {
            while (true)
            {
                if (isDirChangeActive)
                {
                    yield return null;
                }
                else
                {
                    moveDir = GetRandVector();
                }
                //Debug.Log(moveDir);
                //Move();

                yield return new WaitForSeconds(Random.Range(3f, 5f));
            }
        }
        public void Attack()
        {

        }
        public override string GetCurState()
        {
            return stateMachine.GetCurStateName();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 12)
            {
                if (collision.gameObject.GetComponent<AttackSpear>() != null)
                {
                    if (!isHittable) return;
                    curHitDamage = collision.gameObject.GetComponent<AttackSpear>().Damage;
                    stateMachine.ChangeState(State.Hit);
                }
                if (collision.gameObject.GetComponent<ReturnSpear>() != null)
                {
                    if (!isHittable) return;
                    curHitDamage = collision.gameObject.GetComponent<ReturnSpear>().Damage;
                    stateMachine.ChangeState(State.Hit);
                }
            }
        }
    }
}