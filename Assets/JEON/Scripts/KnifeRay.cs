using Jeon;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class KnifeRay : MonoBehaviour
{
    public LayerMask mask;
    public RaycastHit hitInfo;
    public float maxDistance = 10f;

    public Vector3 collisionNormal;
    public Vector3 hitInfoPos;
    public GameObject fish;

    FishBodyMeat fishBodyMeat;

    private void Awake()
    {
        fishBodyMeat = null;
    }
    private void Update()
    {
        // DrawRay로 방향으로 값만큼으로 선을 그어준다
        Debug.DrawRay(transform.position, transform.forward * hitInfo.distance, Color.red);

        // 레이가 특정레이어 오브젝트에 닿고있으면
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDistance, mask))
        {
            fishBodyMeat = hitInfo.transform.GetComponent<FishBodyMeat>();
            collisionNormal = hitInfo.normal;
            hitInfoPos = hitInfo.transform.position;
            fish = hitInfo.collider.gameObject;
            Debug.Log($"{collisionNormal}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // v는 닿은 콜라이더의 노말백터에 칼의 forward방향을 뺀 값입니다.
        Vector3 v = collisionNormal - transform.forward;
        // Mathf.Atan2(v.y, v.x)는 주어진 벡터 v의 y와 x 성분을 이용하여 아크탄젠트 값을 계산합니다
        // 이 결과 값은 라디안 단위이므로, 일반적으로 각도로 표현하려면 Mathf.Rad2Deg 상수를 곱해야 합니다.
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

        // 닿은 오브젝트의 레이어가 29번이며, angle의 값이 70보다크고 120보다 작으면 트리거되도록
        if (other.gameObject.layer == 29 && angle > 70 && angle <120)
        {
            fishBodyMeat.CuttingFish();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Vector3 v = collisionNormal - transform.forward;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

        // Exit된게 29번 레이어며 angle이 -210이상 -150이하면 exit되도록
        if ((other.gameObject.layer == 29) && angle > -210 && angle < -150)
        {
            Debug.Log("나가졌다");

            fishBodyMeat.TakePrefab();
        }
    }
}
