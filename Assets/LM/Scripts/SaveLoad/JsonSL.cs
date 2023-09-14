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
        public List<List<string>> fishTank;
        public float curWeight;
        public int harpoonLevel;
        public List<List<string>> fishBox;

        public float masterVolume;
        public float SFXVolume;
        public float BGMVolume;
    }
    public class SaveFishData
    {
        // name, weight, length, rank 
    }

    public void Save(int slot)
    {
        SaveData save = new SaveData();
        // save.day =
        // save.isHome =
        // save.money =
        foreach (List<string> list in FindObjectOfType<KIM_FishTank>().ReturnFishTankFishList())
        {
            save.fishTank.Add(list);
        }
        save.curWeight = FindObjectOfType<Diver>().CurWeight;
        save.harpoonLevel = FindObjectOfType<HarpoonGun>().Level;
        foreach(List<string> list in FindObjectOfType<FishBox>().fishList)
        {
            save.fishBox.Add(list);
        }
        SettingUI ui = FindObjectOfType<SettingUI>();
        // save.masterVolume = ui.

        string json = JsonUtility.ToJson(save, true);
        string path = Path.Combine(Application.dataPath, $"save{slot}.json");
        File.WriteAllText(path, json);
    }
    public bool Load(int slot) 
    {
        // 화면 가리기
        // 다 지우기

        string path = Path.Combine(Application.dataPath, $"save{slot}.json");
        if (!File.Exists(path))
        {
            Debug.Log("No Save");
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
        FindObjectOfType<Diver>().CurWeight = save.curWeight;
        FindObjectOfType<HarpoonGun>().Level = save.harpoonLevel;
        // save.fishBox = 
        Debug.Log("Load");
        return true;
    }
}