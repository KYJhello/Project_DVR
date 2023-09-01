using KIM;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace KIM
{
    public class NonAttackableFish : Fish
    {
        //public enum State { State_Idle = 0, State_Move, State_Hit, State_Escape, State_Die }
        //StateMachine<State, NonAttackableFish> stateMachine;

        private void Awake()
        {
            StartCoroutine(MoveRoutine());
            //stateMachine = new StateMachine<State, NonAttackableFish>(this);
            //stateMachine.AddState(State.State_Idle, new IdleState(this, stateMachine));

        }

        //#region FishState
        //private abstract class NonAttackableFishState : StateBase<State, NonAttackableFish>
        //{
        //    protected GameObject gameObject => owner.gameObject;
        //    protected FishData data => owner.data;
        //    protected int curHp => owner.curHp;
        //    protected float curLength => owner.curLength;
        //    protected float curWeight => owner.curWeight;

        //    protected NonAttackableFishState(NonAttackableFish owner, StateMachine<State, NonAttackableFish> stateMachine) : base(owner, stateMachine)
        //    {

        //    }
        //}

        //private class IdleState : NonAttackableFishState
        //{
        //    public IdleState(NonAttackableFish owner, StateMachine<State, NonAttackableFish> stateMachine) : base(owner, stateMachine)
        //    {

        //    }

        //    public override void Enter()
        //    {

        //    }

        //    public override void Exit()
        //    {

        //    }

        //    public override void Setup()
        //    {

        //    }

        //    public override void Transition()
        //    {

        //    }

        //    public override void Update()
        //    {

        //    }
        //}
        //#endregion

        private void Update()
        {
            transform.Translate(moveDir.normalized * Time.deltaTime);

        }

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
            
            // 벽이 있는지 감지
            if (WallDetect())
            {
                Ray ray = new Ray();
                ray.origin = this.transform.position;
                ray.direction = this.transform.position;
                RaycastHit hit;
                // 표면으로 레이케스트를 보내 RaycastHit을 구한다
                Physics.Raycast(ray, out hit, data.WallRecognitionRange, 1 << 14);

                // 벽 표면의 노말백터와 진행방향의 오른쪽노말백터를 내적하여
                // 값이 양수면 오른쪽, 음수면 왼쪽으로 진행
                moveDir = Vector3.Dot(hit.normal, transform.right) >= 0 ?
                    transform.right / 2 : -transform.right / 2;

                // 벽 표면의 노말벡터와 진행방향의 위쪽 벡터를 내적하여
                // 값이 양수면 위, 음수면 아래로 진행
                moveDir += Vector3.Dot(hit.normal, transform.up) >= 0 ?
                    transform.up / 2 : -transform.up / 2;

                moveDir = moveDir.normalized;
            }

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