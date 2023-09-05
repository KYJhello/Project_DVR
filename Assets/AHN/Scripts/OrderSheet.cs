using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class OrderSheet : MonoBehaviour
    {
        [SerializeField] TextMesh menuText;     // 랜덤값으로 뽑을 메뉴

        // TODO : 1.MenuText 입력함수
        // PosManager에 있는 PrintOrderSheet() 함수에서 이 게임오브젝트가 생성될거임. (되도록 풀링으로)
        public void MenuTextInput()
        {
            // menuText.text = "이미 정해진 메뉴들 중, 랜덤값으로 정한 하나의 메뉴";
        }
    }
}
