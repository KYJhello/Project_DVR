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
        // DrawRay�� �������� ����ŭ���� ���� �׾��ش�
        Debug.DrawRay(transform.position, transform.forward * hitInfo.distance, Color.red);

        // ���̰� Ư�����̾� ������Ʈ�� ���������
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
        // v�� ���� �ݶ��̴��� �븻���Ϳ� Į�� forward������ �� ���Դϴ�.
        Vector3 v = collisionNormal - transform.forward;
        // Mathf.Atan2(v.y, v.x)�� �־��� ���� v�� y�� x ������ �̿��Ͽ� ��ũź��Ʈ ���� ����մϴ�
        // �� ��� ���� ���� �����̹Ƿ�, �Ϲ������� ������ ǥ���Ϸ��� Mathf.Rad2Deg ����� ���ؾ� �մϴ�.
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

        // ���� ������Ʈ�� ���̾ 29���̸�, angle�� ���� 70����ũ�� 120���� ������ Ʈ���ŵǵ���
        if (other.gameObject.layer == 29 && angle > 70 && angle <120)
        {
            fishBodyMeat.CuttingFish();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Vector3 v = collisionNormal - transform.forward;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

        // Exit�Ȱ� 29�� ���̾�� angle�� -210�̻� -150���ϸ� exit�ǵ���
        if ((other.gameObject.layer == 29) && angle > -210 && angle < -150)
        {
            Debug.Log("��������");

            fishBodyMeat.TakePrefab();
        }
    }
}
