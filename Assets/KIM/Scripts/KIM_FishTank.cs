using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public class KIM_FishTank : MonoBehaviour
    {
        private List<Dictionary<string, string>> fishList = new List<Dictionary<string, string>>();

        private void Awake()
        {
        }
        private void CreateStoreFishes()
        {
            foreach (Dictionary<string, string> fishInfo in fishList)
            {
                GameObject go = GameManager.Resource.Instantiate<GameObject>("KIM_Prefabs/StoreFish", transform.position + Vector3.up, Quaternion.identity);
                go.GetComponent<StoreFish>().GetFishInfo(fishInfo);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // ���� �浹�� ��ü�� ���̾ �ǽ��ڽ����
            if(other.gameObject.layer == 15)
            {
                if(other.gameObject.GetComponent<FishBox>().GetFishDicList().Count == 0)
                {
                    return;
                }
                Debug.Log("FishTankUpdated");
                foreach(Dictionary<string,string> fishInfo in other.gameObject.GetComponent<FishBox>().GetFishDicList())
                {
                    fishList.Add(fishInfo);
                }
                CreateStoreFishes();
            }
        }
    }
}