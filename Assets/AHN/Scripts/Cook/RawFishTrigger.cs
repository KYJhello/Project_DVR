using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace AHN
{
    public class RawFishTrigger : MonoBehaviour
    {
        [SerializeField] GameObject sushiManager;
        [SerializeField] GameObject rice;
        public int sushiScore;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 21)   // 회랑 trigger되면 Resources에서 회와 맞는 초밥을 꺼내와서 SushiManager 자식으로 넣기
            {
                // TODO : 올려진 이 회의 등급을 받아옴 (회에 스크립트로 등급 점수를 반환하는 함수가 있을 거임. 
                //        other.gameObject.GetComponent<>() 로 등급을 받아오고, 이 등급을 변하는 Sushi로 넘겨줘야 함.
                //        그건 여기 밑에 sushi.gameObject.GetComponent<>()로, Sushi에서 등급을 매개변수로 하는 점수계산 함수를 만들어. 그 함수를 등급을 넣어서 호출하면됨.
                //        그러면 만들어진 sushi에는 등급이 반영된 점수가 있을 것.
                

                //1. 접시 위에 있는 FishRank를 받아와야함. Normal, Rare, SuperRare, Special 4가지로 나눠짐.
                // Normal = 1000원, Rare = 1500원, SuperRare = 2000원, Special = 2500원
                switch (other.gameObject.GetComponent<RawFishForCutting>().FishTier)
                {
                    case "Normal":
                        sushiScore = 1000;
                        break;
                    case "Rare":
                        sushiScore = 1500;
                        break;
                    case "SuperRare":
                        sushiScore = 2000;
                        break;
                    case "Special":
                        sushiScore = 2500;
                        break;
                    default:
                        sushiScore = 5;
                        break;
                }


                GameObject sushi;

                Destroy(other.gameObject);

                switch (other.gameObject.name)
                {
                    case "SalmonSashimi":
                        sushi = GameManager.Resource.Instantiate<GameObject>("SalmonSushi");
                        sushi.GetComponent<SushiScore>().sushiScore = sushiScore;
                        sushi.transform.parent = sushiManager.transform;
                        sushi.transform.position = rice.transform.position;
                        break;
                    case "ASashimi":
                        sushi = GameManager.Resource.Instantiate<GameObject>("ASushi");
                        sushi.GetComponent<SushiScore>().sushiScore = sushiScore;
                        sushi.transform.parent = sushiManager.transform;
                        sushi.transform.position = rice.transform.position;
                        break;
                    default:
                        break;
                }


                Destroy(rice);
            }
        }
    }
}