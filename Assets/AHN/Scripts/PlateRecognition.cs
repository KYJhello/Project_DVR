using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace AHN
{
    public class PlateRecognition : MonoBehaviour
    {
        // 이 오브젝트의 collider에 Plate 레이어(24)가 트리거되면 정해진 위치로 착 달라붙도록 (그 위치는 그냥 이 오브젝트의 위치로)

        public bool isPlate = false;
        public GameObject plate;
        public List<GameObject> plateAndFood;

        private void Start()
        {
            plateAndFood = new List<GameObject>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 24)
            {
                plate = other.gameObject;
                other.gameObject.transform.position = transform.position;
                isPlate = true;
                other.GetComponent<XRGrabInteractable>().enabled = false;
                other.GetComponent<Rigidbody>().isKinematic = true;

                // TODO : 접시 rotation의 z와 x가 freeze 되도록

                other.gameObject.GetComponentInChildren<BoxCollider>().isTrigger = false;
            }
            plateAndFood.Add(other.gameObject);
        }
        public bool IsPlate()
        {
            return isPlate;
        }

        public List<GameObject> PlateAndFood()
        {
            return plateAndFood;
        }
    }
}
