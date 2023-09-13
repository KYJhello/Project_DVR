using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class KinfeRay : MonoBehaviour
{
    public LayerMask mask;
    public RaycastHit hitInfo;
    public float maxDistance = 10f;

    bool firstHeadHit = false;

    float x = -1f;
    Vector3 collisionNormal;
    Vector3 hitInfoPos;
    GameObject fish;

    float timer = 0;
    Coroutine check;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * hitInfo.distance, Color.red);

        // 레이가 특정레이어 오브젝트에 닿고있으면
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDistance, mask))
        {
            firstHeadHit = true;
            collisionNormal = hitInfo.normal;
            hitInfoPos = hitInfo.transform.position;
            fish = hitInfo.collider.gameObject;
            Debug.Log($"{collisionNormal}");
        }
        else
        {
            firstHeadHit = false;       // 레이어가 특정레이어 오브젝트에 닿고있지 않으면
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 닿은 오브젝트의 레이어가 29번이고, 트루상태라면
        if (other.gameObject.layer == 29 && firstHeadHit && collisionNormal.x < x)
        {

            Debug.Log("트리거 됐다");
            Debug.Log($"{collisionNormal.x < x}");
            CuttingFish();
        }
    }

    public void CuttingFish()
    {
        if (!firstHeadHit)
            return;

        if (collisionNormal.x < x)
        {
            Debug.Log("쭉 진행중");
            check = StartCoroutine(CheckSecondHitTime());
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.layer == 29) && !firstHeadHit && collisionNormal.x < x)
        {
            Debug.Log("나가졌다");

            if (timer < 2)
            {
                Debug.Log("프리팹가져와");
                for (int i = 0; i < 2; i++)
                {
                    GameManager.Resource.Instantiate<GameObject>("Jeon_Prefab/FishMeat", hitInfoPos, Quaternion.identity);
                }
                Debug.Log($"{fish.transform.parent.name}");
                Destroy(fish.transform.parent.gameObject);

                StopCoroutine(check);
                timer = 0;
            }
            else if (timer >= 2)
            {
                Debug.Log("다시잘라");
                timer = 0;
                StopCoroutine(check);
            }
        }
    }

    IEnumerator CheckSecondHitTime()
    {
        Debug.Log("2초안에 자르기");
        while (true)
        {
            timer += Time.deltaTime;
            Debug.Log($"{timer}");
            yield return null;
        }
    }
}
