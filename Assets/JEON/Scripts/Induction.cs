using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Induction : XRSocketInteractor
{
    [SerializeField] Transform socketPoint;
    [SerializeField] Transform holdPoint;

    XRGrabInteractable xRGrabInteractable;

    [System.Obsolete]
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        xRGrabInteractable = args.interactable.gameObject.GetComponent<XRGrabInteractable>();

        xRGrabInteractable.attachTransform = socketPoint;

        base.OnSelectEntering(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);

        xRGrabInteractable.attachTransform = holdPoint;
    }
}
