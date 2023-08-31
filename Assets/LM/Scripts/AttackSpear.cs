using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class AttackSpear : HarpoonSpear
    {
        [SerializeField] Transform castPos;
        [SerializeField] LayerMask mask;

        protected override void Awake()
        {
            base.Awake();
        }

        public override void OnFire(Vector3 dir, float force)
        {
            StartCoroutine(FireRoutine(dir, force));
        }
        IEnumerator FireRoutine(Vector3 dir, float speed)
        {
            RaycastHit hit;
            Vector3 startPos = transform.position;
            rb.useGravity = false;
            rb.isKinematic = false;
            Vector3 v3 = dir.normalized;
            while (Vector3.Distance(startPos, transform.position) < maxRange * 2)
            {
                Debug.Log("Firing...");
                rb.velocity = v3 * speed;
                transform.LookAt(v3 * maxRange * 2);
                if (Physics.SphereCast(castPos.position, 0.05f, transform.forward, out hit, 0.1f, mask))
                {
                    rb.AddForce(hit.normal * 10, ForceMode.Impulse);
                    yield break;
                }
                yield return new WaitForFixedUpdate();
            }
            rb.velocity = Vector3.zero;
            Debug.Log("End");
        }
    }
}