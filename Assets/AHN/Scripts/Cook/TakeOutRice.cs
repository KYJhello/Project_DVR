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
        // RicePile에 손을 넣으면 Rice 한 블록이 손에 나오도록.
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
                    rb.isKinematic = false;     // 키네마틱 안 됨~ (Grab 하는 동안 변경이 안 되는 듯)
                    Debug.Log("밥 나감");
                }

                GameObject rice = GameManager.Resource.Instantiate<GameObject>("SushiManager");
                riceCount++;
                // rice.GetComponentInChildren<Rigidbody>().isKinematic = true;
                rice.gameObject.transform.position = riceTransform.position;
            }
        }

    }
}