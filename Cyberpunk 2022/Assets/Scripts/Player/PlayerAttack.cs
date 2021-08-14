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
            // if player is not already aiming the gun, then idle -> gun pose + shooting -> aim pose and stops at aim pose
            // bcos if already aiming then no need to do idle -> gun pose :)
            // "PistolFire" == true does idle -> gun pose + shooting -> aim pose
            //if (!_animator.GetBool("AimGun"))
            //{
            //    _animator.SetBool("PistolFire", true);
            //    _animator.SetBool("AimGun", true);
            //}
            // if aready in gun pose, then just shoot, no need to do idle -> gunpose + shooring
            //else {
            //    _animator.SetBool("PistolFire", false);
            //    _animator.Play("Shooting_Pistol_02");           // plays the fireing animation
            //}
            _animator.Play("Shooting_Pistol_02");           // plays the fireing animation

        }


        // While in aimpose if player starts to walk then stop gun aim pose
        if (_animator.GetFloat("WalkSpeed") >= 1)
        {
            _animator.SetBool("AimGun", false);
            _animator.SetBool("PistolFire", false);

        }

    }
}
