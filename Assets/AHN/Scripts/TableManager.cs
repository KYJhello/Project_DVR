using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace AHN
{
    public class TableManager : MonoBehaviour
    {
        // Seat[] 을 만들어서 좌석들을 관리함.
        // TODO : 만석이면 customer가 더이상 생성되지 못하도록. Bool 반환형으로 테이블이 만석인지아닌지를 알려주는 함수를 만들고 이걸 이벤트로 넣어둠.
        // 그리고 손님이 생성될 때 그 함수를 호출시켜서 true면 손님 생성 금지!


        // seat에 Vector3 (손님과 seat의 거리가) < 1f 이라면 좌석이 찼다는 것.
        // seat 를 딕셔너리로 관리해서 seat1 empty, seat2 full 이런식으로 empty, full을 value로, seat[n]을 key로
        // Dictionary(key, value) 라서 Dictionary(seat[n], stirng) 으로 해서 관리.


        [SerializeField] SerializedDictionary<int, string> SeatDic;
    }
}