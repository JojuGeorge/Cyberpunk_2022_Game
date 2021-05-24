using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInitialization : MonoBehaviour
{
    public string playerDataFileName;
    public string enemyDataFileName;

    private PlayerData _playerData;
    private EnemyData _enemyData;


    private static DataInitialization _instance;
    public static DataInitialization Instance { get { return _instance; } }


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _playerData = new PlayerData();
        _enemyData = new EnemyData();
        LoadJsonData();
    }

    private void OnEnable()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    // Load data from Json
    public void LoadJsonData()
    {
        _playerData = SaveData.Instance.Load<PlayerData>(playerDataFileName);
        _enemyData = SaveData.Instance.Load<EnemyData>(enemyDataFileName);
    }

    // Reset All Data
    public void ResetAllData()
    {
        PlayerDataReset();
        EnemyDataReset();
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
}
