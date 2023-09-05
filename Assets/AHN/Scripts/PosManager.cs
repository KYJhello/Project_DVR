using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AHN
{
    public class PosManager : MonoBehaviour
    {
        public static UnityEvent<int> OnPayEvent = new UnityEvent<int>();
        [SerializeField] TextMesh addSalesText;
        [SerializeField] TextMesh totalSalesText;
        [SerializeField] GameObject orderSheet;     // 주문서
        private int totalSales;
        public int TotalSales { get { return totalSales; } set { totalSales = value; } }

        private void OnEnable()
        {
            OnPayEvent.AddListener(IncreaseInSales);
            OnPayEvent.AddListener(AddText);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        // 1. TODO : 주문출력함수
        public void PrintOrderSheet()
        {
            // Customer의 Orderstate-Exit()에서 호출할 주문출력함수()
            // 주문출력함수()에는 포스기 앞에 메뉴가 적힌 주문서가 뿅하고 생기는 함수.
            // *주문서는 Grab Interactable
            // 주문서가 손님 수만큼 생성되니까 주문서도 풀링으로 해야하나?
            // 주문서는 손님이 Seat를 참조하는 것처럼 이미 있는 메뉴중 랜덤값을 참조
        }

        void IncreaseInSales(int amount)
        {
            totalSales += amount;
            totalSalesText.text = $"합계 : {totalSales}";
        }

        public void AddText(int amount)
        {
            StartCoroutine(AppearAddTextRoutine(amount));
            IncreaseInSales(amount);    // 총매출 늘려줌
        }

        IEnumerator AppearAddTextRoutine(int amount)
        {
            addSalesText.text = $"결제금액 : {amount}원";
            yield return new WaitForSeconds(5f);
            addSalesText.text = "";
        }
    }
}
