using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace AHN
{
    public class TableManager : MonoBehaviour
    {
        public Dictionary<Transform, bool> SeatDic;    // 좌석들의 trasnform, 찼는지안찼는지 여부 bool

        private void Awake()
        {
            SeatDic = new Dictionary<Transform, bool>();
            AddDictionary();
        }

        void AddDictionary()    // 좌석들을 모두 SeatDic에 넣는 과정
        {
            Transform[] seats = gameObject.GetComponentsInChildren<Transform>();

            foreach (Transform seat in seats)
            {
                Transform key = seat.transform;

                if (seat.name == "TableManager")     // 자기 자신은 제외
                    continue;
                else if (seat.GetComponent<PlateRecognition>())     // 접시(음식)이 놓일 테이블 위치도 제외
                    continue;
                else if (SeatDic.ContainsKey(key))
                    continue;

                SeatDic.Add(key, false);
            }
        }

        // 딕셔너리에서 빈자리를 찾는 함수. value값이 False인 transforms들을 반환함. customer측에서는 이 함수를 호출하여 앉으면됨
        public List<Transform> FalseSeat()
        {
            List<Transform> falseSeatsList = new List<Transform>();
            
            foreach (KeyValuePair<Transform, bool> seat in SeatDic)
            {
                if(seat.Value == false)
                {
                    falseSeatsList.Add(seat.Key);
                }
            }

            return falseSeatsList;
        }

        // 가게가 만석인지 아닌지. 만석이면 true
        public bool IsSeatFull()
        {
            if (FalseSeat().Count <= 0)
                return true;
            else
                return false;
        }

        // 좌석 고르기
        public void SelectSeat()
        {
            // 1. 빈좌석을 가져옴
            List<Transform> falseSeatList = FalseSeat();
 
            if (falseSeatList.Count <= 0)   // 좌석 없으면 입장 금지     
                return;

            // 2. falseSeatList에서 랜덤으로 하나를 뽑아서 내 좌석으로 지정
            int randomSeat = UnityEngine.Random.Range(0, falseSeatList.Count - 1);
            Transform customerSeat = falseSeatList[randomSeat];

            // 3. 고른 좌석의 value값은 true로 변경
            SeatDic[falseSeatList[randomSeat]] = true; 
        }

        // TODO : 손님이 다 먹고 나갈경우, 좌석을 다시 false로 변경해주어야 하는데,
        // 손님의 EatState가 끝날 때, 그 상태에서 함수를 하나 만들어. 그리고 그 함수를 이벤트에 
        // 매개변수를 mySeat로 하고, 그 매개변수의 value값을 false로 만들어주는 함수
        public void ChangeValueFalse(Transform seat)
        {
            SeatDic[seat] = false;
        }
    }
}