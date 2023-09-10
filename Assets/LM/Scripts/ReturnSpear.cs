using KIM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace LM
{
    public class ReturnSpear : HarpoonSpear
    {
        [SerializeField] LayerMask mask;

        XRSocketInteractor socketInteractor;
        Collider col;

        public Transform ropePos;
        public Transform returnPos;
        public float pullForce = 3;

        public bool isPulling;
        public bool pullEnd;

        protected override void Awake()
        {
            base.Awake();
            col = GetComponent<Collider>();
            socketInteractor = GetComponentInChildren<XRSocketInteractor>();
        }
        private void OnEnable()
        {
            socketInteractor.hoverEntered.AddListener(CheckFish);
            socketInteractor.selectEntered.AddListener(GetFish);
            socketInteractor.selectExited.AddListener(RemoveGetFish);
        }
        private void OnDisable()
        {
            socketInteractor.hoverEntered?.RemoveListener(CheckFish);
            socketInteractor.selectEntered?.RemoveListener(GetFish);
            socketInteractor.selectExited?.AddListener(RemoveGetFish);
        }

        public void CheckFish(HoverEnterEventArgs args)
        {
            Fish fish = args.interactableObject.transform.gameObject?.GetComponent<Fish>();
            if (fish != null)
            {
                if (true)
                {
                    //만약 체력이 남았으면 데미지 주기
                    socketInteractor.allowSelect = false;
                }
                else
                {
                    //아니라면
                    socketInteractor.allowSelect = true;
                }
            }
        }
        public void GetFish(SelectEnterEventArgs args)
        {
            col.isTrigger = true;
        }
        public void RemoveGetFish(SelectExitEventArgs args)
        {
            col.isTrigger = false;
        }

        public override void OnFire(Vector3 dir, float force)
        {
            socketInteractor.allowSelect = false;
            isPulling = false;
            pullEnd = false;
            // 계속 레이캐스트 하다가 최대사거리 도달/ 충돌/ 물고기 잡힘 중 하나가 확인되면 돌아오기
            // 혹은 직접 당기기
            StartCoroutine(FireRoutine(dir, force));
            StartCoroutine(FishCast());
            StartCoroutine(Return());
        }
        public void OnPullStart()
        {
            isPulling = true;
            rb.velocity = Vector3.zero;
        }
        public void OnPullEnd()
        {
            isPulling = false;
        }
        public void OnEndAll()
        {
            pullEnd = true;
        }
        IEnumerator FireRoutine(Vector3 dir, float speed)
        {
            rb.useGravity = false;
            rb.isKinematic = false;
            Vector3 v3 = dir.normalized;
            float dis = Vector3.Distance(returnPos.position, transform.position);
            transform.rotation = returnPos.rotation;
            while (dis < maxRange)
            {
                if (isPulling || pullEnd)
                    yield break;
                rb.velocity = v3 * speed;
                dis = Vector3.Distance(returnPos.position, transform.position);
                transform.rotation = returnPos.rotation;
                yield return new WaitForFixedUpdate();
            }
            rb.velocity = Vector3.zero;
        }
        IEnumerator Return()
        {
            while (!pullEnd)
            {
                if (isPulling)
                {
                    Vector3 dir = returnPos.position - transform.position;
                    transform.Translate(dir.normalized * pullForce * Time.fixedDeltaTime, Space.World);
                    transform.LookAt(dir.normalized * maxRange * -1);
                }
                yield return new WaitForFixedUpdate();
            }
        }
        IEnumerator FishCast()
        {
            RaycastHit hit;
            while (!pullEnd && !isPulling)
            {
                if (Physics.SphereCast(socketInteractor.gameObject.transform.position, 0.1f, transform.forward, out hit, 0, mask))
                {
                    Fish fish = hit.collider.gameObject.GetComponent<Fish>();
                    if (fish != null)
                    {
                        if (false)
                        {
                            //만약 체력이 남았으면 데미지 주기
                            socketInteractor.allowSelect = false;
                            rb.AddForce(hit.point * 5, ForceMode.Impulse);
                        }
                        else
                        {
                            //아니라면
                            socketInteractor.allowSelect = true;
                        }
                    }
                    yield break;
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
}