using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KIM
{
    public class FishSpawner : MonoBehaviour
    {
        [SerializeField]
        private int spawnNum;
        [SerializeField]
        private bool isStore;

        private void Awake()
        {
            for(int i = 0; i < spawnNum; i++) {
                GameManager.Resource.Instantiate<GameObject>("KIM_Prefabs/Fish", transform.position, Quaternion.identity);
            }
        }
    }
}