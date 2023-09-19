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
            List<GameObject> plateAndFoods = animator.GetComponent<Customer>().mySeat.gameObject.GetComponentInChildren<FoodRecognitionOnTable>().PlateAndFood();

            foreach (GameObject plateAndFood in plateAndFoods)
            {
                if (plateAndFoods.Count <= 0)
                {
                    return;
                }    

                // 결제
                if (plateAndFood.layer == 23)   // 먹은 음식 중 초밥이 있다면
                {
                    int myScore = plateAndFood.gameObject.GetComponent<SushiScore>().sushiScore;  // 그 초밥의 점수를 받아옴
                    PosManager.OnAddPayEvent?.Invoke(myScore);
                    Destroy(plateAndFood);      // 테이블 위에 올려진 접시 및 초밥 없어짐
                }
                else
                {
                    animator.GetComponent<Customer>().mySeat.gameObject.GetComponentInChildren<FoodRecognitionOnTable>().IsPlateFalse();
                    Destroy(plateAndFood);      // 테이블 위에 올려진 접시 및 초밥 없어짐
                }
            }
            plateAndFoods.Clear();
        }
    }
}