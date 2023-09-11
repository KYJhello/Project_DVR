using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public class KIM_FishTank : MonoBehaviour
    {
        private List<Dictionary<string, string>> fishList = new List<Dictionary<string, string>>();

        //private void CreateStoreFishes()
        //{
        //    foreach (Dictionary<string, string> fishInfo in fishList)
        //    {
        //        GameObject go = GameManager.Resource.Instantiate<GameObject>("KIM_Prefabs/StoreFish", transform.position + Vector3.up, Quaternion.identity);
        //        go.GetComponent<StoreFish>().GetFishInfo(fishInfo);
        //    }
        //    gameObject.GetComponent<BoxCollider>().enabled = true;
        //}

        private void OnTriggerEnter(Collider other)
        {
            // 만약 충돌한 물체의 레이어가 피쉬박스라면
            if(other.gameObject.layer == 15)
            {
                //gameObject.GetComponent<BoxCollider>().enabled = false;
                if(other.gameObject.GetComponent<FishBox>().GetFishDicList().Count == 0 )
                {
                    return;
                }
                Debug.Log("FishTankUpdated");
                foreach(Dictionary<string,string> fishInfo in other.gameObject.GetComponent<FishBox>().GetFishDicList())
                {
                    fishList.Add(fishInfo);
                    /*GameObject go = */GameManager.Resource.Instantiate<StoreFish>("KIM_Prefabs/StoreFish", transform.position + Vector3.up, Quaternion.identity).GetFishInfo(fishInfo);
                    //go.GetComponent<StoreFish>().GetFishInfo(fishInfo);
                }
                //CreateStoreFishes();
            }
        }
        
    }
}