using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    private static LifeManager _instance;
    public static LifeManager Instance { get { return _instance; } }


    [SerializeField] public int life;               // life and maxLife is initialied from GameManager.LoadFromPlayerSaveData();
    [SerializeField] public int maxLife;                

    private Player _player;

    private void OnEnable()
    {
        if (_instance == null) {
            _instance = this;
        }
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    // Add life on getting Life bonus
    public void GiveLife() {
        if (life < maxLife) {
            life++;
            GameManager.Instance.PopulatePlayerSaveData();
        }
    }

    public void TakeLife() {
        life--;

        if (life < 1) {
            life = 0;
            Debug.Log("GAME OVER!!!!!!!!!!!!!");
        }
        GameManager.Instance.PopulatePlayerSaveData();
    }


    // update life in HUD on adding and removing
}
