using KIM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AHN
{
    public class OrderState : StateMachineBehaviour
    {
        StateCorutineManager corutineManager;
        List<List<string>> fishs;
        List<string> fishInfo;
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
            // 여기서 필요한 건 0, 3. (2는 나중에)
            fishs = GameObject.FindObjectOfType<KIM_FishTank>().ReturnFishTankFishList();   // 수족관에 있는 물고기들 정보를 받아옴

            // TODO : 살점이 5개씩 나오니까 fishs 리스트를 5개 복사해야함 

            if (fishs.Count <= 0)
            {
                animator.SetTrigger("GoOut");    // TODO : 얘 왜 안 나가지
                Debug.Log("물고기가 없어 주문이 불가능합니다.");
            }
            else
            {
                int orderFishIndex = Random.Range(0, fishs.Count);    // 주문할 물고기 리스트 순서
                fishInfo = fishs[orderFishIndex];   // 주문할 물고기의 4개 정보가 담겨있는 리스트
                GameManager.Instantiate(orderSheet, orderSheetPoolPosition.position, Quaternion.Euler(90f, 0, 0));

                // TODO : argumnet 오류뜸 -> 테이블 번호 다 똑같이 출력됨
                orderSheet.GetComponent<OrderSheet>().MenuTextInput(fishInfo[0], animator.gameObject.GetComponent<Customer>().mySeatNumber());
                //                                                  물고기 이름              테이블 번호


                Debug.Log(animator.gameObject.GetComponent<Customer>().mySeatNumber());

                fishs.RemoveAt(orderFishIndex);     // 주문한 물고기 인덱스 삭제
            }
        }
    }
}