using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static JsonSL;

namespace LM
{
    public class SaveSlots : BaseUI
    {
        JsonSL json;
        protected override void Awake()
        {
            base.Awake();
            json = GetComponentInParent<JsonSL>();
        }
        private void OnEnable()
        {
            buttons["SaveSlot1"].onClick.AddListener(SaveSlot1);
            buttons["SaveSlot2"].onClick.AddListener(SaveSlot2);
            buttons["SaveSlot3"].onClick.AddListener(SaveSlot3);
            buttons["SaveSlot4"].onClick.AddListener(SaveSlot4);
            buttons["SaveSlot5"].onClick.AddListener(SaveSlot5);

            for(int i = 1; i < 6; i++)
            {
                string path = Path.Combine(Application.dataPath, $"save{i}.json");
                if (File.Exists(path))
                {
                    string loadJson = File.ReadAllText(path);
                    SaveData save = JsonUtility.FromJson<SaveData>(loadJson);
                    texts[$"SaveSlot{i}Explain"].text = $"Day {save.day}\n{save.saveTime}";
                    images[$"SaveSlot{i}"].color = Color.green;
                }
                else
                {
                    images[$"SaveSlot{i}"].color = Color.white;
                    texts[$"SaveSlot{i}Explain"].text = "";
                }
            }
        }
        private void OnDisable()
        {
            buttons["SaveSlot1"].onClick.RemoveListener(SaveSlot1);
            buttons["SaveSlot2"].onClick.RemoveListener(SaveSlot2);
            buttons["SaveSlot3"].onClick.RemoveListener(SaveSlot3);
            buttons["SaveSlot4"].onClick.RemoveListener(SaveSlot4);
            buttons["SaveSlot5"].onClick.RemoveListener(SaveSlot5);
        }
        private void SaveSlot1()
        {
            json.Save(1);
            images["SaveSlot1"].color = Color.green;
            gameObject.SetActive(false);
        }
        private void SaveSlot2()
        {
            json.Save(2);
            images["SaveSlot2"].color = Color.green;
            gameObject.SetActive(false);
        }
        private void SaveSlot3()
        {
            json.Save(3);
            images["SaveSlot3"].color = Color.green;
            gameObject.SetActive(false);
        }
        private void SaveSlot4()
        {
            json.Save(4);
            images["SaveSlot4"].color = Color.green;
            gameObject.SetActive(false);
        }
        private void SaveSlot5()
        {
            json.Save(5);
            images["SaveSlot5"].color = Color.green;
            gameObject.SetActive(false);
        }
    }
}