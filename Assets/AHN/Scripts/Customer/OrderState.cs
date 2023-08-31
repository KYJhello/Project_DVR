using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderState : StateMachineBehaviour
{
    StateCorutineManager corutineManager;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        corutineManager = animator.GetComponent<StateCorutineManager>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // animator.SetTrigger("주문하는애니메이션); 이거 실행만 해주면 됨.
        // 3초 동안 주문하는 애니메이션 실행되어야 해서 밑에 따로 코루틴 함수로 구현하는 게 나을듯.
        corutineManager.StartCoroutine(corutineManager.OrderingRoutine());
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
