using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace LM
{
    public class Diver : MonoBehaviour
    {
        [SerializeField] Canvas HUD;

        public UnityEvent OnDived;
        public UnityEvent OnDiveEnded;
        public UnityEvent OnChangeWeight;

        Coroutine BreathRoutine;
        Vector3 prevPos;
        bool isDived;

        public float MaxO2 { get; set; }
        public float CurO2 { get; set; }
        public float MaxWeight { get; set; }
        public float CurWeight { get; set; }
        public float Depth { get; set; }

        private void Awake()
        {
            isDived = false;
            MaxO2 = 60;
            CurO2 = MaxO2;
            MaxWeight = 80;
            CurWeight = 0;
            Depth = 0;
        }
        private void OnEnable()
        {
            
        }
        private void OnDisable()
        {
            
        }

        private void Update()
        {
            if (!isDived && prevPos.y > transform.position.y && transform.position.y < 0)
                OnDive();
            else if (isDived && prevPos.y < transform.position.y && transform.position.y > 0)
                OnDiveEnd();
            
            if(transform.position.y < 0)
                Depth = transform.position.y * 20;
            else
                Depth = 0;
        }

        private void FixedUpdate()
        {
            prevPos = transform.position;
        }



        private void OnDive()
        {
            if (!isDived)
            {
                OnDived?.Invoke();
                isDived = true;
                BreathRoutine = StartCoroutine(Breath());
            }
        }
        private void OnDiveEnd()
        {
            if(isDived)
            {
                OnDiveEnded?.Invoke();
                isDived = false;
                StopCoroutine(BreathRoutine);
                CurO2 = MaxO2;
            }
                
        }

        IEnumerator Breath()
        {
            while(CurO2 <= MaxO2)
            {
                CurO2 -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}