using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KIM
{
    public class GameSceneManager : MonoBehaviour
    {
        [SerializeField] GameObject player;
        [SerializeField] GameObject restaurantObjects;
        [SerializeField] GameObject seaObjects;

        public void SetPlayerPoint()
        {
            player.transform.position = this.transform.position;
            //GameObject.Destroy(restaurantObjects);
            //GameObject.Destroy(seaObjects);
            //GameManager.Resource.Instantiate<GameObject>("KIM_Prefabs/ReCreatable_Rest");
            //GameManager.Resource.Instantiate<GameObject>("KIM_Prefabs/BoatPrefabs");
        }
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}                     