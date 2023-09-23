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
            // 무한루프가 아니라 오늘이 끝나면 (bool로 오늘이 끝났는지 안 끝났는지를 나타내줌녀 좋을듯)
            while (true)
            {
                if (tableManager.IsSeatFull())  // tableManager.IsSeatFull 이 true면 만석. 생성금지
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