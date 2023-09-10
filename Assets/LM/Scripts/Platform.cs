using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class Platform : MonoBehaviour
    {
        Coroutine move;
        public void Up(float speed)
        {
            StopCoroutine(move);
            move = StartCoroutine(Move(speed, Vector3.up));
        }
        public void Down(float speed) 
        {
            StopCoroutine(move);
            move = StartCoroutine (Move(speed, Vector3.down));
        }
        public void Stop()
        {
            StopCoroutine(move);
        }
        IEnumerator Move(float speed, Vector3 dir)
        {
            while(transform.position.y < 0 && transform.position.y > -100)
            {
                transform.Translate(dir * speed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}