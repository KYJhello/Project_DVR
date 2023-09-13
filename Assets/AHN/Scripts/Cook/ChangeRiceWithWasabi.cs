using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace AHN
{
    public class ChangeRiceWithWasabi : MonoBehaviour
    {
        [SerializeField] GameObject wasabi;

        private void Start()
        {
            gameObject.SetActive(true);
            wasabi.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 22)    // 와사비(22)가 트리거 됐다면, 와사비가 이미 발라진 밥으로 교체
            {
                Destroy(other.gameObject);
                wasabi.SetActive(true);
            }
        }
    }
}