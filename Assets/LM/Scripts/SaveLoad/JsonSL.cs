using KIM;
using LM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class JsonSL : MonoBehaviour
{
    [SerializeField] Transform homePos;
    [SerializeField] Transform seaPos;

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
    // name, weight, length, rank 

    public void Save(int slot)
    {
        SaveData save = new SaveData();
        // save.day =
        save.isHome = FindObjectOfType<BoatMover>().isHome;
        // save.money =
        foreach (List<string> list in FindObjectOfType<KIM_FishTank>().fishList)
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
        save.masterVolume = ui.MasterVolume;
        save.SFXVolume = ui.SFXVolume;
        save.BGMVolume = ui.BGMVolume;

        string json = JsonUtility.ToJson(save, true);
        string path = Path.Combine(Application.dataPath, $"save{slot}.json");
        File.WriteAllText(path, json);
    }
    public bool Load(int slot) 
    {
        Mask mask = FindObjectOfType<Mask>();
        mask.gameObject.SetActive(true);
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
            FindObjectOfType<CharacterController>().Move(homePos.position);
            BoatMover boat = FindObjectOfType<BoatMover>();
            boat.gameObject.transform.position = boat.portPos.position;
            boat.isHome = save.isHome;
            // 배도 집으로
        }
        else
        {
            FindObjectOfType<CharacterController>().Move(seaPos.position);
            BoatMover boat = FindObjectOfType<BoatMover>();
            boat.gameObject.transform.position = boat.seaPos.position;
            boat.isHome = save.isHome;
            // 배도 바다로
        }
        // save.money =
        // save.fishTank
        FindObjectOfType<Diver>().CurWeight = save.curWeight;
        FindObjectOfType<HarpoonGun>().Level = save.harpoonLevel;
        FishBox box = FindObjectOfType<FishBox>();
        foreach (List<string> list in save.fishBox)
        {
            box.AddFish(list);
        }
        SettingUI ui = FindObjectOfType<SettingUI>();
        ui.MasterVolume = save.masterVolume;
        ui.SFXVolume = save.SFXVolume;
        ui.BGMVolume = save.BGMVolume;
        Debug.Log("Load");
        mask.gameObject.SetActive(false);
        return true;
    }
    public async void LoadData(int slot)
    {
        Mask mask = FindObjectOfType<Mask>();
        mask.gameObject.SetActive(true);
        Task<bool> task = Task.Run(() => Load(slot));
        // 화면 가리기
        // 다 지우기
        bool result = await task;
        mask.gameObject.SetActive(false);
    }
}