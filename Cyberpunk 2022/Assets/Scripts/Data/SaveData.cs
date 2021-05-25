using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;

public class SaveData //: MonoBehaviour
{
    //private static SaveData _instance;
    //public static SaveData Instance { get { return _instance; } }

    private string destination;
    //private void OnEnable()
    //{
    //    if (_instance == null)
    //    {
    //        _instance = this;
    //    }
    //}

    public void Save<T>( T dataObject, string fileName) where T: class
    {
        destination = Application.persistentDataPath + "/" + fileName + ".dat";
        string data = JsonConvert.SerializeObject(dataObject);
        File.WriteAllText(destination, data);
        Debug.Log("DATA SAVED" + data);    
    }

    public T Load<T>(string fileName) where T: class
    {
        try {
            destination = Application.persistentDataPath + "/" + fileName + ".dat";
            string data ="null";

            if (File.Exists(destination)) {
                data = File.ReadAllText(destination);
                Debug.Log("DATA LOADED" + data);
                // while converting it to obj if edited it gives error
                // JsonReaderException: Additional text encountered after finished reading JSON content
                return JsonConvert.DeserializeObject<T>(data);
            }
            else {
                Debug.Log(destination + " FILE NOT FOUND!!");
            }
            //return null;
        }
        catch (Exception e) {
            Debug.LogException(e);
        }
        return null;
    }

    // If file exists do nothing else create file with empty dataObject attributes
    public void FileCreation<T>(T dataObject, string fileName) {
        destination = Application.persistentDataPath + "/" + fileName + ".dat";

        if (File.Exists(destination)) {
            return;
        }
        else {
            string data = JsonConvert.SerializeObject(dataObject);
            File.WriteAllText(destination, data);
        }

    }
}

