using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace LM
{
    public class DiverHelmetHUD : BaseUI
    {
        Diver diver;
        Coroutine updateRoutine;
        protected override void Awake()
        {
            base.Awake();
            diver = GameObject.Find("XR Origin (XR Rig)")?.GetComponentInChildren<Diver>();
        }
        private void OnEnable()
        {
            Color startColor = new Color(0, 1, 0, 1);

            images["O2SliderBGEdge"].color = startColor;
            images["O2SliderEdge"].color = startColor;

            images["O2Slider"].fillAmount = 1;

            texts["O2Text"].text = ((int)diver.MaxO2).ToString();

            texts["DepthText"].text = "0m";
            texts["DepthText"].enabled = false;
            images["DepthBG"].enabled = false;
            
            images["DepthSliderBG"].color = new Color(0, 1, 1);
            transforms["CurDepthPos"].anchoredPosition = new Vector2(transforms["CurDepthPos"].anchoredPosition.x, 250);
            transforms["DepthSlider"].gameObject.SetActive(false);

            float weight = diver.CurWeight / diver.MaxWeight;
            Color weightColor;

            images["WeightSlider"].fillAmount = weight;
            if(weight <= 0.5f)
            {
                weightColor = new Color(weight * 2, 1, 0);
            }
            else
            {
                weightColor = new Color(1, 1 - (weight - 0.5f) * 2, 0);
            }
            images["WeightSlider"].color = weightColor;

            texts["CurWeightNum"].color = weightColor;
            texts["CurWeightNum"].text = ((int)diver.CurWeight).ToString();
            texts["MaxWeightNum"].color = weightColor;
            texts["MaxWeightNum"].text = ((int)diver.MaxWeight).ToString();

            texts["CurWeightText"].color = weightColor;
            texts["MaxWeightText"].color = weightColor;
            texts["WeightText"].color = weightColor;
            texts["MaxKg"].color = weightColor;
            texts["CurKg"].color = weightColor;

            // 플레이어에 '잠수' 이벤트 만들고 거기에 에드리스너 하기
            diver.OnDived.AddListener(Dive);
            diver.OnDiveEnded.AddListener(DiveEnd);
        }
        private void OnDisable()
        {
            diver.OnDived.RemoveListener(Dive);
            diver.OnDiveEnded.RemoveListener(DiveEnd);
        }


        private void Dive()
        {
            updateRoutine = StartCoroutine(HUDUpdate());
        }
        private void DiveEnd()
        {
            StopCoroutine(updateRoutine);
            StartCoroutine(ResetRoutine());
        }
        private void ChangeWeight()
        {
            StartCoroutine(WeightChangeRoutine());
        }

        IEnumerator HUDUpdate()
        {
            texts["DepthText"].enabled = true;
            images["DepthBG"].enabled = true;
            transforms["DepthSlider"].gameObject.SetActive(true);
            float tic = (1 / diver.MaxO2) * Time.fixedDeltaTime;
            Color o2Color = new Color(0, 1, 0, 1);
            while (true)
            {
                if (o2Color.r < 1)
                {
                    o2Color = new Color(o2Color.r + (tic * 2), 1, 0);
                }
                else
                {
                    o2Color = new Color(1, o2Color.g - (tic * 2), 0);
                }
                images["O2SliderBGEdge"].color = o2Color;
                images["O2SliderEdge"].color = o2Color;

                images["O2Slider"].fillAmount -= tic;
                texts["O2Text"].text = ((int)diver.CurO2).ToString();

                transforms["CurDepthPos"].anchoredPosition = new Vector2(transforms["CurDepthPos"].anchoredPosition.x, 250 + diver.Depth);
                transforms["DepthArrow"].anchoredPosition = new Vector2(transforms["DepthArrow"].anchoredPosition.x, 250 + diver.Depth);
                if (transforms["CurDepthPos"].anchoredPosition.y < -250 || transforms["DepthArrow"].anchoredPosition.y < -250)
                {
                    transforms["CurDepthPos"].anchoredPosition = new Vector2(transforms["CurDepthPos"].anchoredPosition.x, -250);
                    transforms["DepthArrow"].anchoredPosition = new Vector2(transforms["DepthArrow"].anchoredPosition.x, -250);
                }

                texts["DepthText"].text = $"{((int)diver.Depth * -1)}m";
                if (diver.Depth > -250)
                {
                    images["DepthSliderBG"].color = Color.Lerp(Color.cyan, Color.blue, diver.Depth * -1 * 0.004f);
                }
                else
                {
                    images["DepthSliderBG"].color = Color.Lerp(Color.blue, Color.black, (diver.Depth + 250) * -1 * 0.004f);
                }
                

                yield return new WaitForFixedUpdate();
            }
        }
        IEnumerator WeightChangeRoutine()
        {
            float rate = 0;
            float weight = diver.CurWeight / diver.MaxWeight;
            float cur = images["WeightSlider"].fillAmount;
            float curNum = float.Parse(texts["CurWeightNum"].text);
            float maxNum = float.Parse(texts["MaxWeightNum"].text);
            Color startColor = texts["CurWeightNum"].color;
            Color weightColor;
            Color changeColor;


            if (weight <= 0.5f)
                weightColor = new Color(weight * 2, 1, 0);
            else
                weightColor = new Color(1, 1 - (weight - 0.5f) * 2, 0);

            while (rate <= 1)
            {
                images["WeightSlider"].fillAmount = Mathf.Lerp(cur, weight, rate);

                changeColor = Color.Lerp(startColor, weightColor, rate);

                texts["CurWeightNum"].color = changeColor;
                texts["CurWeightNum"].text = ((int)Mathf.Lerp(curNum, diver.CurWeight, rate)).ToString();
                texts["MaxWeightNum"].color = changeColor;
                texts["MaxWeightNum"].text = ((int)Mathf.Lerp(curNum, diver.MaxWeight, rate)).ToString();

                texts["CurWeightText"].color = changeColor;
                texts["MaxWeightText"].color = changeColor;
                texts["WeightText"].color = changeColor;
                texts["MaxKg"].color = changeColor;
                texts["CurKg"].color = changeColor;

                rate += Time.deltaTime;
                yield return null;
            }
        }
        IEnumerator ResetRoutine()
        {
            float t = 0;
            float rate = 0;
            Color c = images["O2SliderBGEdge"].color;
            float fill = images["O2Slider"].fillAmount;
            float txt = float.Parse(texts["O2Text"].text);
            while (t < 2)
            {
                rate = t * 0.5f;
                if(c.g < 1 && t < 1)
                {
                    images["O2SliderBGEdge"].color = new Color(1, Mathf.Lerp(c.g, 1, t), 0);
                    images["O2SliderEdge"].color = new Color(1, Mathf.Lerp(c.g, 1, t), 0);
                }
                else
                {
                    images["O2SliderBGEdge"].color = new Color(Mathf.Lerp(c.r, 0, t - 1), 1, 0);
                    images["O2SliderEdge"].color = new Color(Mathf.Lerp(c.r, 0, t - 1), 1, 0);
                }

                images["O2Slider"].fillAmount = Mathf.Lerp(fill, 1, rate);

                texts["O2Text"].text = ((int)Mathf.Lerp(txt, diver.MaxO2, rate)).ToString();

                t += Time.deltaTime;

                yield return null;
            }
            texts["O2Text"].text = diver.MaxO2.ToString();

            OnEnable();
        }
    }
}
