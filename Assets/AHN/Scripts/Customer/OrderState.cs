using KIM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

namespace AHN
{
    public class OrderState : StateMachineBehaviour
    {
        StateCorutineManager corutineManager;
        //List<List<string>> fishs = new List<List<string>>();
        //List<List<string>> fishsCopy = new List<List<string>>();
        List<string> fishInfo = new List<string>();
        GameObject orderSheet;
        Transform orderSheetPoolPosition;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            corutineManager = animator.GetComponent<StateCorutineManager>();
            orderSheet = GameManager.Resource.Load<GameObject>("OrderSheet");
            orderSheetPoolPosition = GameObject.Find("OrderSheetPoolPosition").transform;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            corutineManager.StartCoroutine(corutineManager.OrderingRoutine());
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // fish list <fishInfo> 
            // fishInfo = name = 0, weight = 1, length = 2, FishRank = 3
            /*
            fishs = GameObject.FindObjectOfType<KIM_FishTank>().ReturnFishTankFishList();   // 수족관에 있는 물고기들 정보를 받아옴
            List<List<string>> fishsCopy = new List<List<string>>(fishs);

            for (int i = 0; i < 5; i++) 
            {
                fishs.AddRange(fishsCopy);      // 물고기 하나 당 살점이 5개 나오므로 배열 5개 복사
            }

            if (fishs.Count <= 0)
            {
                Debug.Log("물고기가 없어 주문이 불가능합니다.");
            }
            else 
            {
                int orderFishIndex = Random.Range(0, fishs.Count);    // 주문할 물고기 리스트 순서
                fishInfo = fishs[orderFishIndex];   // 주문할 물고기의 4개 정보가 담겨있는 리스트

                // 주문서 출력
                GameManager.Instantiate(orderSheet, orderSheetPoolPosition.position, Quaternion.Euler(90f, 0, 0));
                orderSheet.GetComponent<OrderSheet>().MenuTextInput(fishInfo[0], animator.gameObject.GetComponent<Customer>().mySeatNumber());
                //                                                  물고기 이름              테이블 번호

                fishs.RemoveAt(orderFishIndex);     // 주문한 물고기 인덱스 삭제
            }*/



            if (MenuManager.fishs.Count >= 0)       // 만약 현재 수족관에 물고기가 있다면,
            {
                int randomFishIndex = Random.Range(0, MenuManager.fishs.Count - 1);     // 수족관에 있는 물고기의 종류 중 하나고름. 주문할 물고기 리스트 순서
                fishInfo = MenuManager.fishs[randomFishIndex];   // 주문할 물고기의 4개 정보가 담겨있는 리스트

                // 수족관에 있는 물고기 종류중 하나 골랐고,
                // 그 물고기를 기준으로 스테이크 할지 스시로 만들지를 선택함.
                int randomMenuIndex = Random.Range(0, 1);
                switch (randomMenuIndex)
                {
                    case 0:     // 스테이크으로 만들기
                        // 주문서 출력
                        GameManager.Instantiate(orderSheet, orderSheetPoolPosition.position, Quaternion.Euler(90f, 0, 0));
                        string menuName1 = $"{fishInfo[0]}\nSteak";
                        orderSheet.GetComponent<OrderSheet>().MenuTextInput(menuName1, animator.gameObject.GetComponent<Customer>().mySeatNumber());

                        MenuManager.fishs.RemoveAt(randomMenuIndex);     // 주문한 물고기 인덱스 삭제
                        break;

                    case 1:     // 초밥으로 만들기
                        // 주문서 출력
                        GameManager.Instantiate(orderSheet, orderSheetPoolPosition.position, Quaternion.Euler(90f, 0, 0));
                        string menuName2 = $"{fishInfo[0]}\nSushi";
                        orderSheet.GetComponent<OrderSheet>().MenuTextInput(menuName2, animator.gameObject.GetComponent<Customer>().mySeatNumber());


                        // 주문한 물고기 인덱스를 삭제하지 않고, 만약 이미 잘라놓은 이 종류의 회가 있다면,
                        // { A회, 0 } ... 이 리스트를 불러와서, foreach로 하나씩 꺼낸다음 [0]의 이름이 주문한 물고기의 이름과 동일하다면,
                        
                        foreach (List<string> innerList in MenuManager.sasimiCounts)
                        {
                            if (innerList[0] == fishInfo[0])    // 만약 잘려있는 회의 리스트 중에 주문한 회의 이름이 있다면,
                            {
                                // innerList[1]을 확인해서 이게 >= 이라면, 이걸 -- 해줌. 한 조각 사라짐
                                if (int.Parse(innerList[1]) >= 0)
                                {
                                    int count = int.Parse(innerList[1]);
                                    count--;
                                    innerList[1] = count.ToString();
                                    break;
                                }
                            }
                        }

                        // 여기로 왔다면 남은 횟조각이 없는 거임.
                        // 즉 수족관에서 새 물고기를 꺼낼테니, 그 물고기를 리스트에서 제거해주고 count는 4로 만들기.
                        // MenuManager.sasimiCounts.Add(주문한 물고기 이름, 4)
                        List<string> newSasimiCountList = new List<string>();
                        newSasimiCountList.Add(menuName2);
                        newSasimiCountList.Add("4");
                        MenuManager.sasimiCounts.Add(newSasimiCountList);
                        MenuManager.fishs.RemoveAt(randomMenuIndex);     // 주문한 물고기 인덱스 삭제

                        break;

                    default:
                        break;
                }
            }
            else   // 수족관에 물고기가 없다면, 스시로만 주문해야 하며, 현재 잘라놓은 회Count가 있는 지 확인해야함. 
            {
                foreach (List<string> innerList in MenuManager.sasimiCounts)        // 잘라놓은 횟조각 리스트를 둘러보고
                {
                    if (innerList[0] == fishInfo[0])    // 만약 잘려있는 회의 리스트 중에 주문한 회의 이름이 있다면,
                    {
                        // innerList[1]을 확인해서 이게 >= 이라면, 이걸 -- 해줌. 한 조각 사라짐
                        if (int.Parse(innerList[1]) >= 0)
                        {
                            int count = int.Parse(innerList[1]);
                            count--;
                            innerList[1] = count.ToString();

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