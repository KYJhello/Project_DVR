using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit;

namespace LM
{
    public class HarpoonGun : MonoBehaviour
    {
        [SerializeField] Transform spearSocket;
        [SerializeField] float rayLineWidth;
        [SerializeField] LayerMask harpoonGunRayCastMask;
        [SerializeField] GameObject objectRope;
        [SerializeField] Collider gunUpBodyCollider;

        enum RoadedSpearType { None, Attack, Return, Count };

        public float spearForce;
        public bool canPull;

        RoadedSpearType rsType;
        XRSocketInteractor socketInteractor;
        XRGrabInteractable grabInteractable;
        HarpoonSpear roadedSpear;
        ReturnSpear returnSpear;
        AttackSpear attackSpear;
        LineRenderer lineRenderer;
        Coroutine renderRay;
        NoneGravityRope rope;

        private void Awake()
        {
            socketInteractor = spearSocket.gameObject.GetComponent<XRSocketInteractor>();
            grabInteractable = GetComponentInChildren<XRGrabInteractable>();
            lineRenderer = GetComponent<LineRenderer>();
            rope = GetComponentInChildren<NoneGravityRope>();

            lineRenderer.enabled = false;

            rsType = RoadedSpearType.None;
        }
        private void OnEnable()
        {
            socketInteractor?.selectEntered?.AddListener(OnSpearLoad);
            socketInteractor?.selectExited?.AddListener(OnSpearOut);

            grabInteractable?.selectEntered?.AddListener(GrabOn);
            grabInteractable?.selectExited?.AddListener(GrabOff);
            grabInteractable?.activated?.AddListener(TriggerOn);
            grabInteractable?.deactivated?.AddListener(TriggerOff);
        }
        private void OnDisable()
        {
            socketInteractor?.selectEntered?.RemoveListener(OnSpearLoad);
            socketInteractor?.selectExited?.RemoveListener(OnSpearOut);

            grabInteractable?.selectEntered?.RemoveListener(GrabOn);
            grabInteractable?.selectExited?.RemoveListener(GrabOff);
            grabInteractable?.activated?.RemoveListener(TriggerOn);
            grabInteractable?.deactivated?.RemoveListener(TriggerOff);
        }

        public void GrabOn(SelectEnterEventArgs args)
        {
            lineRenderer.enabled = true;
            renderRay = StartCoroutine(RenderRay());
        }
        public void GrabOff(SelectExitEventArgs args)
        {
            lineRenderer.enabled = false;
            StopCoroutine(renderRay);
        }

        public void OnSpearLoad(SelectEnterEventArgs args)
        {
            roadedSpear = args.interactableObject.transform?.GetComponent<HarpoonSpear>();
            attackSpear = roadedSpear?.GetComponent<AttackSpear>();
            returnSpear = roadedSpear?.GetComponent<ReturnSpear>();
            if (attackSpear != null)
            {
                rsType = RoadedSpearType.Attack;

                gunUpBodyCollider.isTrigger = true;
                canPull = false;
                rope.RopeOff();
            }
            else
            {
                rsType = RoadedSpearType.Return;
                returnSpear.OnEndAll();
                gunUpBodyCollider.isTrigger = true;
                returnSpear.isPulling = false;
                returnSpear.returnPos = spearSocket;
                canPull = false;
                rope.RopeOff();
                objectRope.gameObject.SetActive(true);
            }
        }
        public void OnSpearOut(SelectExitEventArgs args)
        {
            gunUpBodyCollider.isTrigger = false;
            objectRope.gameObject.SetActive(false);
            if (rsType == RoadedSpearType.Attack)
                rsType = RoadedSpearType.None;
        }
        public void TriggerOn(ActivateEventArgs args)
        {
            Debug.Log($"Trigger, rsType : {rsType}");
            if (socketInteractor.hasSelection && !canPull)
            {
                // 발사음성 출력
                if (rsType == RoadedSpearType.Attack)
                {
                    Debug.Log("Fire");
                    StartCoroutine(SocketTrigger());
                    attackSpear.OnFire(spearSocket.forward, spearForce);
                }
                else if (rsType == RoadedSpearType.Return)
                {
                    rope.endPos = returnSpear.ropePos;
                    rope.RopeOn();
                    StartCoroutine(SocketTrigger());
                    StartCoroutine(CanPullTrigger());
                    returnSpear.pullEnd = false;
                    objectRope.gameObject.SetActive(false);
                    returnSpear.OnFire(spearSocket.forward, spearForce);
                }
            }
            else if (canPull)
            {
                // 당기는 음성 실행
                returnSpear.OnPullStart();
            }
            else
            {
                // 빈 찰칵 소리
            }
            gunUpBodyCollider.isTrigger = true;

        }
        public void TriggerOff(DeactivateEventArgs args)
        {
            if (canPull)
            {
                // 당기는 음성 종료
                returnSpear.OnPullEnd();
            }
            else if (!socketInteractor.hasSelection)
                gunUpBodyCollider.isTrigger = false;
        }

        IEnumerator RenderRay()
        {
            RaycastHit hit;
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = rayLineWidth;
            lineRenderer.endWidth = rayLineWidth;
            while (true)
            {
                lineRenderer.SetPosition(0, spearSocket.position);
                if (Physics.Raycast(spearSocket.position, spearSocket.forward, out hit, spearForce, harpoonGunRayCastMask))
                    lineRenderer.SetPosition(1, hit.point);
                else
                    lineRenderer.SetPosition(1, spearSocket.position + spearSocket.forward * spearForce);
                yield return new WaitForFixedUpdate();
            }
        }
        IEnumerator SocketTrigger()
        {
            socketInteractor.socketActive = false;
            yield return new WaitForSeconds(1);
            socketInteractor.socketActive = true;
        }
        IEnumerator CanPullTrigger()
        {
            yield return new WaitForSeconds(1);
            canPull = true;
        }
    }
}