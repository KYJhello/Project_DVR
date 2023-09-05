using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bell : MonoBehaviour
{
    public Transform visualTarget;
    public Vector3 localAxis;

    private Vector3 offset;
    private Transform pokeAttechTransform;

    private XRBaseInteractable interactable;

    private bool isFollowing = false;

    private void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
    }

    private void Follow(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;
            isFollowing = true;

            pokeAttechTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttechTransform.position;
        }
    }

    private void Update()
    {
        if (isFollowing)
        {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttechTransform.position + offset); // 
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis); // Vector3.Project 수직으로 투영한 결과 벡터를 반환합니다. 이렇게 얻은 벡터는 normal방향으로만 미치는 힘을 나타냅니다.

            visualTarget.position = pokeAttechTransform.position + offset;
        }
    }

}
