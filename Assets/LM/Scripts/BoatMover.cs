using System.Collections;
using UnityEngine;

namespace LM
{
    public class BoatMover : MonoBehaviour
    {
        [SerializeField] Vector3 portPos;
        [SerializeField] Vector3 seaPos;
        [SerializeField] Vector3 startPosOffset = new Vector3(0, 0.5f, 0);
        [SerializeField] float moveSpeed;
        [SerializeField] GameObject portTeleportAnchor;

        CharacterController controller;
        Collider col;
        bool isPlayerIn;

        private void Awake()
        {
            col = GetComponent<Collider>();
        }
        public void MoveToDive()
        {
            StartCoroutine(Move(1));
        }
        public void MoveToHome() 
        {
            StartCoroutine(Move(0));
        }
        IEnumerator Move(int i)
        {
            float t = 0;
            Vector3 startPos = transform.position;
            Vector3 moveDir;
            if (i == 0)
            {
                // 나중에 거리로 바꾸기
                moveDir = (portPos - startPos).normalized;
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
                moveDir = (seaPos - startPos).normalized;
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