using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace AHN
{
    public class EatState : StateMachineBehaviour
    {
        StateCorutineManager corutineManger;
        int amount = 15;     // 결제한 금액. 일단 15라고 적어놓음

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

            // TODO : 접시를 풀로 생성하는지 Inst그걸로 생성하는지 물어봐야함. 일단 destroy로 없어지게 해놨음


            List<GameObject> plateAndFoods = animator.GetComponent<Customer>().mySeat.gameObject.GetComponentInChildren<PlateRecognition>().PlateAndFood();

            foreach (GameObject plateAndFood in plateAndFoods)
            {
                // 결제
                if (plateAndFood.layer == 23)   // 먹은 음식 중 초밥이 있다면
                {
                    int myScore = plateAndFood.gameObject.GetComponent<SushiScore>().sushiScore;  // 그 초밥의 점수를 받아옴
                    PosManager.OnPayEvent?.Invoke(myScore);
                }
                Destroy(plateAndFood);      // 테이블 위에 올려진 접시 및 초밥 없어짐
            }
        }
    }
}