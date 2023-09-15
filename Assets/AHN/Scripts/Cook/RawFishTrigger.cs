using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class RawFishTrigger : MonoBehaviour
    {
        [SerializeField] GameObject sushiManager;
        [SerializeField] GameObject rice;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 21)   // 회랑 trigger되면 Resources에서 회와 맞는 초밥을 꺼내와서 SushiManager 자식으로 넣기
            {
                // TODO : 회랑 trigger 되면, 그리고 회의 방향과 밥의 방향과 같다면 밑 내용들 진행.
                // if (other.gameObject.transform.p)
                
                Destroy(other.gameObject);
                GameObject sushi = GameManager.Resource.Instantiate<GameObject>("Sushi");
                sushi.transform.parent = sushiManager.transform;
                sushi.transform.position = rice.transform.position;
                Destroy(rice);
            }
        }
    }
}