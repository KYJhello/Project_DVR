using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KIM
{
    public class FishBox : MonoBehaviour
    {
        public List<Dictionary<string, string>> fishList = new List<Dictionary<string, string>>();
        List<string> fishKeys = new List<string>();
        private float totalWeight = 0f;

        private void Awake()
        {
            fishKeys.Add("name");
            fishKeys.Add("weight");
            fishKeys.Add("length");
            fishKeys.Add("fishType");
            fishKeys.Add("fishRank");
        }

        public void AddFish(List<string> info)
        {
            Dictionary<string, string> fish = new Dictionary<string, string>();
            for (int i = 0; i < fishKeys.Count; i++)
            {
                fish.Add(fishKeys[i], info[i]);
            }
            AddWeight(float.Parse(fish["weight"]));;
            fishList.Add(fish);
            Debug.Log(totalWeight);
        }
        public void PullFish(int index)
        {
            // TODO : ¹°°í±â ²¨³ÂÀ» ¶§ ¹°°í±â »ý¼º
            // GameManager.Resource.Instantiate<GameObject>("Sea_Fish_" + (fishList[index])[name], transform.position + Vector3.up, Quaternion.identity);
            SubWeight(float.Parse((fishList[index])["weight"]));
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
        public List<Dictionary<string, string>> GetFishDicList()
        {
            StartCoroutine(DeleteFishListRoutine());
            return fishList;
        }
        IEnumerator DeleteFishListRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.05f);
                fishList.Clear();
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