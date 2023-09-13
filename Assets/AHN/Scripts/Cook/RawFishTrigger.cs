using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class RawFishTrigger : MonoBehaviour
    {
        [SerializeField] GameObject sushiManager;
        [SerializeField] GameObject rice;
        [SerializeField] GameObject sushi;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 21)   // 회랑 trigger되면 Resources에서 회와 맞는 초밥을 꺼내와서 SushiManager 자식으로 넣기
            {
                other.gameObject.SetActive(false);
                sushi = GameManager.Resource.Instantiate<GameObject>("Sushi");
                sushi.transform.parent = sushiManager.transform;
                sushi.transform.position = rice.transform.position;
                rice.SetActive(false);
            }
        }
    }
}