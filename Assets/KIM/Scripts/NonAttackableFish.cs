using KIM;
using LM;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;


namespace KIM
{
    public class NonAttackableFish : Fish
    {
        // 인게임 테스트용 버튼
        [SerializeField] bool TESTDIEBUTTON;
        public enum State { Idle = 0, Move, Hit, Escape, Die }
        

        StateMachine<State, NonAttackableFish> stateMachine;

        Coroutine moveDirRoutine;
        Coroutine wallEscapeRoutine;


        protected override void Awake()
        {
            base.Awake();

            moveDirRoutine = StartCoroutine(MoveDirRoutine());
            stateMachine = new StateMachine<State, NonAttackableFish>(this);
            stateMachine.AddState(State.Idle,   new IdleState(this, stateMachine));
            stateMachine.AddState(State.Move,   new MoveState(this, stateMachine));
            stateMachine.AddState(State.Hit,    new HitState(this, stateMachine));
            stateMachine.AddState(State.Escape, new EscapeState(this, stateMachine));
            stateMachine.AddState(State.Die,    new DieState(this, stateMachine));


        }
        private void Start()
        {
            stateMachine.SetUp(State.Idle);
        }
        private void Update()
        {
            if (TESTDIEBUTTON)
            {
                stateMachine.ChangeState(State.Die);
            }
            stateMachine.Update();
            transform.rotation = Quaternion.LookRotation(moveDir);
        }

        #region FishState
        private abstract class NonAttackableFishState : StateBase<State, NonAttackableFish>
        {
            protected GameObject gameObject => owner.gameObject;
            protected Transform transform => owner.transform;
            protected FishData data => owner.data;
            protected int curHp => owner.curHp;
            protected float curLength => owner.curLength;
            protected float curWeight => owner.curWeight;
            protected Vector3 moveDir => owner.moveDir;

            protected NonAttackableFishState(NonAttackableFish owner, StateMachine<State, NonAttackableFish> stateMachine) : base(owner, stateMachine)
            {

            }
        }

        private class IdleState : NonAttackableFishState
        {
            public IdleState(NonAttackableFish owner, StateMachine<State, NonAttackableFish> stateMachine) : base(owner, stateMachine)
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
        private class MoveState : NonAttackableFishState
        {
            public MoveState(NonAttackableFish owner, StateMachine<State, NonAttackableFish> stateMachine) : base(owner, stateMachine)
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
                //stateMachine.ChangeState(State.Die);
            }

            public override void Update()
            {
                //owner.Move();

                //Debug.Log(moveDir);
                if (owner.WallDetect() && !owner.isDirChangeActive)
                {
                    //Debug.Log("wallDetect");
                    owner.ChangeMoveDir();
                }
                //if (owner.isDirChangeActive)
                //{
                //    //return;
                //}
                transform.Translate(moveDir * owner.data.MoveSpeed * Time.deltaTime, Space.World);
            }
        }
        private class HitState : NonAttackableFishState
        {
            public HitState(NonAttackableFish owner, StateMachine<State, NonAttackableFish> stateMachine) : base(owner, stateMachine)
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
                }else if(owner.curHp > 0)
                {
                    stateMachine.ChangeState(State.Escape);
                }
            }

            public override void Update()
            {

            }
        }
        private class EscapeState : NonAttackableFishState
        {
            private Vector3 escapeMoveDir;
            public EscapeState(NonAttackableFish owner, StateMachine<State, NonAttackableFish> stateMachine) : base(owner, stateMachine)
            {

            }

            public override void Enter()
            {
                owner.wallEscapeRoutine = owner.StartCoroutine(EscapeMoveRoutine());
            }

            public override void Exit()
            {
                owner.StopCoroutine(owner.wallEscapeRoutine);
            }

            public override void Setup()
            {

            }

            public override void Transition()
            {
            }

            public override void Update()
            {
                //if (owner.WallDetect() && !owner.isDirChangeActive)
                //{
                //    //Debug.Log("wallDetect");
                //    owner.ChangeMoveDir();
                //}

                transform.Translate(escapeMoveDir * owner.data.EscapeSpeed * Time.deltaTime, Space.World);
            }
            IEnumerator EscapeMoveRoutine()
            {
                while (true)
                {
                    escapeMoveDir = owner.GetRandVector();
                    yield return new WaitForSeconds(1f);
                    escapeMoveDir = owner.GetRandVector();
                    yield return new WaitForSeconds(1f);
                    escapeMoveDir = owner.GetRandVector();
                    yield return new WaitForSeconds(1f);
                    escapeMoveDir = owner.GetRandVector();
                    yield return new WaitForSeconds(1f);
                    stateMachine.ChangeState(State.Idle);
                }
            }
        }
        private class DieState : NonAttackableFishState
        {
            public DieState(NonAttackableFish owner, StateMachine<State, NonAttackableFish> stateMachine) : base(owner, stateMachine)
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

                yield return new WaitForSeconds(Random.Range(3f,5f));
            }
        }

        // Boids 알고리즘 사용해보기
        // 결합 : 일정 거리 내 개체들이 평균 위치로 이동
        // 분리 : 일정 거리 내 다른 개체와 뭉치는것을 피함
        // 정렬 : 일정 거리 내 개채들이 평균 방향으로 움직인다
        // 벽을 피해야함(오브젝트 일정거리 내에 벽을 감지해서 회피기동하도록)
        protected void Move()
        {
            
        }
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.layer == 12)
            {
                if(collision.gameObject.GetComponent<AttackSpear>() != null)
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
        public override string GetCurState()
        {
            return stateMachine.GetCurStateName();
        }
    }
}