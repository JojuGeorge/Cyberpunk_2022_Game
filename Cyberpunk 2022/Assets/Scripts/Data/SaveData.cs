using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveData : MonoBehaviour
{
    PlayerData pd = new PlayerData();
    public string dataW;
    public string dataR;

    private void Start()
    {
        pd.fullHealth = 4;
        pd.fullLife = 2;
      
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            Save();
        if (Input.GetKeyDown(KeyCode.L))
            Load();
    }
    public void Save()
    {

        dataW = JsonConvert.SerializeObject(pd);
        string destination = Application.persistentDataPath + "/save.dat";
        // FileStream file = File.OpenWrite(destination);
        StreamWriter writer = new StreamWriter(destination);
        writer.Write(dataW);
        writer.Close();
        Debug.Log("DATA SAVED");

    }

    public void Load()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        StreamReader reader = new StreamReader(destination);
        dataR = reader.ReadToEnd();
        reader.Close();
        pd = JsonConvert.DeserializeObject<PlayerData>(dataR);
        Debug.Log("DATA LOADED");

    }
}

