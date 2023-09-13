using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public class StoreFish : MonoBehaviour
    {
        List<string> fishInfo = new List<string>();
        private new string name;
        private string weight;
        private string length;
        private string fishRank;
        private FishBody body;
        private void Awake()
        {
            body = GetComponentInChildren<FishBody>();
        }

        public void SetFishInfo(List<string> info)
        {
            fishInfo = info;
            name = info[0];
            weight = info[1];
            length = info[2];
            fishRank = info[3];
            Debug.Log("StoreFishInfo : " + name + ", " + weight + ", " + fishRank);
            body.Rank = fishRank;
        }
    }
}