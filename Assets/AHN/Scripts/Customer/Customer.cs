using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace AHN
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] TableManager tableManager;
        public Transform kioskDestination;
        public Transform mySeatDestination;     // 자동할당
        public NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            tableManager = GameObject.Find("TableManager").GetComponent<TableManager>();
            kioskDestination = GameObject.Find("KioskDestnation").GetComponent<Transform>();
            SelectSeat();

            agent.enabled = true;
            GetComponent<Animator>().enabled = true;
        }

        // 좌석 고르기
        public void SelectSeat()
        {
            // 1. 빈좌석을 가져옴
            List<Transform> falseSeatList = tableManager.FalseSeat();

            if (falseSeatList.Count <= 0)   // 좌석 없으면 입장 금지
                return;

            // 2. falseSeatList에서 랜덤으로 하나를 뽑아서 내 좌석으로 지정
            int randomSeat = UnityEngine.Random.Range(0, falseSeatList.Count - 1);
            mySeatDestination = falseSeatList[randomSeat];

            // 3. 고른 좌석의 value값은 true로 변경
            // GameManager.Table.SeatDic[falseSeatList[randomSeat]] = true;
            tableManager.SeatDic[falseSeatList[randomSeat]] = true;
        }
    }
}