using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace LM
{
    public class DialInteractable : XRBaseInteractable
    {
        [SerializeField] Transform dial;
        public float value;
        [SerializeField] float minAngle;
        [SerializeField] float maxAngle;

        IXRSelectInteractor interactor;
        Coroutine dialroll;

        protected override void Awake()
        {
            base.Awake();
            value = 0.5f;
            dial.transform.localRotation = Quaternion.Euler(0, 45, 0);
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            interactor = args.interactorObject;
            dialroll = StartCoroutine(DialRoll());
        }
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            StopCoroutine(dialroll);
            interactor = null;

        }
        IEnumerator DialRoll()
        {
            Vector3 startAngle = interactor.transform.rotation.eulerAngles;
            while (true)
            {
                float angle = startAngle.z + (interactor.transform.rotation.eulerAngles.z - startAngle.z);
                if (angle > maxAngle)
                {

                }
                else if (angle < minAngle)
                {

                }

                value = ((angle - minAngle) / (maxAngle - minAngle));

                dial.transform.localRotation = Quaternion.Euler(0, Mathf.Lerp(minAngle, maxAngle, value), 0);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}