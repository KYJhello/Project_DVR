using Jeon;
using KIM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// 1��° �и�
public class FishBodyMeat : MonoBehaviour
{
    // TODO : �и��Ǵ� �κ� �����ͼ� ��ũ��Ʈ �и��ϱ�
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
            Debug.Log("�� ������");

            check = StartCoroutine(CheckSecondHitTime());
        }

    }

    public void TakePrefab()
    {
        if (timer < 5)
        {
            // �������� �����ɴϴ�
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
            // �ٽ� �ڸ����� �ð��� 0�ʷ� �����ְ� �ڷ�ƾ�� ����
            timer = 0;
            StopCoroutine(check);
        }
    }

    IEnumerator CheckSecondHitTime()
    {
        // �ð����� Ÿ���� ����
        while (true)
        {
            timer += Time.deltaTime;
            Debug.Log($"{timer}");
            yield return null;
        }
    }
}
