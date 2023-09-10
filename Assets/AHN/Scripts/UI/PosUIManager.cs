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

        private void Start()
        {
            mainScreen.SetActive(true);
            TotalSalesScreen.SetActive(false);
            FundScreen.SetActive(false);
        }

        public void TotalSalesButton()
        {
            Debug.Log("TotalSalesButton");
            // TODO : 총매출이 나와야 함
            mainScreen.SetActive(false);
            TotalSalesScreen.SetActive(true);
        }


        public void FundButton()
        {
            // TODO : 가게의 총 자본금이 나와야 함
            // 1. 현재 오브젝트 false
            mainScreen.SetActive(false);
            FundScreen.SetActive(true);
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