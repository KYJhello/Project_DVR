using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace AHN
{
    public class CustomerSqawnManager : MonoBehaviour
    {
        GameObject customer;
        TableManager tableManager;

        public void Start()
        {
            tableManager = GameObject.Find("TableManager").GetComponent<TableManager>();
            customer = GameManager.Resource.Load<GameObject>("Customer");

            StartCoroutine(CustomerSpawnRoutine());

        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }


        public IEnumerator CustomerSpawnRoutine()
        {
            while (true)
            {
                if (tableManager.IsSeatFull())  // tableManager.IsSeatFull 이 true면 만석. 생성금지
                {
                    // yield return null;
                    yield return new WaitForSeconds(3f);
                }
                else if (!tableManager.IsSeatFull())
                {
                    GameObject newCustomer = GameManager.Pool.Get(customer, transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(5f);
                }
            }
        }
    }
}