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

            // TODO : Exit 문을 Destination으로 잡고 나가야함
            customerSqawn = GameObject.Find("CustomerSpawnPoint").GetComponent<CustomerSqawnManager>();
            animator.GetComponent<Customer>().agent.destination = customer.kioskDestination.position;

        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // 여기서 손님은 없어져야함. 풀링으로 비활성화.
            // if (Exit 문이랑 손님 거리가 1도 안 된다면)
            GameManager.Pool.Release(animator.gameObject);
        }
    }
}