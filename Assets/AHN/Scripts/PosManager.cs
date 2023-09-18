using JetBrains.Annotations;
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
        public static UnityEvent OnClickTotalSalesButton = new UnityEvent();  // TotalSales 버튼을 눌렀을 때 호출될 event
        public static UnityEvent<int> OnClickFundButton = new UnityEvent<int>();    // Fund 버튼을 눌렀을 때 호출될 evnet
        [SerializeField] TMP_Text paymentAmountText;
        [SerializeField] TMP_Text totalSalesText;
        [SerializeField] TMP_Text fundText;
        [SerializeField] Transform orderSheetPoolPosition;
        GameObject orderSheet;     // 주문서
        private static int totalSales;
        public static int TotalSales { get { return totalSales; } set { totalSales = value; } }
        private static int fund;
        public static int Fund { get {  return fund; } set {  fund = value; } }

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

        public void PrintOrderSheet()   // 주문서 출력하는 함수
        {
            orderSheet = GameManager.Pool.Get(orderSheet, orderSheetPoolPosition.position, Quaternion.identity);

            // TODO : 주문서 나중에 Release 해줘야 하는데 그건 나중에,,,
        }

        void TotalSalesText(int amount)   // 총 매출
        {
            totalSales += amount;
            totalSalesText.text = $"Total Sales : {totalSales}";
            FundText(totalSales);   // 자산도 증가

            // TODO : 하루마다 매출 초기화 -> 타이머 누르면 초기화 되도록.
        }
        
        void FundText(int totalSales)   // 총 자산
        {
            fund += totalSales;
            fundText.text = $"Fund : {fund}";
        }

        public void PaymentAmountText(int amount)   // 결제금액
        {
            StartCoroutine(AppearPaymentAmountRoutine(amount));
            TotalSalesText(amount);
            
            PrintOrderSheet();
        }

        IEnumerator AppearPaymentAmountRoutine(int amount)      // 결제금액이 뜨는 기간. 5초 동안만 화면에 뜨도록
        {
            paymentAmountText.text = $"amount : {amount}";
            yield return new WaitForSeconds(5f);
            paymentAmountText.text = "";
        }

    }
}
