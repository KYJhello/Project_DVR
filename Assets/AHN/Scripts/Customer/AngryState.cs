using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class AngryState : StateMachineBehaviour
    {
        StateCorutineManager corutineManager;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            corutineManager = animator.GetComponent<StateCorutineManager>();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // if (�� ���̺� ������ �÷����ٸ�)
            if (animator.GetComponent<Customer>().mySeat.gameObject.GetComponentInChildren<FoodRecognitionOnTable>().IsPlate())
            {
                Debug.Log("Angry -> Eat");
                animator.SetTrigger("Eat");
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // TODO : Ÿ�̸� �ð� ���ҽ��Ѿ� ��. �ϴ� -10�ʷ�
            
            // ��ٸ��� �ڷ�ƾ Stope
            corutineManager.StopCoroutine(corutineManager.FoodWaitRoutine());

            // (2) Ƚ �� count-- ����ٸ� �ٽ� count++ �ؾ���.   
            // if (animator.GetCom<OrderState>().bool �Լ��� ã�Ƽ� ��ù̸� �̿��� bool�� true��, foreach�� ���� �ֹ��� ������ ã�Ƽ� ������ ������ ���������
            if (animator.GetComponent<OrderState>().isUseSasimi)
            {
                // foreach�� ���� �ֹ��� ������ ã�Ƽ� ������ ������ ���������
                foreach (List<string> innerSushiCounts in MenuManager.sasimiCounts)     // �߶���� Ƚ ������ ����Ʈ�� �ѷ�����
                {
                    if (innerSushiCounts[0] == OrderState.fishInfo[0])     // ���� �߷��ִ� ȸ�� ����Ʈ �߿� �ֹ��� ȸ�� �̸��� �ִٸ�,
                    {
                        if (int.Parse(innerSushiCounts[1]) > 0)     // Ƚ ������ �ִٸ�
                        {
                            int count = int.Parse(innerSushiCounts[1]);

                            count++;
                            innerSushiCounts[1] = count.ToString();
                            Debug.Log("count++");

                            break;
                        }
                    }
                }
            }
            // �ֹ��ߴ� ����⸦ (1)����Ʈ���� RemoveAt�ߴٸ� �ٽ� Add ���ְ�
            if (animator.GetComponent<OrderState>().isUseNewFish == true)
            {
                // �ֹ��� ����� �ε����� �޾ƿͼ� Add����Ʈ(�ε���) ����
                Debug.Log("Add orderfishindex");
                MenuManager.fishs.Add(OrderState.fishInfo);
            }
            
        }
    }
}