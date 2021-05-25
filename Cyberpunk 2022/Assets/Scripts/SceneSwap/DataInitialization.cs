using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DataInitialization : MonoBehaviour
{
    public string playerDataFileName;
    public string enemyDataFileName;

    private SaveData _saveData;
    private PlayerData _playerData;
    private EnemyData _enemyData;
    private bool _dataLoaded;


    private static DataInitialization _instance;
    public static DataInitialization Instance { get { return _instance; } }


    private void Awake()
    {

        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Start()
    {
        _dataLoaded = false;
        _saveData = new SaveData();
        _playerData = new PlayerData();
        _enemyData = new EnemyData();
        LoadJsonData();
    }

    // Load data from Json  // Check before loadin if the file is created
    public void LoadJsonData()
    {
        _playerData = _saveData.Load<PlayerData>(playerDataFileName);
        _enemyData = _saveData.Load<EnemyData>(enemyDataFileName);
        _dataLoaded = true;
    }

    // Reset All Data
    public void ResetAllData()
    {
        if (_dataLoaded)
        {
            PlayerDataReset();
            EnemyDataReset();
            SaveJsonData();
        }
    }

    // Reset All Player Data
    public void PlayerDataReset()
    {
        _playerData.health = _playerData.maxHealth;
        _playerData.life = _playerData.maxLife;
    }

    // Reset All Enemy Data
    public void EnemyDataReset()
    {
        _enemyData.health = _enemyData.maxHealth;
    }

    public void SaveJsonData()
    {
        _saveData.Save<PlayerData>(_playerData, playerDataFileName);
        _saveData.Save<EnemyData>(_enemyData, enemyDataFileName);
        Debug.Log("SAVED");
    }


}
