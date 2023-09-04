using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace AHN
{
    public class MoveToTableState : StateMachineBehaviour
    {
        Customer customer;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            customer = animator.GetComponent<Customer>();
            animator.gameObject.GetComponent<Customer>().agent.destination = customer.mySeatDestination.position;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Vector3.Distance(animator.gameObject.transform.position, customer.mySeatDestination.position) < 1f)
            {
                animator.SetTrigger("IsFrontTable");
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}