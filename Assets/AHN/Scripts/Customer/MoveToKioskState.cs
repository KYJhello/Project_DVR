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
            agent = animator.GetComponent<NavMeshAgent>();  // 여기서 네이매쉬를 가져옴
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)  // 키오스크까지 걸어가는 일
        {
            Debug.Log("MoveToKiosk");

            agent.destination = customer.KioskDestination.position;

            if (Vector3.Distance(animator.gameObject.transform.position, customer.KioskDestination.position) < 1f)
            {
                animator.SetTrigger("IsFrontKiosk");
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)    // 키오스크에 딱 도착할 때쯤
        {
            // 필요없을듯
        }
    }
}