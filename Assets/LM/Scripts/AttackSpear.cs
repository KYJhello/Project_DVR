using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace LM
{
    public class AttackSpear : HarpoonSpear
    {
        [SerializeField] Transform castPos;
        [SerializeField] LayerMask mask;

        public Rigidbody rb;
        XRGrabInteractable interactable;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            interactable = GetComponent<XRGrabInteractable>();
        }
        private void OnEnable()
        {
            interactable.selectExited.AddListener(GrabOutCheck);
        }
        private void OnDisable()
        {
            interactable.selectExited.RemoveListener(GrabOutCheck);
        }
        public override void OnFire(Vector3 dir, float force)
        {
            rb.useGravity = false;
            StartCoroutine(FireRoutine(dir, force));
        }
        IEnumerator FireRoutine(Vector3 dir, float speed)
        {
            RaycastHit hit;
            Vector3 startPos = transform.position;
            rb.useGravity = false;
            rb.isKinematic = false;
            Vector3 v3 = dir.normalized;
            rb.AddForce(v3 * speed);
            while (Vector3.Distance(startPos, transform.position) < maxRange * 2)
            {
                Debug.Log("Firing...");
                rb.useGravity = false;
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
        private void GrabOutCheck(SelectExitEventArgs args)
        {
            HarpoonGun gun = args.interactorObject.transform.GetComponentInParent<HarpoonGun>();
            if (gun != null)
                rb.useGravity = false;
            else rb.useGravity = true;
        }
    }
}