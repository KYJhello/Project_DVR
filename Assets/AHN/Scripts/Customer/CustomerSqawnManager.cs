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

        }

        public IEnumerator CustomerSpawnRoutine()
        {
            // ���ѷ����� �ƴ϶� ������ ������ (bool�� ������ �������� �� ���������� ��Ÿ���ܳ� ������)
            while (true)
            {
                if (tableManager.IsSeatFull())  // tableManager.IsSeatFull �� true�� ����. ��������
                {
                    // yield return null;
                    yield return new WaitForSeconds(3f);
                }
                else if (!tableManager.IsSeatFull())
                {
                    GameManager.Pool.Get(customer, transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(5f);
                }
            }
        }
    }
}