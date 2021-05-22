using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    private static LifeManager _instance;
    public static LifeManager Instance { get { return _instance; } }

    public int lifeCounter;             // later set it up with PlayerStatsSO

    private int _maxLife;                // later set it up with PlayerStatsSO
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

    public void GiveLife() {
        if (lifeCounter < _maxLife) {
            lifeCounter++;
        }
    }

    public void TakeLife() {
        lifeCounter--;

        if (lifeCounter < 1) {
            lifeCounter = 0;
            Debug.Log("GAME OVER!!!!!!!!!!!!!");
        }
    }


    // update life in HUD on adding and removing
}
