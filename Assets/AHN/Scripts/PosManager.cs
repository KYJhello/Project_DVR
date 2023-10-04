using JetBrains.Annotations;
using LM;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace AHN
{
    public class PosManager : MonoBehaviour
    {
        public static UnityEvent<int> OnPayEvent = new UnityEvent<int>();     // 결제 금액. EatState에서 호출할 event
        public static UnityEvent<int> OnAddPayEvent = new UnityEvent<int>();  // 누적 점수. EatState에서 호출할 event
        public static UnityEvent OnInitTotalSales = new UnityEvent();   // NextDay로 넘어가면 totalSales 초기화
        // public static UnityEvent OnClickTotalSalesButton = new UnityEvent();  // TotalSales 버튼을 눌렀을 때 호출될 event
        // public static UnityEvent<int> OnClickFundButton = new UnityEvent<int>();    // Fund 버튼을 눌렀을 때 호출될 evnet
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
            fund = 0;
        }

        private void OnEnable()
        {
            OnAddPayEvent.AddListener(PaymentAmountText);
            OnInitTotalSales.AddListener(InitTotalSales);
        }

        void InitTotalSales()   // 다음날로 넘어가면 총 매출 초기화. 이벤트로 넣어서 NextDay에서 호출
        {
            totalSales = 0;
            totalSalesText.text = $"Total Sales : {totalSales}";
        }


        void TotalSalesText(int amount)   // 총 매출
        {
            FundText(amount);   // 자산도 증가
            totalSales += amount;
            totalSalesText.text = $"Total Sales : {totalSales}";
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
            
        }

        IEnumerator AppearPaymentAmountRoutine(int amount)      // 결제금액이 뜨는 기간. 5초 동안만 화면에 뜨도록
        {
            paymentAmountText.text = $"amount : {amount}";
            yield return new WaitForSeconds(5f);
            paymentAmountText.text = "";
        }

    }
}
