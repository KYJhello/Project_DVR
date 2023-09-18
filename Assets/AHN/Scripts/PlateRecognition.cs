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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 24)
            {
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                plate = other.gameObject;
                other.gameObject.transform.position = transform.position;
                isPlate = true;
                other.GetComponent<XRGrabInteractable>().enabled = false;
                rb.isKinematic = true;

                // TODO : 접시 rotation의 z와 x가 freeze 되도록
                other.gameObject.GetComponentInChildren<BoxCollider>().isTrigger = false;
            }
        }

        public bool IsPlate()
        {
            return isPlate;
        }
    }
}
