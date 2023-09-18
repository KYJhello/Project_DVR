using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class TakeOutPlate : MonoBehaviour
    {
        [SerializeField] GameObject plateTransform;

        private void Start()
        {
            GameObject plate = GameManager.Resource.Instantiate<GameObject>("Plate_AHN");
            plate.gameObject.transform.position = plateTransform.transform.position;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 24)
            {
                Debug.Log("Plate Out");
                GameObject plate = GameManager.Resource.Instantiate<GameObject>("Plate_AHN");
                plate.gameObject.transform.position = plateTransform.transform.position;
            }
        }
    }
}