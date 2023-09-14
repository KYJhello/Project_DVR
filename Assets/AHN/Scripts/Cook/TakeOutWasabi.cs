using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class TakeOutWasabi : MonoBehaviour
    {
        // WasabiPile에 손을 넣으면 Wasabi한 블록이 손에 나오도록.
        [SerializeField] Transform wasabiTransform;
        [SerializeField] int inWasabiCount;

        private void Start()
        {
            GameObject wasabi = GameManager.Resource.Instantiate<GameObject>("Wasabi");
            wasabi.gameObject.transform.position = wasabiTransform.position;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 22)
            {
                if (other.GetComponent<Rigidbody>() != null)
                {
                    other.GetComponent<Rigidbody>().isKinematic = false;
                }

                GameObject wasabi = GameManager.Resource.Instantiate<GameObject>("Wasabi");
                // wasabi.GetComponent<Rigidbody>().isKinematic = true;
                wasabi.gameObject.transform.position = wasabiTransform.position;
            }
        }
    }
}