using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class OrderSheet : MonoBehaviour
    {
        [SerializeField] TextMesh menuText;     // 랜덤값으로 뽑을 메뉴

        // 1. MenuText 입력함수
        public void MenuTextInput()
        {
            // PosManager에 있는 PrintOrderSheet() 함수에서 이 게임오브젝트가 생성될거임. (되도록 풀링으로)
            // 현재 있는 물고기에 따라 정해진 메뉴들 중, 랜덤값으로 하나의 메뉴가 출력되도록 할 것임.
        }
    }
}
