using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public class StoreFish : MonoBehaviour
    {
        Dictionary<string, string> fishInfo = new Dictionary<string, string>();
        private new string name;
        private string weight;
        private string length;
        private string fishType;
        private string fishRank;


        private void Awake()
        {
            
        }

        public void GetFishInfo(Dictionary<string, string> info)
        {
            fishInfo = info;
            name = info["name"];
            weight = info["length"];
            fishType = info["fishType"];
            fishRank = info["fishRank"];
            Debug.Log("StoreFishInfo : " + name + ", " + weight + ", " + fishRank);
        }
    }
}