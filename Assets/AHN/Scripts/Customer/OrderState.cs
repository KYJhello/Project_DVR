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
            Debug.Log(fishs);

            // TODO : (나중에)물고기 하나 썰면 회가 3조각 (일단 3조각으로) 나오니까 총 fishInfo.Count() * 3이 주문할 수 있는 최대 개수.
            // 카운트에서 랜덤으로 하나 뽑으면 List의 그 순서의 물고기 주문. 그러면서 그 순서는 삭제.
            // 주문할 땐 0번인 "name" 초밥 이 주문서의 text에 써지면 됨. 결제 때문에 3번도 받아오면 됨.
            if (fishs.Count <= 0)
            {
                animator.SetTrigger("GoOut");
            }
            else
            {
                int orderFishIndex = Random.Range(0, fishs.Count);    // 주문할 물고기 리스트 순서
                fishInfo = fishs[orderFishIndex];   // 주문할 물고기의 4개 정보가 담겨있는 리스트
                GameManager.Pool.Get(orderSheet, orderSheetPoolPosition.position, Quaternion.identity);     // 주문서 출력

                Debug.Log(fishInfo);

                // TODO : orderSheet의 text 변경
                orderSheet.GetComponent<OrderSheet>().MenuTextInput(fishInfo[0], animator.gameObject.GetComponent<Customer>().mySeatNumber());
                //                                                  물고기 이름              테이블 번호

                fishInfo.Clear();
                fishs.RemoveAt(orderFishIndex);     // 주문한 물고기 인덱스 삭제
            }
        }
    }
}