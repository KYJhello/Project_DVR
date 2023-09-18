using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class WaitState : StateMachineBehaviour
    {
        StateCorutineManager corutineManager;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            corutineManager = animator.GetComponent<StateCorutineManager>();
            corutineManager.StartCoroutine(corutineManager.FoodWaitRoutine());  // 60초 세기 시작
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // if (내 테이블에 음식이 올려졌다면)
            if (animator.GetComponent<Customer>().mySeat.gameObject.GetComponentInChildren<PlateRecognition>().IsPlate())
            {
                animator.SetTrigger("Eat");
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // 기다리던 코루틴 Stop
            corutineManager.StopCoroutine(corutineManager.FoodWaitRoutine());
        }
    }
}
