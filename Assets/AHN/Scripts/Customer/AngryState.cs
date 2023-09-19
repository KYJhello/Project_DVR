using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class AngryState : StateMachineBehaviour
    {
        StateCorutineManager corutineManager;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            corutineManager = animator.GetComponent<StateCorutineManager>();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // if (내 테이블에 음식이 올려졌다면)
            if (animator.GetComponent<Customer>().mySeat.gameObject.GetComponentInChildren<FoodRecognitionOnTable>().IsPlate())
            {
                Debug.Log("Angry -> Eat");
                animator.SetTrigger("Eat");
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // 기다리던 코루틴 Stop
            corutineManager.StopCoroutine(corutineManager.FoodWaitRoutine());

            // TODO : 타이머 시간 감소시켜야 함. 일단 -10초로
        }
    }
}