using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AHN
{
    public class MoveToKioskState : StateMachineBehaviour
    {
        Customer customer;
        NavMeshAgent agent;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)   // 매장에 들어오자마자 할 일
        {
            customer = animator.GetComponent<Customer>();
            agent = animator.GetComponent<NavMeshAgent>();
            animator.GetComponent<Customer>().SelectSeat();
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)  // 키오스크까지 걸어가는 일
        {
            agent.destination = customer.kioskDestination.position;

            if (Vector3.Distance(animator.gameObject.transform.position, customer.kioskDestination.position) < 1f)
            {
                animator.SetTrigger("IsFrontKiosk");
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)    // 키오스크에 딱 도착할 때쯤
        {
            // 필요없을듯
        }
        
        public void SelectSeat()    // 좌석 고르기
        {
            // 1. 빈좌석을 가져옴
            List<Transform> falseSeatList = GameManager.Table.FalseSeat();

            if (falseSeatList.Count <= 0)   // 좌석 없으면 입장 금지 
                return;

            // 2. falseSeatList에서 랜덤으로 하나를 뽑아서 내 좌석으로 지정
            int randomSeat = Random.Range(0, falseSeatList.Count - 1);
            Transform mySeat = falseSeatList[randomSeat];
        }
    }
}