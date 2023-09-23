using Jeon;
using KIM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// 1번째 분리
public class FishBodyMeat : MonoBehaviour
{
    // TODO : 분리되는 부분 가져와서 스크립트 분리하기
    public float x = -0.01f;
    public float timer = 0;

    public string fishName;
    public string fishTier;

    public bool firstHeadHit = false;

    Coroutine check;

    KnifeRay kinfeRay;

    GameObject fishPrefab;

    private void Awake()
    {
        kinfeRay = GameObject.Find("KnifeRayCast").GetComponent<KnifeRay>();
    }

    public void ChackTimer()
    {
        if (timer < 2)
        {
            for (int i = 0; i < 2; i++)
            {
                if (fishName == "Salmon")
                {
                    fishPrefab = GameManager.Resource.Instantiate<GameObject>("Jeon_Prefab/SalmonMeat", kinfeRay.hitInfoPos, Quaternion.identity);
                }
                else if (fishName == "Hirame")
                {
                    fishPrefab = GameManager.Resource.Instantiate<GameObject>("Jeon_Prefab/HirameMeat", kinfeRay.hitInfoPos, Quaternion.identity);
                }
                else if (fishName == "Aji")
                {
                    fishPrefab = GameManager.Resource.Instantiate<GameObject>("Jeon_Prefab/AjiMeat", kinfeRay.hitInfoPos, Quaternion.identity);
                }
            }
            TakeFishInfo();

            Destroy(kinfeRay.fish.transform.parent.gameObject);

            StopCoroutine(check);
            timer = 0;
        }
        else if (timer >= 2)
        {

            timer = 0;
            StopCoroutine(check);
        }
    }
    private void TakeFishInfo()
    {
        fishPrefab.GetComponent<RawSalmon>().FishTier = fishTier;
        fishPrefab.GetComponent<RawSalmon>().FishName = fishName;
    }

    public void CuttingFish()
    {
        if (!firstHeadHit)
            return;

        if (kinfeRay.collisionNormal.x <= x)
        {
            Debug.Log("쭉 진행중");

            check = StartCoroutine(CheckSecondHitTime());
        }

    }

    public void TakePrefab()
    {
        if (timer < 5)
        {
            // 프리팹을 가져옵니다
            for (int i = 0; i < 2; i++)
            {
                GameManager.Resource.Instantiate<GameObject>("Jeon_Prefab/FishMeat", kinfeRay.hitInfoPos, Quaternion.identity);
            }
            Debug.Log($"{kinfeRay.fish.transform.parent.name}");

            Destroy(kinfeRay.fish.transform.parent.gameObject);

            StopCoroutine(check);
            timer = 0;
        }
        else if (timer >= 5)
        {
            // 다시 자르도록 시간을 0초로 맞춰주고 코루틴은 정지
            timer = 0;
            StopCoroutine(check);
        }
    }

    IEnumerator CheckSecondHitTime()
    {
        // 시간마다 타임을 증가
        while (true)
        {
            timer += Time.deltaTime;
            Debug.Log($"{timer}");
            yield return null;
        }
    }
}
