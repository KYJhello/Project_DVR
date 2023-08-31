using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatState : StateMachineBehaviour
{
    StateCorutineManager corutineManger;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        corutineManger = animator.GetComponent<StateCorutineManager>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 음식 먹는 애니메이션 7초 정도?
        corutineManger.StartCoroutine(corutineManger.EatRoutine());
        animator.SetTrigger("GoOut");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
