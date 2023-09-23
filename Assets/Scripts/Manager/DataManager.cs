using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AHN;

public class DataManager : MonoBehaviour
{
    private int day = 0;
    private int yesterDayFund;
    private int todayIncome;
    private int totalMoney;
    private int level = 0;
    private List<List<string>> fishTankList;
    public int Day { get { return day; } }
    public int Level { get { return level; } }
    public List<List<string>> FishTankList { get { return fishTankList; } }


    public List<int> ReturnDayInfo()
    {
        List<int> dayInfo = new List<int>();

        CalCurDayData();

        dayInfo.Add(day);
        dayInfo.Add(yesterDayFund);
        dayInfo.Add(todayIncome);
        dayInfo.Add(totalMoney);

        return dayInfo;
    }
    public void CalCurDayData()
    {
        day++;
        yesterDayFund = PosManager.Fund - PosManager.TotalSales;
        todayIncome = PosManager.TotalSales;
        totalMoney = PosManager.Fund;
    }
    public void LevelUp()
    {
        level++;
    }
    public void SetFishTankList(List<List<string>> fishList)
    {
        fishTankList = fishList;
    }
}
