using AHN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class NextDay : BaseUI
    {
        [SerializeField] KIM.GameSceneManager gsManager;
        int day = 0;
        int yesterDayFund;
        int todayIncome;
        int totalMoney;
        public int Day {get{return day;}}
        protected override void Awake()
        {
            base.Awake();
            gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            // day = 
            day++;
            yesterDayFund = PosManager.Fund - PosManager.TotalSales;
            todayIncome = PosManager.TotalSales;
            totalMoney = PosManager.Fund;
            texts["Day"].text = $"Day {day.ToString()}";
            texts["YesterdayFund"].text = $"Yesterday Fund  |   {yesterDayFund.ToString()}";
            texts["TodayIncome"].text = $"Today Income    |   +{todayIncome.ToString()}";
            texts["TodayFund"].text = $"Total Fund        |   {totalMoney.ToString()}";
        }
        public void NextDayButtonClicked()
        {
            gsManager.SetPlayerPoint();
            //this.gameObject.SetActive(false);
        }
        
    }
}