using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KIM
{
    public class FishBox : MonoBehaviour
    {
        public UnityEvent<int> OnPullRequest;

        List<Dictionary<string, string>> fishList;
        List<string> fishKeys;

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
            // TODO : 물고기 꺼냈을 때 물고기 생성
            // GameManager.Resource.Instantiate<GameObject>("Sea_Fish_" + (fishList[index])[name], transform.position + Vector3.up, Quaternion.identity);


            fishList.RemoveAt(index);
        }

        private void OnTriggerEnter(Collider other)
        {
            // 물고기랑 닿으면
            if (other.gameObject.layer == 10)
            {
                if (other.gameObject.GetComponent<Fish>().enabled == false)
                {
                    // TODO 피쉬에서 정보 보내는 함수 만들어서 실행시키기
                    //AddFish(other.gameObject.GetComponent<Fish>());
                }
            }
            // 
        }
    }
}