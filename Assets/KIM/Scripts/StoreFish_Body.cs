using Jeon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public class StoreFish_Body : MonoBehaviour
    {
        [SerializeField] private string fishRank;
        [SerializeField] private string fishName;

        public Jeon.StoreFish storeFish;

        public bool isHeadCutting;
        public bool isTailCutting;

        Transform fishBodyPos;
        Quaternion quaternion;

        private void Awake()
        {
            storeFish = gameObject.transform.parent.GetComponent<Jeon.StoreFish>();
            fishRank = gameObject.transform.parent.GetComponent<StoreFishInfo>().FishRank;
            fishName = gameObject.transform.parent.GetComponent<StoreFishInfo>().FishName;
            fishBodyPos = gameObject.transform;
        }

        private void Update()
        {
            if (isHeadCutting == true && isTailCutting == true)
            {
                Debug.Log(fishName);
                InstanteFishBodyPrefab(fishName);
            }
        }
        public void FishCuttingReset()
        {
            isHeadCutting = false;
            isTailCutting = false;
        }
        public void InstanteFishBodyPrefab(string fishName)
        {
            quaternion = Quaternion.Euler(0, -90, 0);
            if (fishName == "Salmon")
            {
                GameObject salmonBodyMeat = GameManager.Resource.Instantiate<GameObject>("Jeon_Prefab/Salmon_Body_Meat", fishBodyPos.position, quaternion, false);
                salmonBodyMeat.GetComponent<FishBodyMeat>().fishName = this.fishName;
                salmonBodyMeat.GetComponent<FishBodyMeat>().fishTier = fishRank;
            }
            else if (fishName == "Hirame")
            {
                GameObject hirameBodyMeat = GameManager.Resource.Instantiate<GameObject>("Jeon_Prefab/Hirame_Body_Meat", fishBodyPos.position, quaternion, false);
                hirameBodyMeat.GetComponent<FishBodyMeat>().fishName = this.fishName;
                hirameBodyMeat.GetComponent<FishBodyMeat>().fishTier = fishRank;
            }
            else if (fishName == "Aji")
            {
                GameObject ajiBodyMeat = GameManager.Resource.Instantiate<GameObject>("Jeon_Prefab/Aji_Body_Meat", fishBodyPos.position, quaternion, false);
                ajiBodyMeat.GetComponent<FishBodyMeat>().fishName = this.fishName;
                ajiBodyMeat.GetComponent<FishBodyMeat>().fishTier = fishRank;
            }


            Destroy(gameObject);
            FishCuttingReset();
        }
    }
}