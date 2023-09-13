using KIM;
using LM;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSL : MonoBehaviour
{
    [SerializeField] Vector3 homePos;
    [SerializeField] Vector3 seaPos;

    [Serializable]
    public struct SaveList
    {
        public List<SaveData> saves;
    }

    [Serializable]
    public class SaveData
    {
        public int day;
        public bool isHome;
        public int money;
        public List<SaveFishData> fishTank;
        public float repute;
        public float curWeight;
        public int harpoonLevel;
        public List<SaveFishData> fishBox;

        public float masterVolume;
        public float SFXVolume;
        public float BGMVolume;
    }
    public class SaveFishData
    {

    }

    public void Save(int slot)
    {
        SaveData save = new SaveData();
        // save.day =
        // save.isHome =
        // save.money =
        // save.fishTank
        // save.repute = 
        save.curWeight = FindObjectOfType<Diver>().CurWeight;
        save.harpoonLevel = FindObjectOfType<HarpoonGun>().Level;
        // save.fishBox = 

        SettingUI ui = FindObjectOfType<SettingUI>();
        // save.masterVolume = ui.

        string json = JsonUtility.ToJson(save, true);
        string path = Path.Combine(Application.dataPath, $"save{slot}.json");
        File.WriteAllText(path, json);
    }
    public bool Load(int slot) 
    {
        // 화면 가리기
        string path = Path.Combine(Application.dataPath, $"save{slot}.json");
        if (!Directory.Exists(path))
        {
            return false;
        }

        string loadJson = File.ReadAllText(path);
        SaveData save = JsonUtility.FromJson<SaveData>(loadJson);
        // save.day =
        if(save.isHome)
        {
            FindObjectOfType<CharacterController>().Move(homePos);
            // 배도 집으로
        }
        else
        {
            FindObjectOfType<CharacterController>().Move(seaPos);
            // 배도 바다로
        }
        // save.money =
        // save.fishTank
        // save.repute = 
        FindObjectOfType<Diver>().CurWeight = save.curWeight;
        FindObjectOfType<HarpoonGun>().Level = save.harpoonLevel;
        // save.fishBox = 

        return true;
    }
}