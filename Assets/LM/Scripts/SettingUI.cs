using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class SettingUI : BaseUI
    {
        protected override void Awake()
        {
            base.Awake();
            sliders["MasterVolumeSlider"].maxValue = 100;
            sliders["MasterVolumeSlider"].minValue = 0;
            sliders["MasterVolumeSlider"].value = 50;
            texts["MasterVolumeCurValueText"].text = ((int)sliders["MasterVolumeSlider"].value).ToString();
            sliders["SFXVolumeSlider"].maxValue = 100;
            sliders["SFXVolumeSlider"].minValue = 0;
            sliders["SFXVolumeSlider"].value = 50;
            texts["SFXVolumeCurValueText"].text = ((int)sliders["SFXVolumeSlider"].value).ToString();
            sliders["BGMVolumeSlider"].maxValue = 100;
            sliders["BGMVolumeSlider"].minValue = 0;
            sliders["BGMVolumeSlider"].value = 50;
            texts["BGMVolumeCurValueText"].text = ((int)sliders["BGMVolumeSlider"].value).ToString();
        }

        private void OnEnable()
        {
            sliders["MasterVolumeSlider"].onValueChanged.AddListener(OnMasterVolumeChanged);
            sliders["SFXVolumeSlider"].onValueChanged.AddListener(OnSFXVolumeChanged);
            sliders["BGMVolumeSlider"].onValueChanged.AddListener(OnBGMVolumeChanged);
        }
        private void OnDisable()
        {
            sliders["MasterVolumeSlider"].onValueChanged.RemoveListener(OnMasterVolumeChanged);
            sliders["SFXVolumeSlider"].onValueChanged.RemoveListener(OnSFXVolumeChanged);
            sliders["BGMVolumeSlider"].onValueChanged.RemoveListener(OnBGMVolumeChanged);
        }
        private void OnMasterVolumeChanged(float value)
        {
            texts["MasterVolumeCurValueText"].text = ((int)value).ToString();
            float sound;
            if(value <= 0)
                sound = -80;
            else
                sound = Mathf.Lerp(-40, 0, value * 0.01f);
        }
        private void OnSFXVolumeChanged(float value)
        {
            texts["SFXVolumeCurValueText"].text = ((int)value).ToString();
            float sound;
            if (value <= 0)
                sound = -80;
            else
                sound = Mathf.Lerp(-40, 0, value * 0.01f);
        }
        private void OnBGMVolumeChanged(float value)
        {
            texts["BGMVolumeCurValueText"].text = ((int)value).ToString();
            float sound;
            if (value <= 0)
                sound = -80;
            else
                sound = Mathf.Lerp(-40, 0, value * 0.01f);
        }
        public float MasterVolume { get { return sliders["MasterVolumeSlider"].value; } set { sliders["MasterVolumeSlider"].value = value; } }

        public float SFXVolume { get { return sliders["SFXVolumeSlider"].value; } set { sliders["SFXVolumeSlider"].value = value; } }
        public float BGMVolume { get { return sliders["BGMVolumeSlider"].value; } set { sliders["BGMVolumeSlider"].value = value; } }
    }
}
