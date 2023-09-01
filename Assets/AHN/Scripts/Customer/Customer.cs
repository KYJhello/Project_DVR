using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class Customer : MonoBehaviour
    {
        // 키오스크 앞, 테이블 앞, 문으로 갈 때 갈 위치에 빈 오브젝트를 넣어서 그 오브젝트를 향해서 갈 수 있도록함.
        // ex.키오스크 앞에 빈오브젝트 하나 둬서 customer가 그 빈오브젝트를 향해서 move하도록.

        public Transform customersPos;
        public Transform customersDir;
        public Transform KioskDestination;
        public float speed;

        private void Awake()
        {
            customersPos.position = transform.position;
        }
    }
}