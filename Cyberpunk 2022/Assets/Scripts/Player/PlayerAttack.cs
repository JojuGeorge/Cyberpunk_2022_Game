using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private int _currentWeaponIndex = 1;
    [SerializeField] private int _maxWeaponTypes = 3;
    [SerializeField] private string[] _weaponList;
    [SerializeField] private string _selectedWeapon;
    [SerializeField] private bool _automaticGun;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        WeaponSelector();
        CurrentWeaponFireing();
    }

    private void FixedUpdate()
    {
        // plays the fireing animation "Shooting_Pistol_02", "Shooting_Large_Gun"
        if (Input.GetButtonDown("Fire1") && !_automaticGun)
        {
            _animator.Play(_selectedWeapon);
        }
 
        if (Input.GetButton("Fire1") && _automaticGun)
        {
            _animator.Play(_selectedWeapon);
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

        if (_currentWeaponIndex > 2)
            _automaticGun = true;
        else
            _automaticGun = false;

    }

    private void CurrentWeaponFireing() {
        switch (_currentWeaponIndex) {
            case 1:
                _animator.SetLayerWeight(1, 0);
                _animator.SetLayerWeight(2, 0);

                break;
            case 2:
                _selectedWeapon = _weaponList[1];
                _animator.SetLayerWeight(1, 1);
                _animator.SetLayerWeight(2, 0);
                Debug.Log(_selectedWeapon);
                break;
            case 3:
                _selectedWeapon = _weaponList[2];
                _animator.SetLayerWeight(2, 1);
                _animator.SetLayerWeight(1, 0);
                Debug.Log(_selectedWeapon);

                break;

        }
    }
}
