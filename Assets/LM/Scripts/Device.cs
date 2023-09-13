using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace LM
{
    public class Device : MonoBehaviour
    {
        public Platform platform;

        XRGrabInteractable interactable;
        public bool isActive;
        SliderInteractable slider;
        DialInteractable dial;
        DeviceCanvas canvas;

        private void Awake()
        {
            interactable = GetComponent<XRGrabInteractable>();
            slider = GetComponentInChildren<SliderInteractable>();
            dial = GetComponentInChildren<DialInteractable>();
            canvas = GetComponentInChildren<DeviceCanvas>();
            isActive = false;
        }
        private void OnEnable()
        {
            interactable.selectEntered.AddListener(FindPlate);
            
        }
        private void OnDisable()
        {
            interactable.selectEntered.RemoveListener(FindPlate);
        }
        private void FindPlate(SelectEnterEventArgs args)
        {
            if(platform != null && platform.gameObject.activeSelf)
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
        }

        public void PlateUp()
        {
            if(isActive)
                platform.Up(slider.value * ((dial.value * 2) + 1));
        }
        public void PlateDown()
        {
            if(isActive)
                platform.Down(slider.value * ((dial.value * 2) + 1));
        }
        public void PlateStop()
        {
            if(isActive)
                platform.Stop();
        }
    }
}
