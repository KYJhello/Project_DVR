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
            fishList.Add(fish);
        }
        public void PullFish(int index)
        {
            // TODO : ¹°°í±â ²¨³ÂÀ» ¶§ ¹°°í±â »ý¼º
            // GameManager.Resource.Instantiate<GameObject>("Sea_Fish_" + (fishList[index])[name], transform.position + Vector3.up, Quaternion.identity);


            fishList.RemoveAt(index);
        }

        //private void OnCollisionEnter(Collision other)
        //{
        //    // ¹°°í±â¶û ´êÀ¸¸é
        //    if (other.gameObject.layer == 10)
        //    {
        //        AddFish(other.gameObject.GetComponent<Fish>()?.GetFishInfo());
        //    }
        //    // 
        //}
        private void OnTriggerEnter(Collider other)
        {
            // ¹°°í±â¶û ´êÀ¸¸é
            if (other.gameObject.layer == 10 && other.gameObject.GetComponent<Fish>().GetIsDie())
            {
                AddFish(other.gameObject.GetComponent<Fish>()?.GetFishInfo());
            }
            // 
        }
    }
}