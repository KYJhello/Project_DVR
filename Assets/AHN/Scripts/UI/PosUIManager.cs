using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class PosUIManager : InGameUI
    {
        [SerializeField] GameObject mainScreen;
        [SerializeField] GameObject TotalSalesScreen;
        [SerializeField] GameObject FundScreen;
        PosManager posManager;
        int a = 0;

        private void Start()
        {
            mainScreen.SetActive(true);
            TotalSalesScreen.SetActive(false);
            FundScreen.SetActive(false);
        }

        public void TotalSalesButton()
        {
            Debug.Log("TotalSalesButton");
            mainScreen.SetActive(false);
            TotalSalesScreen.SetActive(true);
            
            // TODO : 총매출이 나와야 함 -> 유니티 이벤트로 할 필요없이 프로퍼티로 만들어놨으니까 그 값을 가져와서 text로 보여주기만 하면 됨.
        }


        public void FundButton()
        {
            Debug.Log("FundButton");
            mainScreen.SetActive(false);
            FundScreen.SetActive(true);
            // TODO : 가게의 총 자본금이 나와야 함
        }

        public void BackButton()
        {
            // 다시 MainScreen 뜨도록
            FundScreen.SetActive(false);
            TotalSalesScreen.SetActive(false);
            mainScreen.SetActive(true);
        }
    }
}