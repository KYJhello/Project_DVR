using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LM
{
    public class DeviceCanvas : BaseUI
    {
        Device device; 
        protected override void Awake()
        {
            base.Awake();
            device = GetComponentInParent<Device>();
        }
        private void OnEnable()
        {
            toggles["UpToggle"].onValueChanged.AddListener(UpToggle);
            toggles["DownToggle"].onValueChanged.AddListener(DownToggle);
            toggles["StopToggle"].onValueChanged.AddListener(StopToggle);
        }
        private void OnDisable()
        {
            toggles["UpToggle"].onValueChanged.RemoveListener(UpToggle);
            toggles["DownToggle"].onValueChanged.RemoveListener(DownToggle);
            toggles["StopToggle"].onValueChanged.RemoveListener(StopToggle);
        }
        public void UpToggle(bool isOn)
        {
            if (isOn)
                device.PlateUp();
        }
        public void DownToggle(bool isOn)
        {
            if (isOn)
                device.PlateDown();
        }
        public void StopToggle(bool isOn)
        {
            if (isOn)
                device.PlateStop();
        }
    }
}