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
        public static UnityEvent<int> OnPayEvent = new UnityEvent<int>();     // ���� �ݾ�. EatState���� ȣ���� event
        public static UnityEvent<int> OnAddPayEvent = new UnityEvent<int>();  // ���� ����. EatState���� ȣ���� event
        public static UnityEvent OnInitTotalSales = new UnityEvent();   // NextDay�� �Ѿ�� totalSales �ʱ�ȭ
        // public static UnityEvent OnClickTotalSalesButton = new UnityEvent();  // TotalSales ��ư�� ������ �� ȣ��� event
        // public static UnityEvent<int> OnClickFundButton = new UnityEvent<int>();    // Fund ��ư�� ������ �� ȣ��� evnet
        [SerializeField] TMP_Text paymentAmountText;
        [SerializeField] TMP_Text totalSalesText;
        [SerializeField] TMP_Text fundText;
        [SerializeField] Transform orderSheetPoolPosition;
        GameObject orderSheet;     // �ֹ���

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

        void InitTotalSales()   // �������� �Ѿ�� �� ���� �ʱ�ȭ. �̺�Ʈ�� �־ NextDay���� ȣ��
        {
            totalSales = 0;
            totalSalesText.text = $"Total Sales : {totalSales}";
        }


        void TotalSalesText(int amount)   // �� ����
        {
            FundText(amount);   // �ڻ굵 ����
            totalSales += amount;
            totalSalesText.text = $"Total Sales : {totalSales}";
        }
        
        void FundText(int totalSales)   // �� �ڻ�
        {
            fund += totalSales;
            fundText.text = $"Fund : {fund}";
        }

        public void PaymentAmountText(int amount)   // �����ݾ�
        {
            StartCoroutine(AppearPaymentAmountRoutine(amount));
            TotalSalesText(amount);
            
        }

        IEnumerator AppearPaymentAmountRoutine(int amount)      // �����ݾ��� �ߴ� �Ⱓ. 5�� ���ȸ� ȭ�鿡 �ߵ���
        {
            paymentAmountText.text = $"amount : {amount}";
            yield return new WaitForSeconds(5f);
            paymentAmountText.text = "";
        }

    }
}
