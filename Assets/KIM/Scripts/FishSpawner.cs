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
        // TODO : MinY, Height ==> ���߿� maxY�� Depth�� �ٲ����
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
            // spawnNum��ŭ ������ ����
            for(int i = 0; i < spawnNum/3; i++) {
                GameManager.Resource.Instantiate<Fish>("KIM_Prefabs/SeaFish/Salmon", transform.parent.position + Vector3.down*199.5f + new Vector3(randPM() * Random.Range(playerWidth, seaWidth), Random.Range(seaMinY, seaHeight),randPM() * Random.Range(playerWidth,seaWidth)), Quaternion.identity, transform.parent, true);
            }
            for (int i = 0; i < spawnNum/3; i++)
            {
                GameManager.Resource.Instantiate<Fish>("KIM_Prefabs/SeaFish/Aji", transform.parent.position + Vector3.down * 199.5f + new Vector3(randPM() * Random.Range(playerWidth, seaWidth), Random.Range(seaMinY, seaHeight), randPM() * Random.Range(playerWidth, seaWidth)), Quaternion.identity, transform.parent, true);
            }
            for (int i = 0; i < spawnNum / 3; i++)
            {
                GameManager.Resource.Instantiate<Fish>("KIM_Prefabs/SeaFish/Hirame", transform.parent.position + Vector3.down * 199.5f + new Vector3(randPM() * Random.Range(playerWidth, seaWidth), Random.Range(seaMinY, seaMinY+10f), randPM() * Random.Range(playerWidth, seaWidth)), Quaternion.identity, transform.parent, true);
            }

            GameManager.Resource.Instantiate<Fish>("KIM_Prefabs/SeaFish/Shark", transform.parent.position + Vector3.down * 199.5f + new Vector3(randPM() * Random.Range(playerWidth, seaWidth), seaMinY, randPM() * Random.Range(playerWidth, seaWidth)), Quaternion.identity, transform.parent, true);

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