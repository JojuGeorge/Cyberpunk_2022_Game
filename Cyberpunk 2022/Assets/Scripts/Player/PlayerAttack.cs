using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int clicked;
    public float clickedTime;
    public float pistolShotDelay;

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
            clicked++;
            if (clicked == 1)
            {
                clickedTime = Time.time;
                SingleShot();
                _animator.SetBool("AimGun", true);

            }
        }


        else if (clicked > 1 && Time.time - clickedTime < pistolShotDelay)
        {
            MultipleShot();
        }

        //reset
        if (Time.time - clickedTime > pistolShotDelay)
        {
            clicked = 0;
            _animator.SetBool("MultipleShots", false);
        }

        if (_animator.GetFloat("WalkSpeed") >= 1)
            _animator.SetBool("AimGun", false);

    }

    public void SingleShot() {
        Debug.Log("Single shot" + (Time.time - clickedTime));

        if(!_animator.GetBool("AimGun"))
            _animator.SetTrigger("ShootingPistol");
        else
            _animator.Play("Shooting_Pistol_02");
    }

    public void MultipleShot() {
        Debug.Log("Multiple shot");
        //  _animator.SetBool("MultipleShots", true);
        while (clicked > 0 ) {
            _animator.Play("Shooting_Pistol_02");
            clicked--;
        }

    }
}
