using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : StateMachineBehaviour
{
    StateCorutineManager corutineManager;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 시간 재기 시작!

        corutineManager = animator.GetComponent<StateCorutineManager>();
        corutineManager.StartCoroutine(corutineManager.FoodWaitRoutine());  // 60초 세기 시작
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // if (내 테이블에 음식이 올려졌다면)
        // animator.Settrigger("Eat");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 시간 초기화
        // corutineManager.StopCoroutine(corutineManager.FoodWaitRoutine());
    }
}
