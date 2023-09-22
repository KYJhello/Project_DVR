using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Induction : XRSocketInteractor
{
    // 소켓되는 포인트를 변경하기위해서 XRGrabInteractable을 재정의해서 소켓이 될 당시의 위치를 
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
