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
            if (fishs.Count <= 0)
            {
                animator.SetTrigger("GoOut");
            }
            else
            {
                int orderFishIndex = Random.Range(0, fishs.Count);    // 주문할 물고기 리스트 순서
                fishInfo = fishs[orderFishIndex];   // 주문할 물고기의 4개 정보가 담겨있는 리스트
                GameManager.Pool.Get(orderSheet, orderSheetPoolPosition.position, Quaternion.Euler(90f, 0, 0));     // 주문서 출력
                Debug.Log(fishInfo);

                orderSheet.GetComponent<OrderSheet>().MenuTextInput(fishInfo[0], animator.gameObject.GetComponent<Customer>().mySeatNumber());
                //                                                  물고기 이름              테이블 번호

                // TODO : 나중에 주문서 없앨 때 Pool로 Release해서 없애야 함.


                fishInfo.Clear();
                fishs.RemoveAt(orderFishIndex);     // 주문한 물고기 인덱스 삭제
            }
        }
    }
}