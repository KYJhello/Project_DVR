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
        public Transform kioskDestination;
        public Transform mySeatDestination;     // 얘는 드래그가 안 돼. 직접 할당해줘야함.
        public float speed;
        
        [SerializeField] TableManager tableManager;

        private void Awake()
        {
            customersPos.position = transform.position;
        }

        // 좌석 고르기
        public void SelectSeat()
        {
            // 1. 빈좌석을 가져옴
            // List<Transform> falseSeatList = GameManager.Table.FalseSeat();  //-> 이게 왜 안되는지 모르겠음 (일단 씬에 gameObj없으니까 그거 붙여바)
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