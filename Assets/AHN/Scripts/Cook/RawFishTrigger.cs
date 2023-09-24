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
            if (other.gameObject.layer == 21)   // ȸ�� trigger�Ǹ� Resources���� ȸ�� �´� �ʹ��� �����ͼ� SushiManager �ڽ����� �ֱ�
            {
                //1. ���� ���� �ִ� FishRank�� �޾ƿ;���. Normal, Rare, SuperRare, Special 4������ ������.
                // Normal = 1000��, Rare = 1500��, SuperRare = 2000��, Special = 2500��
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
                        sushi.GetComponent<SushiInfo>().sushiScore = AddSushiScore.currentSushiScore;      // �ʹ� ���� �ݿ�
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