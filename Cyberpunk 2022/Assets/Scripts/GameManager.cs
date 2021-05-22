using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject currentCheckpoint;            // Is set from checkpoint.cs to the currently passed checkpoint


    [SerializeField] private float _respawnDelay;

    private Player _player;
    private PlayerData _playerData;
    public bool updated;                            // initilly false, set to true in Player and LifeManager is enabled

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        //LoadJsonData();
        updated = false;
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
        LoadJsonData();                               // Populate with json file, call last in Start() for proper population
    }

    void Update()
    {
        //test
        // save data to json
        if (Input.GetKeyDown(KeyCode.K))
        {
            PopulateSaveData();
            SaveJsonData();
        }

        //test
        // load data from json
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadJsonData();
        }

        // To check if Player and LifeManager is enabled then populate it with data
        if (!updated) {
            if (_player.enabled && LifeManager.Instance.enabled) {
                LoadFromPlayerSaveData();
                updated = !updated;
                Debug.Log("updated = " + updated);
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
        PopulatePlayerSaveData();
    }

    // Save data to Json
    public void SaveJsonData() {
        SaveData.Instance.Save<PlayerData>(_playerData, "playerDataFile");
    }

    // Load data from Json
    public void LoadJsonData() {
        _playerData = SaveData.Instance.Load<PlayerData>("playerDataFile");
        if (updated)
            LoadFromPlayerSaveData();
    }


    // Populate data with values for saving to Json, called before saving to Json
    public void PopulatePlayerSaveData() {
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
