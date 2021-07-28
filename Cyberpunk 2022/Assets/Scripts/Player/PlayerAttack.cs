using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerAnimation _playerAnimation;                   // For player animations
    private Animator _animator;

    void Start()
    {
        _playerAnimation = FindObjectOfType<PlayerAnimation>();
        _animator = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!_animator.GetBool("AimGun"))
            {
                _animator.SetBool("PistolFire", true);
                _animator.SetBool("AimGun", true);
            }
            else {
                _animator.SetBool("PistolFire", false);
                _animator.Play("Shooting_Pistol_02");
            }
        }


        if (_animator.GetFloat("WalkSpeed") >= 1)
        {
            _animator.SetBool("AimGun", false);
            _animator.SetBool("PistolFire", false);

        }

    }
}
