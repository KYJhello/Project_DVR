using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrashCover : MonoBehaviour
{
    XRGrabInteractable grab;

    Transform maxTransform;

    private void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        maxTransform = GetComponent<Transform>();
    }
}
