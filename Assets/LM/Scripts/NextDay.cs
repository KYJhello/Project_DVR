using AHN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class NextDay : BaseUI
    {
        [SerializeField] KIM.GameSceneManager gsManager;
        CustomerSqawnManager customerSqawn;

        protected override void Awake()
        {
            base.Awake();
            gameObject.SetActive(false);
        }

        private void Start()
        {
            customerSqawn = GameObject.Find("CustomerSpawnPoint").GetComponent<AHN.CustomerSqawnManager>();
        }

        private void OnEnable()
        {
            List<int> dayInfo = GameManager.Data.ReturnDayInfo();
            texts["Day"].text = $"Day {dayInfo[0].ToString()}";
            texts["YesterdayFund"].text = $"Yesterday Fund  |   {dayInfo[1].ToString()}";
            texts["TodayIncome"].text = $"Today Income    |   +{dayInfo[2].ToString()}";
            texts["TodayFund"].text = $"Total Fund        |   {dayInfo[3].ToString()}";
        }
        public void NextDayButtonClicked()
        {
            gsManager.ReloadScene();
            StopCoroutine(customerSqawn.CustomerSpawnRoutine());
            //this.gameObject.SetActive(false);
        }
    }
}