using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace AHN
{
    public class MakeSushi : MonoBehaviour
    {
        [SerializeField] GameObject rice;
        bool canScroeUp = true;
        bool oneButton = false;
        bool twoButton = false;
        bool threeButton = false;
        int currentScore = 0;   // 디버깅하기 위한 예시. 현재 점수

        XRBaseController xrController;

        private void Start()
        {
            xrController = GameObject.FindObjectOfType<XRBaseController>(true);
        }

        private void OnEnable()
        {
            rice.SetActive(true);
        }

        /// <summary>
        /// 밥을 쥐고 컨트롤러 버튼을 누를수록 점수가 증가 + 진동. 하지만 점수 증가는 3번까지만.
        /// </summary>
        /// <param name="currentScore"></param>
        public void ScoreUP(ActivateEventArgs args)
        {
            if (threeButton)    // 세 번을 다 채웠으니 더 이상 점수 못올리고 return
            {
                Debug.Log(currentScore);
                ActivateHaptic(args);
                return;
            }
            else if (twoButton)     // 세 번째 클릭
            {
                threeButton = true;
                currentScore += 500;
                twoButton = false;                 // canScoreUp = false, oneButton = false, twoButton = false, threeButton = true;
                ActivateHaptic(args);
                Debug.Log(currentScore);
            }
            else if (oneButton)     // 두 번째 클릭
            {
                twoButton = true;
                currentScore += 500;
                oneButton = false;                 // canScoreUp = false, oneButton = false, twoButton = true, threeButton = false;
                ActivateHaptic(args);
                Debug.Log(currentScore);
            }
            if (canScroeUp)     // 첫 번째 클릭
            {
                oneButton = true;
                currentScore += 500;
                canScroeUp = false;                 // canScoreUp = false, oneButton = true, twoButton = false, threeButton = false;
                ActivateHaptic(args);
                Debug.Log(currentScore);
            }
        }

        void ActivateHaptic(ActivateEventArgs args)
        {
            XRBaseControllerInteractor interactor = args.interactorObject.transform.GetComponent<XRBaseControllerInteractor>();
            if (interactor != null)
            {
                interactor.SendHapticImpulse(0.3f, 0.1f);
            }
        }
    }
}
