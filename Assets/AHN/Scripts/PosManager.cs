using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace AHN
{
    public class PosManager : MonoBehaviour
    {
        public static UnityEvent<int> OnPayEvent = new UnityEvent<int>();   // EatState에서 호출할 event
        [SerializeField] TMP_Text paymentAmountText;
        [SerializeField] TMP_Text totalSalesText;
        [SerializeField] TMP_Text fundText;
        [SerializeField] Transform orderSheetPoolPosition;
        GameObject orderSheet;     // 주문서
        private int totalSales;
        public int TotalSales { get { return totalSales; } set { totalSales = value; } }

        private void Start()
        {
            orderSheet = GameManager.Resource.Load<GameObject>("OrderSheet");
            orderSheetPoolPosition = GameObject.Find("OrderSheetPoolPosition").GetComponent<Transform>();
        }

        private void OnEnable()
        {
            OnPayEvent.AddListener(TotalSalesText);
            OnPayEvent.AddListener(PaymentAmountText);
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
            orderSheet = GameManager.Pool.Get(orderSheet, orderSheetPoolPosition.position, Quaternion.identity);

            // TODO : 주문서 나중에 Release 해줘야 하는데 그건 나중에,,,
        }

        void TotalSalesText(int amount)   // 총매출
        {
            totalSales += amount;
            totalSalesText.text = $"Total Sales : {totalSales}";

            // TODO : 하루마다 매출 초기화

        }

        void FundText(int totalSales)   // 가게 총 자본
        {
            // 모든 매출 다 더하기

        }

        public void PaymentAmountText(int amount)   // 결제금액
        {
            StartCoroutine(AppearPaymentAmountRoutine(amount));
            TotalSalesText(amount);
            
            PrintOrderSheet();
        }

        IEnumerator AppearPaymentAmountRoutine(int amount)
        {
            paymentAmountText.text = $"amount : {amount}";
            yield return new WaitForSeconds(5f);
            paymentAmountText.text = "";
        }

    }
}
