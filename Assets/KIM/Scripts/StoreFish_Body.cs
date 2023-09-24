using Jeon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public class StoreFish_Body : MonoBehaviour
    {
        public string fishRank;
        public string fishName;

        public Jeon.StoreFish storeFish;

        public bool isHeadCutting;
        public bool isTailCutting;

        Transform fishBodyPos;
        Quaternion quaternion;

        private void Awake()
        {
            storeFish = gameObject.transform.parent.GetComponent<Jeon.StoreFish>();
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
        public void InstanteFishBodyPrefab(string name)
        {
            quaternion = Quaternion.Euler(0, -90, 0);

            if (name == "Salmon")
            {
                GameObject salmonBodyMeat = GameManager.Resource.Instantiate<GameObject>("Jeon_Prefab/Salmon_Body_Meat", fishBodyPos.position, quaternion, false);
                salmonBodyMeat.GetComponent<FishBodyMeat>().fishName = fishName;
                salmonBodyMeat.GetComponent<FishBodyMeat>().fishTier = fishRank;
            }
            else if (name == "Hirame")
            {
                GameObject hirameBodyMeat = GameManager.Resource.Instantiate<GameObject>("Jeon_Prefab/Hirame_Body_Meat", fishBodyPos.position, quaternion, false);
                hirameBodyMeat.GetComponent<FishBodyMeat>().fishName = fishName;
                hirameBodyMeat.GetComponent<FishBodyMeat>().fishTier = fishRank;
            }
            else if (name == "Aji")
            {
                GameObject ajiBodyMeat = GameManager.Resource.Instantiate<GameObject>("Jeon_Prefab/Aji_Body_Meat", fishBodyPos.position, quaternion, false);
                ajiBodyMeat.GetComponent<FishBodyMeat>().fishName = fishName;
                ajiBodyMeat.GetComponent<FishBodyMeat>().fishTier = fishRank;
            }


            Destroy(gameObject);
            FishCuttingReset();
        }
    }
}