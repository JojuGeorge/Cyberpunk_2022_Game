using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;

public class SaveData : MonoBehaviour
{
    //PlayerData pd = new PlayerData();
    //public int fullHealth = 5;
    //public int fullLife = 4;

    //public string dataW;
    //public string dataR;

    //private void Start()
    //{
    //    pd.fullHealth = this.fullHealth;
    //    pd.fullLife = this.fullLife;

    //}

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //        Save();
    //    if (Input.GetKeyDown(KeyCode.L))
    //        Load();
    //}

    private static SaveData _instance;
    public static SaveData Instance { get { return _instance; } }

    private void OnEnable()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void Save<T>( T dataObject, string fileName) where T: class
    {
        string destination = Application.persistentDataPath + "/" + fileName + ".dat";
        string data = JsonConvert.SerializeObject(dataObject);
        File.WriteAllText(destination, data);
        Debug.Log("DATA SAVED" + data);    
    }

    public T Load<T>(string fileName) where T: class
    {
        try {
            string destination = Application.persistentDataPath + "/" + fileName + ".dat";
            string data="null";

            if (File.Exists(destination)) {
                data = File.ReadAllText(destination);
                Debug.Log("DATA LOADED" + data);
            }
            else {
                Debug.Log(destination + " FILE NOT FOUND!!");
            }

            // while converting it to obj if edited it gives error
            // JsonReaderException: Additional text encountered after finished reading JSON content
            return JsonConvert.DeserializeObject<T>(data);
        }
        catch (Exception e) {
            Debug.LogException(e);
        }
        return null;
    }
}

