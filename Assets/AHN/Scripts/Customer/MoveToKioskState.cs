using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToKioskState : StateMachineBehaviour
{
    Customer customer;
    NavMeshAgent agent;
    float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)   // 매장에 들어오자마자 할 일
    {
        timer = 0f;
        customer = animator.GetComponent<Customer>();
        agent = animator.GetComponent<NavMeshAgent>();  // 여기서 네이매쉬를 가져옴
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)  // 키오스크까지 걸어가는 일
    {
        Debug.Log("MoveToKiosk");

        agent.destination = customer.destination.position;      // 이렇게 하면 detination으로 가는 기능인듯
                                                                // customer.destinaton은 키오스크 앞이 될 것임.
        timer += Time.deltaTime;
        if (timer > 3f)
        {
            animator.SetTrigger("IsFrontKiosk");
            timer = 0f;
        }

        // if (customer의 pos와 desination의 거리가 < 1f) 이면, animator.SetTrigger("주문하는거");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)    // 키오스크에 딱 도착할 때쯤
    {
        // 필요없을듯
    }
}
