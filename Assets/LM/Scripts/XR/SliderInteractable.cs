using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace LM
{
    public class SliderInteractable : XRBaseInteractable
    {
        [SerializeField] Transform slider;
        public float value;
        [SerializeField] float min;
        [SerializeField] float max;

        IXRSelectInteractor interactor;
        Coroutine handlePosUpdate;
        protected override void Awake()
        {
            base.Awake();
            value = 0.5f;
            slider.transform.localPosition = Vector3.zero;
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            interactor = args.interactorObject;
            handlePosUpdate = StartCoroutine(UpdateHandlePos());
        }
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            StopCoroutine(handlePosUpdate);
            interactor = null;
        }

        IEnumerator UpdateHandlePos()
        {
            while (true)
            {
                Vector3 localPos = transform.InverseTransformPoint(interactor.GetAttachTransform(this).position);
                value = Mathf.Clamp((localPos.z - min) / (max - min), 0, 1);

                slider.transform.localPosition = new Vector3(0, 0, Mathf.Lerp(min, max, value));
                yield return new WaitForEndOfFrame();
            }
        }
    }
}