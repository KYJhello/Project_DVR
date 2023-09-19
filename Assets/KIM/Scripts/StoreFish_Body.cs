using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public class StoreFish_Body : MonoBehaviour
    {
        private string fishRank;
        private string fishName;
        public string FishRank { get { return fishRank; } set { fishRank = value; } }
        public string FishName { get { return fishName; } set { fishName = value; } }

        private void Awake()
        {
            fishRank = GetComponent<StoreFish>().FishRank;
            fishName = GetComponent<StoreFish>().FishName;

        }
        public void SetRank(string rank)
        {
            rank = fishRank;
        }

        public string ReTurnRank()
        {
            return FishRank;
        }

        public string ReTurnName()
        {
            return FishName;
        }
    }
}