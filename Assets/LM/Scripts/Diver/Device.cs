using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace LM
{
    public class Device : MonoBehaviour
    {
        public Platform platform;

        public bool isActive;
        SliderInteractable slider;
        DialInteractable dial;
        DeviceCanvas canvas;

        private void Awake()
        {
            slider = GetComponentInChildren<SliderInteractable>();
            dial = GetComponentInChildren<DialInteractable>();
            canvas = GetComponentInChildren<DeviceCanvas>();
            isActive = false;
        }
        private void OnEnable()
        {
            FindPlatform();
        }

        public void FindPlatform()
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

        public void PlatformUp()
        {
            if(isActive)
                platform.Up(slider.value * ((dial.value * 2) + 1));
        }
        public void PlatformDown()
        {
            if(isActive)
                platform.Down(slider.value * ((dial.value * 2) + 1));
        }
        public void PlatformStop()
        {
            if(isActive)
                platform.Stop();
        }
    }
}
