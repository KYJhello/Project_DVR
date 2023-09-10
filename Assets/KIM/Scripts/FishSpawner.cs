using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace KIM
{
    public class FishSpawner : MonoBehaviour
    {
        [SerializeField]
        private int spawnNum;
        // TODO : MinY, Height ==> 나중에 maxY와 Depth로 바꿔야함
        [SerializeField]
        private float seaMinY;
        [SerializeField]
        private float seaHeight;
        [SerializeField]
        private float seaWidth;
        [SerializeField]
        private float playerWidth;


        private void Awake()
        { 
            // spawnNum만큼 프리팹 생성
            for(int i = 0; i < spawnNum; i++) {
                GameManager.Resource.Instantiate<GameObject>("KIM_Prefabs/Fish", new Vector3(randPM() * Random.Range(playerWidth, seaWidth), Random.Range(seaMinY, seaHeight),randPM() * Random.Range(playerWidth,seaWidth)), Quaternion.identity);
            }
        }
        private int randPM()
        {
            int num;
            num = Random.Range(-1, 1);
            if(num != 0)
            {
                return num;
            }
            return 1;
        }
    }
}