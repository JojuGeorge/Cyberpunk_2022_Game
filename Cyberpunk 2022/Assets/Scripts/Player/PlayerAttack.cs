using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private int _currentWeaponIndex = 1;
    [SerializeField] private int _maxWeaponTypes = 3;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        WeaponSelector();   
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.Play("Shooting_Pistol_02");           // plays the fireing animation
        }
    }

    // Weapon selection using mouse scroll
    private void WeaponSelector()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0) {
            if (_currentWeaponIndex < _maxWeaponTypes)
            {
                _currentWeaponIndex++;
            }
            else if (_currentWeaponIndex >= _maxWeaponTypes) {
                _currentWeaponIndex = 1;
            }
        }else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0) {
            if (_currentWeaponIndex > 1)
            {
                _currentWeaponIndex--;
            }
            else if (_currentWeaponIndex <= 1)
            {
                _currentWeaponIndex = _maxWeaponTypes;
            }
        }

    }
}
