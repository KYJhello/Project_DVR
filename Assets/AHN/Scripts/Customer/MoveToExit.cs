using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class MoveToExit : StateMachineBehaviour
    {
        CustomerSqawnManager customerSqawn;
        Customer customer;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            customer = animator.GetComponent<Customer>();

            animator.GetComponent<Customer>().tableManager.ChangeValueFalse(animator.GetComponent<Customer>().mySeat);      // 자기 자리를 false 로 바꿈

            customerSqawn = GameObject.Find("CustomerSpawnPoint").GetComponent<CustomerSqawnManager>();
            animator.GetComponent<Customer>().agent.destination = customer.customerSpawnPoint.position;

        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Vector3.Distance(animator.transform.position, customer.customerSpawnPoint.position) < 2f)
            {
                GameManager.Pool.Release(animator.gameObject);
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}