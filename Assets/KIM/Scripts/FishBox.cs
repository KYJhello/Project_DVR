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
        private List<List<string>> fishList = new List<List<string>>();
        private float totalWeight = 0f;

        public void AddFish(List<string> info)
        {
            StopAllCoroutines();
            fishList.Add(info);
            AddWeight(float.Parse(info[1]));
            Debug.Log("FishBox TotalWeight : " +  totalWeight);
        }
        public void PullFish(int index)
        {
            // TODO : ¹°°í±â ²¨³ÂÀ» ¶§ ¹°°í±â »ý¼º
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
        public List<List<string>> GetFishList()
        {
            StartCoroutine(DeleteFishListRoutine());
            return fishList;
        }
        public List<List<string>> ReturnFishBoxFishList()
        {
            return fishList;
        }
        IEnumerator DeleteFishListRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.05f);
                fishList.Clear();
                totalWeight = 0f;
                fishList = new List<List<string>>();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            // Á×Àº ¹°°í±â¶û ´êÀ¸¸é
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