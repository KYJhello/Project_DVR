using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace AHN
{
    public class MoveToTableState : StateMachineBehaviour
    {
        NavMeshAgent agent;
        Customer customer;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            agent = animator.GetComponent<NavMeshAgent>();
            customer = animator.GetComponent<Customer>();
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            agent.destination = customer.mySeatDestination.position;

            if (Vector3.Distance(animator.gameObject.transform.position, customer.mySeatDestination.position) < 1f)
            {
                animator.SetTrigger("IsFrontTable");
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}