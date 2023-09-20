using Jeon;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class KinfeRay : MonoBehaviour
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
        Debug.DrawRay(transform.position, transform.forward * hitInfo.distance, Color.red);


        // 레이가 특정레이어 오브젝트에 닿고있으면
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDistance, mask))
        {
            fishBodyMeat = hitInfo.transform.GetComponent<FishBodyMeat>();
            fishBodyMeat.firstHeadHit = true;
            collisionNormal = hitInfo.normal;
            hitInfoPos = hitInfo.transform.position;
            fish = hitInfo.collider.gameObject;
            Debug.Log($"{collisionNormal}");
        }
        else
        {
            if (fishBodyMeat != null)
                fishBodyMeat.firstHeadHit = false;       // 레이어가 특정레이어 오브젝트에 닿고있지 않으면
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 v = collisionNormal - transform.forward;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        // 닿은 오브젝트의 레이어가 29번이고, 트루상태라면
        if (other.gameObject.layer == 29 && fishBodyMeat.firstHeadHit && angle > 70 && angle <120)
        {
            //Debug.Log("트리거 됐다");
            //Debug.Log($"{collisionNormal.x < fishBodyMeat.x}");

            fishBodyMeat.CuttingFish();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Vector3 v = collisionNormal - transform.forward;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        if ((other.gameObject.layer == 29) && !fishBodyMeat.firstHeadHit && angle > -210 && angle < -150)
        {
            Debug.Log("나가졌다");

            fishBodyMeat.TakePrefab();
        }
    }
}
