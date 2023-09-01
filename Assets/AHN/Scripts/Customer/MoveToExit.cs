using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class MoveToExit : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // 만약 이전의 anim이 angry였다면, 총 시간을 (15초 정도)감소시켜야 해!
            // 이거 유니티 이벤트로 받아와야 하나...
            // 일단 StateCorutineManager에서 구현하도록 ㄱㄱ
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // 여기서 손님은 없어져야함. 비활성화해서 풀링으로 구현해도 ㄱㅊ을듯. 근데 일단은 Destroy로 구현 ㄱ
        }
    }
}