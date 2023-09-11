using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace LM
{
    public class BoatMover : MonoBehaviour
    {
        [SerializeField] Vector3 portPos;
        [SerializeField] Vector3 seaPos;
        [SerializeField] Vector3 startPosOffset = new Vector3(0, 0.5f, 0);

        private void Awake()
        {
            
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
            if (i == 0)
            {
                while(t <= 10)
                {
                    transform.position = Vector3.Lerp(startPos, portPos, t * 0.1f);
                    t += Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                }
                yield break;
            }
            else if (i == 1)
            {
                while (t <= 10)
                {
                    transform.position = Vector3.Lerp(startPos, seaPos, t * 0.1f);
                    t += Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                }
                yield break;
            }
            else
                yield break;
        }
    }
}