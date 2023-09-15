using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class OrderSheet : MonoBehaviour
    {
        public TextMesh menuText;     // 랜덤값으로 뽑을 메뉴

        // TODO : 테이블 번호도 적어야 함. 이건 손님의 mySeat가 됨. 그거를 참조 ㄱㄱ

        public void MenuTextInput(string menu, int seatNumber)
        {
            // menuText.text = "이미 정해진 메뉴들 중, 랜덤값으로 정한 하나의 메뉴";
            menuText.text = $"{menu}\n{seatNumber}번 좌석";
        }
    }
}
