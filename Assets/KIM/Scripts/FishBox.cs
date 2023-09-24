using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KIM
{
    public class FishBox : MonoBehaviour
    {
        // fish list <fishInfo> 
        // fishInfo = name = 0, weight = 1, length = 2, FishRank = 3
        public List<List<string>> fishList = new List<List<string>>();
        private float totalWeight = 0f;

        public void AddFish(List<string> info)
        {
            //StopAllCoroutines();
            fishList.Add(info);
            AddWeight(float.Parse(info[1]));
            Debug.Log("FishBox TotalWeight : " +  totalWeight);
        }
        public void PullFish(int index)
        {
            // TODO : 물고기 꺼냈을 때 물고기 생성
            // GameManager.Resource.Instantiate<GameObject>("Sea_Fish_" + (fishList[index])[name], transform.position + Vector3.up, Quaternion.identity);
            fishList.RemoveAt(index);
        }
        private void AddWeight(float input)
        {
            totalWeight += input;
        }
        private void SubWeight(float input)
        {
            totalWeight -= input;
        }
        public float ReturnCurWeight()
        {
            return totalWeight;
        }
        // 수족관에서 호출하는 함수
        public List<List<string>> GetFishList()
        {
            List<List<string>> tempFishList = new List<List<string>>();
            foreach(List<string> fishInfo in fishList)
            {
                tempFishList.Add(fishInfo);
            }
            fishList.Clear();
            totalWeight = 0f;
            fishList = new List<List<string>>();
            return tempFishList;
        }
        public List<List<string>> ReturnFishBoxFishList()
        {
            return fishList;
        }


        // 1) 가장 먼저 실행됨
        private void OnTriggerEnter(Collider other)
        {
            // 죽은 물고기랑 닿으면
            if (other.gameObject.layer == 10)
            {
                if (other.gameObject.GetComponent<Fish>().GetIsDie())
                {
                    AddFish(other.gameObject.GetComponent<Fish>()?.GetFishInfo());
                }
            }
            // 
        }
    }
}