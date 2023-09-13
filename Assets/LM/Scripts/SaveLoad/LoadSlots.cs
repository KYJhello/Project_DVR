using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LM
{
    public class LoadSlots : BaseUI
    {
        JsonSL json;
        protected override void Awake()
        {
            base.Awake();
            json = GetComponentInParent<JsonSL>();
        }
        private void OnEnable()
        {
            buttons["LoadSlot1"].onClick.AddListener(LoadSlot1);
            buttons["LoadSlot2"].onClick.AddListener(LoadSlot2);
            buttons["LoadSlot3"].onClick.AddListener(LoadSlot3);
            buttons["LoadSlot4"].onClick.AddListener(LoadSlot4);
            buttons["LoadSlot5"].onClick.AddListener(LoadSlot5);

            for (int i = 1; i < 6; i++)
            {
                string path = Path.Combine(Application.dataPath, $"save{i}.json");
                if (Directory.Exists(path))
                {
                    Debug.Log($"{i}save load");
                    images[$"LoadSlot{i}"].color = Color.green;
                }
                else
                {
                    Debug.Log("¾ø¾î");
                    images[$"LoadSlot{i}"].color = Color.white;
                }
            }
        }
        private void OnDisable()
        {
            buttons["LoadSlot1"].onClick.RemoveListener(LoadSlot1);
            buttons["LoadSlot2"].onClick.RemoveListener(LoadSlot2);
            buttons["LoadSlot3"].onClick.RemoveListener(LoadSlot3);
            buttons["LoadSlot4"].onClick.RemoveListener(LoadSlot4);
            buttons["LoadSlot5"].onClick.RemoveListener(LoadSlot5);
        }
        private void LoadSlot1()
        {
            json.Load(1);
            images["LoadSlot1"].color = Color.green;
            gameObject.SetActive(false);
        }
        private void LoadSlot2()
        {
            json.Load(2);
            images["LoadSlot2"].color = Color.green;
            gameObject.SetActive(false);
        }
        private void LoadSlot3()
        {
            json.Load(3);
            images["LoadSlot3"].color = Color.green;
            gameObject.SetActive(false);
        }
        private void LoadSlot4()
        {
            json.Load(4);
            images["LoadSlot4"].color = Color.green;
            gameObject.SetActive(false);
        }
        private void LoadSlot5()
        {
            json.Load(5);
            images["LoadSlot5"].color = Color.green;
            gameObject.SetActive(false);
        }
    }
}