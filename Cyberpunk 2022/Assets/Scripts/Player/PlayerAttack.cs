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

        if (Time.time - clickedTime > pistolShotDelay)
        {
            clicked = 0;
            _animator.SetBool("MultipleShots", false);
            _animator.SetBool("SingleShot", false);
        }

    }

    public void SingleShot() {
        Debug.Log("Single shot" + (Time.time - clickedTime));
        _animator.SetTrigger("ShootingPistol");
        _animator.SetBool("SingleShot", true);
    }

    public void MultipleShot() {
        Debug.Log("Multiple shot");
        //while (clicked > 0)
        //{
        //    _playerAnimation.MultiplePistolShot();
        //    clicked--;
        //}
       //  _animator.SetBool("MultipleShots", true);

    }
}
