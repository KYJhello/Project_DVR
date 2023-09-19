using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSushiScore : MonoBehaviour
{
    // SushiManager인 밥에 붙일 score.
    // 밥에서 여기 점수를 += 하고, fish에서 +=하고, 마지막 초밥이 만들어 질 때 그 초밥에게 여기 변수를 넘겨줌
    public static int currentSushiScore;

    private void OnEnable()
    {
        currentSushiScore = 0;
    }
}
