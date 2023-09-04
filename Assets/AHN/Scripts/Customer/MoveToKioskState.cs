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

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)   // 매장에 들어오자마자 할 일
        {
            customer = animator.GetComponent<Customer>();
            animator.gameObject.GetComponent<Customer>().agent.destination = customer.kioskDestination.position;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)  // 키오스크까지 걸어가는 일
        {
            if (Vector3.Distance(animator.gameObject.transform.position, customer.kioskDestination.position) < 1f)
            {
                animator.SetTrigger("IsFrontKiosk");
            }
        }
    }
}