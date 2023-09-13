using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LM
{
    public class BaseUI : MonoBehaviour
    {
        protected Dictionary<string, RectTransform> transforms;
        protected Dictionary<string, TMP_Text> texts;
        protected Dictionary<string, Image> images;
        protected Dictionary<string, Button> buttons;
        protected Dictionary<string, Toggle> toggles;

        protected virtual void Awake()
        {
            BindChildren();
        }

        protected virtual void BindChildren()
        {
            transforms = new Dictionary<string, RectTransform>();
            texts = new Dictionary<string, TMP_Text>();
            images = new Dictionary<string, Image>();
            buttons = new Dictionary<string, Button>();
            toggles = new Dictionary<string, Toggle>();

            RectTransform[] children = GetComponentsInChildren<RectTransform>();
            foreach (RectTransform child in children)
            {
                string key = child.gameObject.name;

                if (transforms.ContainsKey(key))
                    continue;

                transforms.Add(key, child);

                TMP_Text text = child.GetComponent<TMP_Text>();
                if (text != null)
                    texts.Add(key, text);

                Image image = child.GetComponent<Image>();
                if(image != null)
                    images.Add(key, image);

                Button button = child.GetComponent<Button>();
                if (button != null)
                    buttons.Add(key, button);

                Toggle toggle = child.GetComponent<Toggle>();
                if(toggle != null)
                    toggles.Add(key, toggle);
            }
        }
    }
}
