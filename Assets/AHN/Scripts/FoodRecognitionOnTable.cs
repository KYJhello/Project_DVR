using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.XR.Interaction.Toolkit;

namespace AHN
{
    public class FoodRecognitionOnTable : MonoBehaviour
    {
        // 이 오브젝트의 collider에 Plate 레이어(24)가 트리거되면 정해진 위치로 착 달라붙도록 (그 위치는 그냥 이 오브젝트의 위치로)

        public bool isPlate = false;
        public List<GameObject> plateAndFood;

        private void Start()
        {
            plateAndFood = new List<GameObject>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 24)   // 24 : 접시
            {
                GameObject plate = other.gameObject;
                other.gameObject.transform.position = transform.position;
                isPlate = true;
                other.GetComponent<XRGrabInteractable>().enabled = false;
                other.GetComponent<Rigidbody>().isKinematic = true;

                other.gameObject.GetComponentInChildren<BoxCollider>().isTrigger = false;
                plateAndFood.Add(other.gameObject);

            }
            else if (other.gameObject.layer == 23)  // 초밥
            {
                GameObject sushi = other.gameObject;
                plateAndFood.Add(other.gameObject);
            }
        }

        public bool IsPlate()   // 테이블 앞에 접시가 있는지 없는지
        {
            return isPlate;
        }

        public void IsPlateFalse()
        {
            isPlate = false;
        }

        public List<GameObject> PlateAndFood()
        {
            return plateAndFood;
        }
    }
}
