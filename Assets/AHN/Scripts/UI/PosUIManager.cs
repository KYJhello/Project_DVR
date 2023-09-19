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

        private void Start()
        {
            mainScreen.SetActive(true);
            TotalSalesScreen.SetActive(false);
            FundScreen.SetActive(false);
        }

        public void TotalSalesButton()
        {
            mainScreen.SetActive(false);
            TotalSalesScreen.SetActive(true);
        }


        public void FundButton()
        {
            mainScreen.SetActive(false);
            FundScreen.SetActive(true);
        }

        public void BackButton()
        {
            // ´Ù½Ã MainScreen ¶ßµµ·Ï
            FundScreen.SetActive(false);
            TotalSalesScreen.SetActive(false);
            mainScreen.SetActive(true);
        }
    }
}