using KIM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public enum Menu { Steak, Sushi }   

    public static List<List<string>> fishs = new List<List<string>>();    // 수족관에 있는 물고기들

    public static List<List<string>> sasimiCounts = new List<List<string>>();   // 손질 후 남은 회 갯수
    // 물고기의 종류가 변동되므로 sasimiCounts 를 List로 둬서, {SalmonSushi,1}, {ASushi,4} 이런식으로 함


    private void Start()
    {
        // fish list <fishInfo> 
        // fishInfo = name = 0, weight = 1, length = 2, FishRank = 3
        fishs = GameObject.FindObjectOfType<KIM_FishTank>().ReturnFishTankFishList();   // 수족관에 있는 물고기들 정보를 받아옴
    
        // sasimiCounts 에 현재 물고기들 이름을 [0]에 다 넣고 [1]에는 0으로 만듦
        List<string> countInit = new List<string>();

        foreach (List<string> currentFishs in fishs)
        {
            // if (중복 이름이 있다면 continue)

            string fishName = currentFishs[0];     // 첫번째 물고기 name
            countInit.Add(fishName);
            countInit.Add("0");

            List<string> list = new List<string>();
            list = countInit;
            
            sasimiCounts.Add(list);     // { 물고기 이름, 0 } .. . .. 의 정보들이 있는 리스트
        }
    }
}
