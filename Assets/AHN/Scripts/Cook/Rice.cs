using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

/*
namespace AHN
{
    public class Rice : XRBaseInteractable
    {
        /// <summary>
        /// TODO : 컨트롤러 버튼 3번 이상 누르면 맛 평가가 올라감. 또한 버튼을 누를 때마다 진동.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            base.OnSelectEntering(args);

            // if (컨트롤러의 메인버튼(A?)을 3번 이상 눌렀다면)
            // { 초밥의 점수 += 10f; }
            
            // if (컨트롤러의 메인버튼(A?)을 누른다면)
            // { 진동기능; } 



            // * 지금은 Grab 버튼을 계속 누르고 있어야 잡히는데 
            // * 한 번만 눌러도 계속 잡힐 수 있도록
        }
    }
}*/


namespace AHN
{
    public class Rice : MonoBehaviour
    {
        // 점수가 올라가는 함수. (밥이 뭉쳐지는 건 어려우니 패스)
        bool canScroeUp = true;
        bool oneButton = false;
        bool twoButton = false;
        bool threeButton = false;

        /// <summary>
        /// 밥을 쥐고 컨트롤러 버튼을 누를수록 점수가 증가. 하지만 3번까지만.
        /// </summary>
        /// <param name="currentScore"></param>
        public void ScoreUP(int currentScore)
        {
            if (canScroeUp)     // 첫 번째 클릭
            {
                oneButton = true;
                currentScore += 500;
                canScroeUp = false;                 // canScoreUp = false, oneButton = true, twoButton = false, threeButton = false;
                Debug.Log(currentScore);
            }
            else if (oneButton)     // 두 번째 클릭
            {
                twoButton = true;
                currentScore += 500;
                oneButton = false;                 // canScoreUp = false, oneButton = false, twoButton = true, threeButton = false;
                Debug.Log(currentScore);
            }
            else if (twoButton)     // 세 번째 클릭
            {
                threeButton = true;
                currentScore += 500;
                twoButton = false;                 // canScoreUp = false, oneButton = false, twoButton = false, threeButton = true;
                Debug.Log(currentScore);
            }
            else if (threeButton)    // 세 번을 다 채웠으니 더 이상 점수 못올리고 return
            {
                Debug.Log(currentScore);
                return;
            }
        }
    }
}