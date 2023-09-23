using KIM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

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

                switch (other.gameObject.GetComponent<RawFishForCutting>().FishName)
                {
                    case "Salmon":
                        sushi = GameManager.Resource.Instantiate<GameObject>("SalmonSushi");
                        sushi.GetComponent<SushiInfo>().sushiScore = AddSushiScore.currentSushiScore;      // 초밥 점수 반영
                        sushi.GetComponent<SushiInfo>().fishName = other.gameObject.GetComponent<RawFishForCutting>().FishName;
                        sushi.transform.parent = sushiManager.transform;
                        sushi.transform.position = rice.transform.position;
                        break;
                    case "Aji":
                        sushi = GameManager.Resource.Instantiate<GameObject>("AjiSushi");
                        sushi.GetComponent<SushiInfo>().sushiScore = AddSushiScore.currentSushiScore;
                        sushi.GetComponent<SushiInfo>().fishName = other.gameObject.GetComponent<RawFishForCutting>().FishName;
                        sushi.transform.parent = sushiManager.transform;
                        sushi.transform.position = rice.transform.position;
                        break;
                    case "Hirame":
                        sushi = GameManager.Resource.Instantiate<GameObject>("HirameSushi");
                        sushi.GetComponent<SushiInfo>().sushiScore = AddSushiScore.currentSushiScore;
                        sushi.GetComponent<SushiInfo>().fishName = other.gameObject.GetComponent<RawFishForCutting>().FishName;
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