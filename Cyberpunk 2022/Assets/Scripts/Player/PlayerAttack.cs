using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerAnimation _playerAnimation;                   // For player animations

    void Start()
    {
        _playerAnimation = FindObjectOfType<PlayerAnimation>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot() {
        _playerAnimation.ShootingPistol();
    }
}
