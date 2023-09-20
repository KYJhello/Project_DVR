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
            // 내 앞 테이블에 놓여진 음식들을 List plateAndFoods에 저장. (접시, 스시, 스테이크 ..)
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
                    int myScore = plateAndFood.gameObject.GetComponent<SushiInfo>().sushiScore;    // 그 초밥의 점수를 받아옴

                    // 내가 주문한 물고기가 맞는지 확인
                    if (OrderState.fishInfo[0] == plateAndFood.gameObject.GetComponent<SushiInfo>().fishName)
                    {
                        PosManager.OnAddPayEvent?.Invoke(myScore);
                    }
                    else    // 아니라면
                    {
                        myScore = 0;    // 점수 없음
                        PosManager.OnAddPayEvent?.Invoke(myScore);
                    }

                    Destroy(plateAndFood);      // 테이블 위에 올려진 접시 및 초밥 없어짐
                }
                else if (plateAndFood.layer == 18)      // 먹은 음식 중 스테이크가 있다면
                {
                    int myScore = plateAndFood.gameObject.GetComponent<SteakInfo>().steakScore;    // 스테이크의 점수를 받아옴

                    // 내가 주문한 물고기가 맞는지 확인
                    if (OrderState.fishInfo[0] == plateAndFood.gameObject.GetComponent<SteakInfo>().fishName) 
                    {
                        PosManager.OnAddPayEvent?.Invoke(myScore);
                    }
                    else    // 아니라면
                    {
                        myScore = 0;    // 점수 없음
                        PosManager.OnAddPayEvent?.Invoke(myScore);
                    }

                    Destroy(plateAndFood);
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