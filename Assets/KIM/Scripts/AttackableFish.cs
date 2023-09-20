using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public class AttackableFish : Fish, IAttackable
    {
        
        public enum State { Idle = 0, Move, Hit, Attack, Die }
        StateMachine<State, AttackableFish> stateMachine;

        Coroutine moveDirRoutine;
        SphereCollider recogRangeTrigger;

        Vector3 playerPos;

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
            RaycastHit hit;
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
                if (Physics.SphereCast(transform.position, 0, Vector3.zero, out hit, data.PlayerRecognitionRange, 1 << 2))
                {
                    stateMachine.ChangeState(State.Attack);
                }
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
        private class AttackState : AttackableFishState
        {
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
                transform.LookAt(owner.playerPos);
                transform.Translate((owner.playerPos - transform.position).normalized * owner.data.MoveSpeed * Time.deltaTime, Space.World);

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
        private void OnTriggerStay(Collider other)
        {
            if(other.gameObject.layer == 2)
            {
                Debug.Log("Player Detected");
                playerPos = other.transform.position;
                stateMachine.ChangeState(State.Attack);
            }
        }
    }
}