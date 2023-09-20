using AHN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class GameEnd : BaseUI
    {
        public int goalFund;
        protected override void Awake()
        {
            base.Awake();
            gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            texts["GoalFund"].text = $"Goal Fund : {goalFund}";
            texts["TotalFund"].text = $"Total Fund : {PosManager.Fund}";
            if(goalFund <= PosManager.Fund)
            {
                images["Clear"].gameObject.SetActive(true);
            }
            else
            {
                images["Fail"].gameObject.SetActive(true);
            }
        }
    }
}