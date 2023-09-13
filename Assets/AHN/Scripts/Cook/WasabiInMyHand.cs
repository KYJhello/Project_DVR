using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class WasabiInMyHand : MonoBehaviour
    {
        // WasabiPile에 손을 넣으면 Wasabi한 블록이 손에 나오도록.
        [SerializeField] GameObject wasabi;
        [SerializeField] GameObject wasabiTransform;

        private void Start()
        {
            wasabi = GameManager.Resource.Instantiate<GameObject>("Wasabi");
            wasabi.GetComponent<Rigidbody>().isKinematic = true;
            wasabi.gameObject.transform.position = wasabiTransform.transform.position;
        }
    }
}