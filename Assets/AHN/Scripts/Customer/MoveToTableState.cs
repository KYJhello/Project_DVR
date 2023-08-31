using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTableState : StateMachineBehaviour
{
    NavMeshAgent agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 테이블까지 걷는 애니메이션 + 테이블까지 네비매시로 이동.
        // 근데 의자가 7개로 치면 그 7개 중 랜덤값으로 나온 의자로 가야함.
        // 의자는 배열로 관리. 의자[0~6 랜덤값]로 가는 거로 ㄱㄱ
        // 누군가가 이미 의자에 앉아 있다면. 그 의자는 bool isEmpty 라는 변수를 두어서 사람이 있으면 isEmpty = false;
        // for (int i = 0; i < 의자.Length; ++i) 로 의자를 다 둘러보고
        // if (의자[0~.isEmpty == true) 인 곳 중에서 랜덤값을 뽑고 거기로 ㄱㄱ

        Debug.Log("MoveToTable");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
