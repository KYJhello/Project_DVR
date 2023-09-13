using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public class KIM_FishTank : MonoBehaviour
    {
        private List<Dictionary<string, string>> fishList/* = new List<Dictionary<string, string>>()*/;
        private bool isCreating = false;

        private void OnTriggerEnter(Collider other)
        {
            // 만약 충돌한 물체의 레이어가 피쉬박스라면
            if(other.gameObject.layer == 15 && !isCreating)
            {
                if(other.gameObject.GetComponent<FishBox>().GetFishDicList().Count <= 0) { return; }
                isCreating = true;
                fishList = new List<Dictionary<string, string>>();
                StartCoroutine(CreateFishRoutine(other));
            }
        }
        IEnumerator CreateFishRoutine(Collider other)
        {
            while (true)
            {
                foreach (Dictionary<string, string> fishInfo in other.gameObject.GetComponent<FishBox>().GetFishDicList())
                {
                    fishList.Add(fishInfo);
                    /*GameObject go = */
                }
                foreach(Dictionary<string,string> fishInfo in fishList)
                {
                    GameManager.Resource.Instantiate<StoreFish>("KIM_Prefabs/StoreFish", transform.position + Vector3.up, Quaternion.identity).GetFishInfo(fishInfo);
                    yield return new WaitForSeconds(0.2f);
                }
                isCreating = false;
                fishList.Clear();
                yield return null;
            }
        }
        
    }
}