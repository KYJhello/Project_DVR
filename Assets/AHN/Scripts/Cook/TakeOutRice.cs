using SerializableCallback;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace AHN
{
    public class TakeOutRice : MonoBehaviour
    {
        // RicePile�� ���� ������ Rice �� ����� �տ� ��������.
        [SerializeField] Transform riceTransform;
        int riceCount;

        private void Start()
        {
            GameObject rice = GameManager.Resource.Instantiate<GameObject>("SushiManager");
            riceCount = 1;
            // rice.GetComponentInChildren<Rigidbody>().isKinematic = true;
            rice.gameObject.transform.position = riceTransform.position;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 20)
            {
                Rigidbody rb = other.GetComponentInChildren<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;     // Ű�׸�ƽ �� ��~ (Grab �ϴ� ���� ������ �� �Ǵ� ��)
                    Debug.Log("�� ����");
                }

                GameObject rice = GameManager.Resource.Instantiate<GameObject>("SushiManager");
                riceCount++;
                // rice.GetComponentInChildren<Rigidbody>().isKinematic = true;
                rice.gameObject.transform.position = riceTransform.position;
            }
        }

    }
}