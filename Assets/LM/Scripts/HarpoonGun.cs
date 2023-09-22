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

        public int Level { get; set; }
        public bool OnSocket { get; set; }
        enum RoadedSpearType { None, Attack, Return, Count };

        public float spearForce = 10;
        public bool canPull;
        public float pullForce = 3;
        public float maxRange = 5;
        public int damage;

        RoadedSpearType rsType;
        XRSocketInteractor socketInteractor;
        XRGrabInteractable grabInteractable;
        HarpoonSpear roadedSpear;
        ReturnSpear returnSpear;
        AttackSpear attackSpear;
        LineRenderer lineRenderer;
        Coroutine renderRay;
        NoneGravityRope rope;
        Transform playerPos;

        private void Awake()
        {
            socketInteractor = spearSocket.gameObject.GetComponent<XRSocketInteractor>();
            grabInteractable = GetComponentInChildren<XRGrabInteractable>();
            lineRenderer = GetComponent<LineRenderer>();
            rope = GetComponentInChildren<NoneGravityRope>();

            lineRenderer.enabled = false;
            Level = 0;
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
            if(args.interactorObject.transform.parent.transform.parent.GetComponentInChildren<Diver>() == null)
                return;
            // Level = 
            lineRenderer.enabled = true;
            renderRay = StartCoroutine(RenderRay());
            switch(Level)
            {
                case 0:
                    spearForce = 10;
                    pullForce = 3;
                    maxRange = 5;
                    damage = 3;
                    break;
                case 1:
                    spearForce = 15;
                    pullForce = 5;
                    maxRange = 10;
                    damage = 5;
                    break;
                case 2:
                    spearForce = 20;
                    pullForce = 8;
                    maxRange = 15;
                    damage = 8;
                    break;
                default:
                    spearForce = 25;
                    pullForce = 10;
                    maxRange = 20;
                    damage = 10;
                    break;
            }
            playerPos = args.interactorObject.transform;
        }
        public void GrabOff(SelectExitEventArgs args)
        {
            if (args.interactorObject.transform.parent.transform.parent.GetComponentInChildren<Diver>() == null)
                return;
            lineRenderer.enabled = false;
            StopCoroutine(renderRay);
            StartCoroutine(Return());
        }

        public void OnSpearLoad(SelectEnterEventArgs args)
        {
            GameManager.Sound.Play("Sounds/Handling_Gun_01_Clip_In_SFX");
            roadedSpear = args.interactableObject.transform?.GetComponent<HarpoonSpear>();
            attackSpear = roadedSpear?.GetComponent<AttackSpear>();
            returnSpear = roadedSpear?.GetComponent<ReturnSpear>();
            if (attackSpear != null)
            {
                rsType = RoadedSpearType.Attack;
                gunUpBodyCollider.isTrigger = true;
                canPull = false;
                rope.RopeOff();
                attackSpear.maxRange = maxRange;
                attackSpear.damage = damage * 2;
            }
            else
            {
                rsType = RoadedSpearType.Return;
                returnSpear.OnEndAll();
                gunUpBodyCollider.isTrigger = true;
                returnSpear.isPulling = false;
                returnSpear.returnPos = spearSocket;
                canPull = false;
                returnSpear.pullForce = pullForce;
                returnSpear.maxRange = maxRange;
                returnSpear.damage = damage;
                rope.RopeOff();
                objectRope.gameObject.SetActive(true);
            }
        }
        public void OnSpearOut(SelectExitEventArgs args)
        {
            GameManager.Sound.Play("Sounds/Handling_Gun_01_Clip_Out_SFX");
            gunUpBodyCollider.isTrigger = false;
            objectRope.gameObject.SetActive(false);
            if (rsType == RoadedSpearType.Attack)
                rsType = RoadedSpearType.None;
        }
        public void TriggerOn(ActivateEventArgs args)
        {
            gunUpBodyCollider.isTrigger = true;
            if (socketInteractor.hasSelection && !canPull)
            {
                // 발사음성 출력
                if (rsType == RoadedSpearType.Attack)
                {
                    StartCoroutine(SocketTrigger());
                    attackSpear.rb.useGravity = false;
                    GameManager.Sound.Play("Sounds/Flare gun 5-2");
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
                    returnSpear.rb.useGravity = false;
                    GameManager.Sound.Play("Sounds/Pistol_01_Mountain_Tail_SFX");
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
                GameManager.Sound.Play("Sounds/Handling_Gun_01_Arming_SFX", Define.Sound.Effect, 1.3f);
                // 빈 찰칵 소리
            }
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
        IEnumerator Return()
        {
            if (OnSocket)
                yield break;
            while(Vector3.Distance(transform.position, playerPos.position) > 100)
            {
                if (OnSocket)
                    yield break;
                // 소켓으로 복귀
                yield return new WaitForSeconds(1);
            }
        }

    }
}