using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public class KIM_FishTank : MonoBehaviour
    {
        // fish list <fishInfo> 
        // fishInfo = name = 0, weight = 1, length = 2, FishRank = 3

        private List<List<string>> fishList = new List<List<string>>();
        private bool isCreating = false;

        private void OnTriggerEnter(Collider other)
        {
            // 만약 충돌한 물체의 레이어가 피쉬박스라면
            if(other.gameObject.layer == 15 && !isCreating)
            {
                if(other.gameObject.GetComponent<FishBox>().GetFishList().Count <= 0) { return; }
                isCreating = true;
                fishList = new List<List<string>>();
                StartCoroutine(CreateFishRoutine(other));
            }
        }
        public List<List<string>> ReturnFishTankFishList()
        {
            return fishList;
        }

        IEnumerator CreateFishRoutine(Collider other)
        {
            while (true)
            {
                foreach (List<string> fishInfo in other.gameObject.GetComponent<FishBox>().GetFishList())
                {
                    fishList.Add(fishInfo);
                }
                foreach(List<string> fishInfo in fishList)
                {
                    GameManager.Resource.Instantiate<StoreFish>("KIM_Prefabs/StoreFish", transform.position + Vector3.up, Quaternion.identity).SetFishInfo(fishInfo);
                    yield return new WaitForSeconds(0.2f);
                }
                isCreating = false;
                fishList.Clear();
                yield return null;
            }
        }
        
    }
}