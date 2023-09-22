using KIM;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ProBuilder.MeshOperations;

namespace AHN
{
    public class OrderState : StateMachineBehaviour
    {
        // 여기에 횟점을 ++ 해주는 함수 하나랑, 리스트에 Add 해주는 함수를 각각 만들어서
        // 이벤트를 만들어서, 횟 점들을 이용해 만드는 것과 수족관에서 물고기를 빼올 때 각 이벤트를 추가하여 AngryExit에서 호출함. 


        public static UnityEvent<int> OnIncreasesasimiCount = new UnityEvent<int>();
        public static UnityEvent<List<string>> OnAddFishListCount = new UnityEvent<List<string>>();
        StateCorutineManager corutineManager;
        public static List<string> fishInfo = new List<string>();
        GameObject orderSheet;
        Transform orderSheetPoolPosition;
        public bool isUseSasimi = false;
        public int initSasimiCount;
        public bool isUseNewFish = false;
        public int randomFishIndex;
        public int count;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            corutineManager = animator.GetComponent<StateCorutineManager>();
            orderSheet = GameManager.Resource.Load<GameObject>("OrderSheet");
            orderSheetPoolPosition = GameObject.Find("OrderSheetPoolPosition").transform;

            corutineManager.StartCoroutine(corutineManager.OrderingRoutine());
        }


        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (MenuManager.fishs.Count > 0)       // 만약 현재 수족관에 물고기가 있다면,
            {
                 randomFishIndex = Random.Range(0, MenuManager.fishs.Count - 1);     // 수족관에 있는 물고기의 종류 중 하나고름. 주문할 물고기 리스트 순서

                 fishInfo = MenuManager.fishs[randomFishIndex];   // 주문할 물고기의 4개 정보가 담겨있는 리스트
                 
                 // 수족관에 있는 물고기 종류중 하나 골랐고,
                 // 그 물고기를 기준으로 스테이크 할지 스시로 만들지를 선택함.
                 int randomMenuIndex = Random.Range(0, 2);
                 switch (randomMenuIndex)
                 {
                     case 0:     // 스테이크으로 만들기
                         // 주문서 출력
                         string menuName1 = $"{fishInfo[0]}\nSteak";
                         orderSheet.GetComponent<OrderSheet>().MenuTextInput(menuName1, animator.gameObject.GetComponent<Customer>().mySeatNumber());
                         GameManager.Instantiate(orderSheet, orderSheetPoolPosition.position, Quaternion.Euler(90f, 0, 0));

                         isUseNewFish = true;
                         MenuManager.fishs.RemoveAt(randomFishIndex);     // 주문한 물고기 인덱스 삭제
                         break;
                 
                     case 1:     // 초밥으로 만들기
                         // 주문서 출력
                         string menuName2 = $"{fishInfo[0]}\nSushi";
                         orderSheet.GetComponent<OrderSheet>().MenuTextInput(menuName2, animator.gameObject.GetComponent<Customer>().mySeatNumber());
                         GameManager.Instantiate(orderSheet, orderSheetPoolPosition.position, Quaternion.Euler(90f, 0, 0));
                 
                 
                         // 주문한 물고기 인덱스를 삭제하지 않고, 만약 이미 잘라놓은 이 종류의 회가 있다면,
                         // { A회, 0 } ... 이 리스트를 불러와서, foreach로 하나씩 꺼낸다음 [0]의 이름이 주문한 물고기의 이름과 동일하다면,
                        
                        // sasimiCounts 에서 fishiInfo[0]의 이름이 있는지 확인
                        foreach (List<string> InnersasimiCounts in MenuManager.sasimiCounts)
                        {
                            if (InnersasimiCounts[0] == fishInfo[0])    // 주문한 물고기를 찾았다면
                            {
                                // 그 물고기의 횟 조각이 몇 개 남았는지 확인
                                if (int.Parse(InnersasimiCounts[1]) > 0)    // 횟 조각이 있다면,
                                {
                                    count = int.Parse(InnersasimiCounts[1]);

                                    // 이 때의 count 값을 넘겨주는 함수를 만들어. int count2를 매개변수로 하고 위 count를 주어 return count2++;를 반환하는 함수.
                                    --count;    // 횟조각 하나 감소시켜줌
                                    InnersasimiCounts[1] = count.ToString();
                                    
                                    isUseSasimi = true;
                                }
                                else    // 횟조각이 없다면
                                {
                                    // 새 수족관에서 새 물고기를 꺼낼테니, 그 물고기를 리스트에서 제거해주고 그 물고기의 count를 9로 만듦. (총 10점이 나오고 지금 한 점을 쓸 것이니 9점으로)
                                    MenuManager.fishs.RemoveAt(randomFishIndex);
                                    InnersasimiCounts[1] = "9";

                                    isUseNewFish = true;
                                }
                            }
                            else    // 예외사항 
                                return;
                        }
                        break;
                     
                     default:
                        break;
                 }
            }

            else   // 수족관에 물고기가 없다면, 스시로만 주문해야 하며, 현재 잘라놓은 회Count가 있는 지 확인해야함. 
            {
                randomFishIndex = Random.Range(0, MenuManager.fishs.Count - 1);     // 수족관에 있는 물고기의 종류 중 하나고름. 주문할 물고기 리스트 순서

                fishInfo = MenuManager.fishs[randomFishIndex];   // 주문할 물고기의 4개 정보가 담겨있는 리스트

                foreach (List<string> innerSushiCounts in MenuManager.sasimiCounts)     // 잘라놓은 횟 점들의 리스트를 둘러보고
                {
                    if (innerSushiCounts[0] == fishInfo[0])     // 만약 잘려있는 회의 리스트 중에 주문한 회의 이름이 있다면,
                    {
                        if (int.Parse(innerSushiCounts[1]) > 0)     // 횟 조각이 있다면
                        {
                            count = int.Parse(innerSushiCounts[1]);

                            --count;
                            innerSushiCounts[1] = count.ToString();

                            isUseSasimi = true;

                            // 그리고 주문서 생성
                            GameManager.Instantiate(orderSheet, orderSheetPoolPosition.position, Quaternion.Euler(90f, 0, 0));
                            string menuName2 = $"{fishInfo[0]}\nSushi";
                            orderSheet.GetComponent<OrderSheet>().MenuTextInput(menuName2, animator.gameObject.GetComponent<Customer>().mySeatNumber());

                            break;

                        }
                    }
                }

                // 여기에 왔다면 잘린 횟조각도 없는 거라 GameOver 해주면 되는데, 일단 GoOut 되도록.
                animator.SetTrigger("GoOut");
                // TODO : Timer에서 GameOver 함수
            }
        }
    }
}