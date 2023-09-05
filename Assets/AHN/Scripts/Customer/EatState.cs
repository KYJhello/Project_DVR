using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class EatState : StateMachineBehaviour
    {
        StateCorutineManager corutineManger;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            corutineManger = animator.GetComponent<StateCorutineManager>();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            corutineManger.StartCoroutine(corutineManger.EatRoutine());
            animator.SetTrigger("GoOut");
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // TODO : 결제.
            // 완성된 초밥에 이미 있는 int(총점수)를 받아올건데, 초밥에서 int(총점수)를 반환하는 함수를 만들고 유니티 이벤트로 그 함수를 담음
        }
    }
}