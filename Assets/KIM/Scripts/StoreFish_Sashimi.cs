using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public class StoreFish_Sashimi : MonoBehaviour
    {
        private string rank;
        public string Rank { get { return rank; } set { rank = value; } }

        public void SetRank(string rank)
        {
            Rank = rank;
        }
        public string ReturnRank()
        {
            return rank;
        }
    }
}
