using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace LM
{
    [RequireComponent(typeof(Rigidbody))]
    public class RopeInteractable : XRBaseInteractable
    {
        [SerializeField] public Transform ropes;

        [SerializeField] RopeProvider ropeProvider;

        [SerializeField] float maxInteractionDistance;

        protected override void Awake()
        {
            base.Awake();
            if (ropeProvider == null)
                GameObject.Find("RopeMove");
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }
        protected override void OnDisable()
        {
            base.OnDisable();
        }

        public override bool IsHoverableBy(IXRHoverInteractor interactor)
        {
            return base.IsHoverableBy(interactor) && GetDistanceSqrToInteractor(interactor) <= maxInteractionDistance * maxInteractionDistance;
        }
        public override bool IsSelectableBy(IXRSelectInteractor interactor)
        {
            return base.IsSelectableBy(interactor) && (IsSelected(interactor) || GetDistanceSqrToInteractor(interactor) <= maxInteractionDistance * maxInteractionDistance);
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            if(ropeProvider != null)
            {
                ropeProvider.RopeGrab(this, args.interactorObject);
            }
            else
            {
                ropeProvider = args.interactorObject.transform?.GetComponent<RopeProvider>();
                if (ropeProvider != null)
                {
                    ropeProvider.RopeGrab(this, args.interactorObject);
                }
            }
        }
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            if(ropeProvider != null)
            {
                ropeProvider.RopeOff(args.interactorObject);
            }
        }
        protected override void Reset()
        {
            base.Reset();
        }
    }
}