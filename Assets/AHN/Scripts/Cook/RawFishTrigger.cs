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
                //1. 접시 위에 있는 FishRank를 받아와야함. Normal, Rare, SuperRare, Special 4가지로 나눠짐.
                // Normal = 1000원, Rare = 1500원, SuperRare = 2000원, Special = 2500원
                switch (other.gameObject.GetComponent<RawFishForCutting>().FishTier)
                {
                    case "Normal":
                        AddSushiScore.currentSushiScore += 1000;
                        break;
                    case "Rare":
                        AddSushiScore.currentSushiScore += 1500;
                        break;
                    case "SuperRare":
                        AddSushiScore.currentSushiScore += 2000;
                        break;
                    case "Special":
                        AddSushiScore.currentSushiScore += 2500;
                        break;
                    default:
                        AddSushiScore.currentSushiScore += 5;
                        break;
                }
                GameObject sushi;

                Destroy(other.gameObject);

                switch (other.gameObject.name)
                {
                    // TODO : 회에 붙어있는 RawFishForCutting에서 Fish등급말고 FishName을 불러와서 그 이름을 가지고 case 하면 될듯.
                    // switch (other.gameObject.GetComponent<RawFishForCutting>().이름불러오는 거)

                    case "SalmonSashimi":
                        sushi = GameManager.Resource.Instantiate<GameObject>("SalmonSushi");
                        sushi.GetComponent<SushiScore>().sushiScore = AddSushiScore.currentSushiScore;      // 초밥 점수 반영
                        sushi.transform.parent = sushiManager.transform;
                        sushi.transform.position = rice.transform.position;
                        break;
                    case "ASashimi":
                        sushi = GameManager.Resource.Instantiate<GameObject>("ASushi");
                        sushi.GetComponent<SushiScore>().sushiScore = AddSushiScore.currentSushiScore;
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