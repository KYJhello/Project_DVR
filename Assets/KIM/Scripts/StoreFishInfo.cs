using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public class StoreFishInfo : MonoBehaviour
    {
        List<string> fishInfo = new List<string>();
        private new string name;
        private string weight;
        private string length;
        private string fishRank;

        public string FishName { get { return name; } set { name = value; } }
        public string Weight { get { return weight; } set { weight = value; } }
        public string Length { get { return length; } set { length = value; } }
        public string FishRank { get { return fishRank; } set { fishRank = value; } }

        StoreFish_Body body_Info;
        
        public void SetFishInfo(List<string> info)
        {
            fishInfo = info;
            name = info[0];
            weight = info[1];
            length = info[2];
            fishRank = info[3];
            Debug.Log("StoreFishInfo : " + name + ", " + weight + ", " + fishRank);
            Debug.Log($"{gameObject.GetComponentInChildren<StoreFish_Body>()}");
            SetBodyInfo();
        }

        public void SetBodyInfo()
        {
            body_Info = gameObject.GetComponentInChildren<StoreFish_Body>();
            body_Info.fishName = name;
            body_Info.fishRank = fishRank;
        }
        public string ReturnFishRank()
        {
            return fishRank;
        }
    }
}