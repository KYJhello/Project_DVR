using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace AHN
{
    public class EatState : StateMachineBehaviour
    {
        StateCorutineManager corutineManger;
        int amount = 15;     // 결제한 금액. 일단 15라고 적어놓음

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            corutineManger = animator.GetComponent<StateCorutineManager>();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            corutineManger.StartCoroutine(corutineManger.EatRoutine());
            animator.SetTrigger("GoOut");
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // TODO : 결제.
            // KIM_FishTank 리스트에서 물고기 등급을 받아옴. 등급에 따라 결제금액이 달라짐.
            // + 결제하면서 결제되는 사운드
            PosManager.OnPayEvent?.Invoke(amount);

            // TODO : 접시를 풀로 생성하는지 Inst그걸로 생성하는지 물어봐야함. 일단 destroy로 없어지게 해놨음

            List<GameObject> plateAndFoods = animator.GetComponent<Customer>().mySeat.gameObject.GetComponentInChildren<PlateRecognition>().PlateAndFood();

            foreach (GameObject plateAndFood in plateAndFoods)
            {
                Destroy(plateAndFood);
            }
        }
    }
}