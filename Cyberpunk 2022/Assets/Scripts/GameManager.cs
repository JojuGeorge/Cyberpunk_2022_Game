using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject currentCheckpoint;            // Is set from checkpoint.cs to the currently passed checkpoint
    public string playerDataFileName;
    public string enemyDataFileName;

    [SerializeField] private float _respawnDelay;

    private Player _player;
    private PlayerData _playerData;
    private EnemyData _enemyData;
    
    [HideInInspector] public bool autoDataLoad;                            // initilly false, set to true in Player and LifeManager is enabled

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        //LoadJsonData();
        autoDataLoad = false;
    }

    private void OnEnable()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }


    void Start()
    {
        _player = FindObjectOfType<Player>();
        _playerData = new PlayerData();
        _enemyData = new EnemyData();

        // if file not exist then create data file to save the game data
        SaveData.Instance.FileCreation<PlayerData>(_playerData, playerDataFileName);
        SaveData.Instance.FileCreation<EnemyData>(_enemyData, enemyDataFileName);
    }

    void Update()
    {
        //test
        // save data to json
        if (Input.GetKeyDown(KeyCode.K)) {
            PopulateSaveData();
            SaveJsonData();
        }

        //test
        // When game starts populate from json data to game ; initial automatic load using bool autoDataLoad
        // if L is clicked or the player, LifeManager and enemy is enabled, then populate it with data by loading json
        if (Input.GetKeyDown(KeyCode.L) || !autoDataLoad) {             // here !autoDataLoad bcos only need to automatically check once when all GO is enabled
            if (_player.enabled && LifeManager.Instance.enabled){       // add checks to enemy also
                LoadJsonData();
                autoDataLoad = true;
                Debug.Log("updated = " + autoDataLoad);
            }
        }
    }

    public void RespawnPlayer(){
        StartCoroutine(RespawnPlayerCO());
    }

    private IEnumerator RespawnPlayerCO(){
        _player.enabled = false;
        _player._rb.velocity = Vector2.zero;            // prevents player from sliding after death
        

        yield return new WaitForSeconds(_respawnDelay);

        _player.transform.position = currentCheckpoint.transform.position;
        _player.enabled = true;
        _player.Health = _playerData.maxHealth;            // reset player health on death and respawn
    }


    public void PopulateSaveData() {
        PopulateToPlayerSaveData();
    }

    // Save data to Json
    public void SaveJsonData() {
        SaveData.Instance.Save<PlayerData>(_playerData, playerDataFileName);
        SaveData.Instance.Save<EnemyData>(_enemyData, enemyDataFileName);               //test enemy part not complete
    }

    // Load data from Json
    public void LoadJsonData() {
        _playerData = SaveData.Instance.Load<PlayerData>(playerDataFileName);
        _enemyData = SaveData.Instance.Load<EnemyData>(enemyDataFileName);
        LoadFromPlayerSaveData();
    }


    // Populate data with values for saving to Json, called before saving to Json
    public void PopulateToPlayerSaveData() {
        _playerData.health = _player.Health;
        _playerData.life = LifeManager.Instance.life;
    }

    // populate data from loaded Json to the game properties
    public void LoadFromPlayerSaveData() {
        _player.Health = _playerData.health;
        LifeManager.Instance.life = _playerData.life;
        LifeManager.Instance.maxLife = _playerData.maxLife;
    }
}
