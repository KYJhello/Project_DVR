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
                // TODO : 회에 맞는 초밥으로 바뀔 수 있어야함. "Sushi" 부분을 변수로 놔야할듯. Switch case 문으로
                // TODO : 올려진 이 회의 등급을 받아옴 (회에 스크립트로 등급 점수를 반환하는 함수가 있을 거임. 
                //        other.gameObject.GetComponent<>() 로 등급을 받아오고, 이 등급을 변하는 Sushi로 넘겨줘야 함.
                //        그건 여기 밑에 sushi.gameObject.GetComponent<>()로, Sushi에서 등급을 매개변수로 하는 점수계산 함수를 만들어. 그 함수를 등급을 넣어서 호출하면됨.
                //        그러면 만들어진 sushi에는 등급이 반영된 점수가 있을 것.
                
                Destroy(other.gameObject);
                GameObject sushi = GameManager.Resource.Instantiate<GameObject>("Sushi");
                sushi.transform.parent = sushiManager.transform;
                sushi.transform.position = rice.transform.position;
                Destroy(rice);
            }
        }
    }
}