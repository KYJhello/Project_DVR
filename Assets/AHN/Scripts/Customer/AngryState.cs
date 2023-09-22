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
            // TODO : 타이머 시간 감소시켜야 함. 일단 -10초로
            
            // 기다리던 코루틴 Stope
            corutineManager.StopCoroutine(corutineManager.FoodWaitRoutine());

            // 주문했던 물고기를 (1)리스트에서 RemoveAt했다면 다시 Add 해주고
            if (animator.GetComponent<OrderState>().isUseNewFish)
            {
                // 주문한 물고기 인덱스를 받아와서 Add리스트(인덱스) 해줌
                MenuManager.fishs.Add(OrderState.fishInfo);
            }
            
            // (2) 횟 점 count-- 해줬다면 다시 count++ 해야함.   
            // if (animator.GetCom<OrderState>().bool 함수를 찾아서 사시미를 이용한 bool이 true면, foreach로 내가 주문한 생선을 찾아서 기존의 값으로 돌려줘야함
            if (animator.GetComponent<OrderState>().isUseSasimi)
            {
                // foreach로 내가 주문한 생선을 찾아서 기존의 값으로 돌려줘야함
                foreach (List<string> innerSushiCounts in MenuManager.sasimiCounts)     // 잘라놓은 횟 점들의 리스트를 둘러보고
                {
                    if (innerSushiCounts[0] == OrderState.fishInfo[0])     // 만약 잘려있는 회의 리스트 중에 주문한 회의 이름이 있다면,
                    {
                        if (int.Parse(innerSushiCounts[1]) > 0)     // 횟 조각이 있다면
                        {
                            int count = int.Parse(innerSushiCounts[1]);

                            count++;
                            innerSushiCounts[1] = count.ToString();

                            break;
                        }
                    }
                }
            }
        }
    }
}