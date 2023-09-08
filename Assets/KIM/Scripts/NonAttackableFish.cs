using KIM;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace KIM
{
    public class NonAttackableFish : Fish
    {
        StateMachine<State, NonAttackableFish> stateMachine;

        Coroutine nomalMoveRoutine;
        Coroutine wallEscapeRoutine;

        protected override void Awake()
        {
            base.Awake();

            StartCoroutine(MoveRoutine());
            stateMachine = new StateMachine<State, NonAttackableFish>(this);
            stateMachine.AddState(State.Idle, new IdleState(this, stateMachine));
            stateMachine.AddState(State.Move, new MoveState(this, stateMachine));
            stateMachine.AddState(State.Store, new StoreState(this, stateMachine));
            
        }
        private void Start()
        {
            stateMachine.SetUp(State.Idle);
        }
        private void Update()
        {
            stateMachine.Update();
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
                if (owner.inStore)
                {
                    stateMachine.ChangeState(State.Store);
                }
                stateMachine.ChangeState(State.Move);
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

            }

            public override void Update()
            {
                //owner.Move();

                if (owner.WallDetect())
                {
                    Debug.Log("wallDetect");
                    owner.ChangeMoveDir();
                }

                transform.Translate(moveDir * owner.data.MoveSpeed * Time.deltaTime);
            }
        }
        private class StoreState : NonAttackableFishState
        {
            public StoreState(NonAttackableFish owner, StateMachine<State, NonAttackableFish> stateMachine) : base(owner, stateMachine)
            {

            }

            public override void Enter()
            {
                Debug.Log("IN Store");
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


        IEnumerator MoveRoutine()
        {
            while (true)
            {
                moveDir = GetRandVector();
                Debug.Log(moveDir);
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
        
        protected void Die()
        {
        }

        //protected override void Move()
        //{
        //    base.Move();
        //}
    }
}