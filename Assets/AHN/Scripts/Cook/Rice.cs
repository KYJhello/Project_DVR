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
        /// TODO : ��Ʈ�ѷ� ��ư 3�� �̻� ������ �� �򰡰� �ö�. ���� ��ư�� ���� ������ ����.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            base.OnSelectEntering(args);

            // if (��Ʈ�ѷ��� ���ι�ư(A?)�� 3�� �̻� �����ٸ�)
            // { �ʹ��� ���� += 10f; }
            
            // if (��Ʈ�ѷ��� ���ι�ư(A?)�� �����ٸ�)
            // { �������; } 



            // * ������ Grab ��ư�� ��� ������ �־�� �����µ� 
            // * �� ���� ������ ��� ���� �� �ֵ���
        }
    }
}*/


namespace AHN
{
    public class Rice : MonoBehaviour
    {
        // ������ �ö󰡴� �Լ�. (���� �������� �� ������ �н�)
        bool canScroeUp = true;
        bool oneButton = false;
        bool twoButton = false;
        bool threeButton = false;
        int currentScore = 0;   // ������ϱ� ���� ����. ���� ����

        XRController xrController;

        private void Start()
        {
            xrController = GameObject.FindObjectOfType<XRController>(); ;
        }

        /// <summary>
        /// ���� ��� ��Ʈ�ѷ� ��ư�� �������� ������ ����. ������ 3��������.
        /// </summary>
        /// <param name="currentScore"></param>
        public void ScoreUP(int currentScore)
        {
            // TODO : ��ư ���� ������ ���� ����


            if (threeButton)    // �� ���� �� ä������ �� �̻� ���� ���ø��� return
            {
                Debug.Log(currentScore);
                ActivateHaptic();
                return;
            }
            else if (twoButton)     // �� ��° Ŭ��
            {
                threeButton = true;
                currentScore += 500;
                twoButton = false;                 // canScoreUp = false, oneButton = false, twoButton = false, threeButton = true;
                ActivateHaptic();
                Debug.Log(currentScore);
            }
            else if (oneButton)     // �� ��° Ŭ��
            {
                twoButton = true;
                currentScore += 500;
                oneButton = false;                 // canScoreUp = false, oneButton = false, twoButton = true, threeButton = false;
                ActivateHaptic();
                Debug.Log(currentScore);
            }
            if (canScroeUp)     // ù ��° Ŭ��
            {
                oneButton = true;
                currentScore += 500;
                canScroeUp = false;                 // canScoreUp = false, oneButton = true, twoButton = false, threeButton = false;
                ActivateHaptic();
                Debug.Log(currentScore);
            }
        }

        void ActivateHaptic()
        {
            xrController.SendHapticImpulse(0.8f, 2f);
        }
    }
}