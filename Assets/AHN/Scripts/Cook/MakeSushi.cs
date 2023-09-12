using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace AHN
{
    public class MakeSushi : MonoBehaviour
    {
        [SerializeField] GameObject rice;
        [SerializeField] GameObject sushi;
        [SerializeField] GameObject rawFish;
        [SerializeField] GameObject wasabi;
        [SerializeField] Transform riceSocketTransform;    // 밥과 회가 얼마나 가까워졌는지의 기준이 됨

        bool canScroeUp = true;
        bool oneButton = false;
        bool twoButton = false;
        bool threeButton = false;
        int currentScore = 0;   // 디버깅하기 위한 예시. 현재 점수

        XRBaseController xrController;

        private void Start()
        {
            xrController = GameObject.FindObjectOfType<XRBaseController>(true);
            sushi.SetActive(false);
        }

        private void OnEnable()
        {
            StartCoroutine(LocatedRiceAndSushiSameTransformRoutine());      // 초밥과 밥의 위치가 항상 같게 있도록
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

        IEnumerator LocatedRiceAndSushiSameTransformRoutine()
        {
            while (true)
            {
                if (Vector3.Distance(rawFish.transform.position, riceSocketTransform.position) < 0.5f)    // 회와 밥의 위치가 가까워졌다면,
                {
                    FinishSushi();
                }

                sushi.transform.position = rice.transform.position;
                yield return null;
            }
        }

        void FinishSushi()
        {
            // 밥 자식으로 있는 Socket Transform과 회의 위치가 가까워지면 밥, 와사비, 회를 비활성화하고 초밥을 활성화 시킨다.
            sushi.SetActive(true);      // 여기서 완성본 스시들은 Resources 폴더에 다 넣고 회에 따라서 회에 맞는 sushi를 Resources에서 가져오기?
            wasabi.SetActive(false);
            rawFish.SetActive(false);
            rice.SetActive(false);
        }
    }
}