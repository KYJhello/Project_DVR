using System.Collections;
using UnityEngine;

namespace LM
{
    public class BoatMover : MonoBehaviour
    {
        [SerializeField] public Transform portPos;
        [SerializeField] public Transform seaPos;
        [SerializeField] Vector3 startPosOffset = new Vector3(0, 0.5f, 0);
        [SerializeField] float moveSpeed;
        [SerializeField] GameObject portTeleportAnchor;

        public bool isHome;

        CharacterController controller;
        Collider col;
        bool isPlayerIn;

        private void Awake()
        {
            col = GetComponent<Collider>();
        }
        public void MoveToDive()
        {
            isHome = false;
            StartCoroutine(Move(1));
        }
        public void MoveToHome() 
        {
            isHome = true;
            StartCoroutine(Move(0));
        }
        IEnumerator Move(int i)
        {
            float t = 0;
            Vector3 startPos = transform.position;
            Vector3 moveDir;
            if (i == 0)
            {
                // ���߿� �Ÿ��� �ٲٱ�
                moveDir = (portPos.position - startPos).normalized;
                while(t <= 180)
                {
                    transform.position += (moveDir * moveSpeed * Time.fixedDeltaTime);
                    if (isPlayerIn && controller != null)
                    {
                        controller.Move(moveDir * moveSpeed * Time.fixedDeltaTime);
                    }
                    t += Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                }
                portTeleportAnchor.SetActive(true);
                yield break;
            }
            else if (i == 1)
            {
                moveDir = (seaPos.position - startPos).normalized;
                portTeleportAnchor.SetActive(false);
                while (t <= 180)
                {
                    transform.position += (moveDir * moveSpeed * Time.fixedDeltaTime);
                    if (isPlayerIn && controller != null)
                    {
                        controller.Move(moveDir * moveSpeed * Time.fixedDeltaTime);
                    }
                    t += Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                }
                yield break;
            }
            else
                yield break;
        }

        private void OnTriggerEnter(Collider other)
        {
            controller = other?.GetComponentInChildren<CharacterController>();
            if (controller != null)
            {
                isPlayerIn = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if(controller != null && other?.GetComponentInChildren<CharacterController>() != null)
            {
                isPlayerIn = false;
                controller = null;
            }
        }
    }
}