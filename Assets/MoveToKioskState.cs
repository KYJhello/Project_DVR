using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToKioskState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)   // 매장에 들어오자마자 할 일
    {
        
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)  // 키오스크까지 걸어가는 일
    {
        // 애니메이션으로 계속 걸어가다가 저장해놓은 키오스크 앞 Position의 값과 
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)    // 키오스크에 딱 도착할 때쯤
    {
        
    }
}
