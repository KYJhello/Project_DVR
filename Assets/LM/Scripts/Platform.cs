using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class Platform : MonoBehaviour
    {
        Coroutine move;
        CharacterController player;
        private void Awake()
        {
            player = FindAnyObjectByType<CharacterController>();
        }
        private void OnEnable()
        {
            if(player == null)
                player = FindAnyObjectByType<CharacterController>();
        }
        public void Up(float speed)
        {
            if (move != null)
                StopCoroutine(move);
            move = StartCoroutine(Move(speed, Vector3.up));
        }
        public void Down(float speed) 
        {
            if (move != null)
                StopCoroutine(move);
            move = StartCoroutine (Move(speed, Vector3.down));
        }
        public void Stop()
        {
            if (move != null)
                StopCoroutine(move);
        }
        IEnumerator Move(float speed, Vector3 dir)
        {
            while(transform.position.y < 0 && transform.position.y > -100)
            {
                transform.position += (dir * speed * Time.fixedDeltaTime);
                player.Move(dir * speed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}