using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject currentCheckpoint;            // Is set from checkpoint.cs to the currently passed checkpoint


    [SerializeField] private float _respawnDelay;

    private Player _player;

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

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
    }

    void Update()
    {
        
    }

    public void RespawnPlayer(){
        StartCoroutine(RespawnPlayerCO());
    }

    private IEnumerator RespawnPlayerCO(){
        _player.enabled = false;

        yield return new WaitForSeconds(_respawnDelay);
        _player.transform.position = currentCheckpoint.transform.position;
        _player.enabled = true;
    }
}
