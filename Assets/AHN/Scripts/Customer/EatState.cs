using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace AHN
{
    public class EatState : StateMachineBehaviour
    {
        StateCorutineManager corutineManger;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            corutineManger = animator.GetComponent<StateCorutineManager>();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            corutineManger.StartCoroutine(corutineManger.EatRoutine());
            animator.SetTrigger("GoOut");
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // �� �� ���̺� ������ ���ĵ��� List plateAndFoods�� ����. (����, ����, ������ũ ..)
            List<GameObject> plateAndFoods = animator.GetComponent<Customer>().mySeat.gameObject.GetComponentInChildren<FoodRecognitionOnTable>().PlateAndFood();

            foreach (GameObject plateAndFood in plateAndFoods)
            {
                if (plateAndFoods.Count <= 0)
                {
                    return;
                }    

                // ����
                if (plateAndFood.layer == 23)   // ���� ���� �� �ʹ��� �ִٸ�
                {
                    int myScore = plateAndFood.gameObject.GetComponent<SushiInfo>().sushiScore;    // �� �ʹ��� ������ �޾ƿ�

                    // ���� �ֹ��� ����Ⱑ �´��� Ȯ��
                    if (OrderState.fishInfo[0] == plateAndFood.gameObject.GetComponent<SushiInfo>().fishName)
                    {
                        PosManager.OnAddPayEvent?.Invoke(myScore);
                    }
                    else    // �ƴ϶��
                    {
                        myScore = 0;    // ���� ����
                        PosManager.OnAddPayEvent?.Invoke(myScore);
                    }

                    Destroy(plateAndFood);      // ���̺� ���� �÷��� ���� �� �ʹ� ������
                }
                else if (plateAndFood.layer == 18)      // ���� ���� �� ������ũ�� �ִٸ�
                {
                    int myScore = plateAndFood.gameObject.GetComponent<SteakInfo>().steakScore;    // ������ũ�� ������ �޾ƿ�

                    // ���� �ֹ��� ����Ⱑ �´��� Ȯ��
                    if (OrderState.fishInfo[0] == plateAndFood.gameObject.GetComponent<SteakInfo>().fishName) 
                    {
                        PosManager.OnAddPayEvent?.Invoke(myScore);
                    }
                    else    // �ƴ϶��
                    {
                        myScore = 0;    // ���� ����
                        PosManager.OnAddPayEvent?.Invoke(myScore);
                    }

                    Destroy(plateAndFood);
                }
                else
                {
                    animator.GetComponent<Customer>().mySeat.gameObject.GetComponentInChildren<FoodRecognitionOnTable>().IsPlateFalse();
                    Destroy(plateAndFood);      // ���̺� ���� �÷��� ���� �� �ʹ� ������
                }
            }
            plateAndFoods.Clear();
        }
    }
}