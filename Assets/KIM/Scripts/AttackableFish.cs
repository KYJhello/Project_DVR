using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public class AttackableFish : Fish, IAttackable
    {
        public enum State { State_Idle = 0, State_Move, State_Hit, State_Escape, State_Die, State_Attack }
        StateMachine<State, AttackableFish> stateMachine;

        private void Awake()
        {
            stateMachine = new StateMachine<State, AttackableFish>(this);
            stateMachine.AddState(State.State_Idle, new IdleState(this, stateMachine));
        }

        #region FishState
        private abstract class AttackableFishState : StateBase<State, AttackableFish>
        {
            protected GameObject gameObject => owner.gameObject;
            protected FishData data => owner.data;
            protected int curHp => owner.curHp;
            protected float curLength => owner.curLength;
            protected float curWeight => owner.curWeight;

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



        public void Attack()
        {

        }

        protected void Die()
        {
        }

        //protected override void Move()
        //{
        //}
    }
}