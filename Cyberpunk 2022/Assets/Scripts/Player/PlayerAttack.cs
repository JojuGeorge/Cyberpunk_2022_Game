using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private int _currentWeaponIndex = 0;       // Adjusted using mouse scroll for now; Based on this animator layers are enabled or disabled
    [SerializeField] private int _maxWeaponTypes = 3;           // Player max weapon capacity
    [SerializeField] private string[] _weaponList;              // Store list animation name for playing it based on _currentWeaponIndex
    [SerializeField] private string _selectedWeapon;            // Currently selected weapons animation name
    [SerializeField] private bool _automaticGun;                // True if _currentWeaponIndex > 1 (for now)
                                                                    // When true hold down mouse for continous shots : else one click = one fire

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _maxWeaponTypes = _weaponList.Length - 1;               // -1 bcos 0 == idle in _weaponList, therefore only 1 less weapon
    }

    void Update()
    {
        WeaponSelector();               // _currentWeaponIndex is set
        CurrentWeaponFireing();         // Enables or disables animator layer based on the _currentWeaponIndex
                                            // Also sets the _selecteWeapon - i.e animation name
    }

    private void FixedUpdate()
    {
        // Plays the fireing animation; if _uatomaticGun == false then single click else continous fire on holding down
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
                _currentWeaponIndex = 0;
            }
        }else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0) {
            if (_currentWeaponIndex > 0)
            {
                _currentWeaponIndex--;
            }
            else if (_currentWeaponIndex <= 0)
            {
                _currentWeaponIndex = _maxWeaponTypes;
            }
        }

        // For single fire and click + hold down for automatic fire
        if (_currentWeaponIndex > 1)
            _automaticGun = true;
        else
            _automaticGun = false;

    }

    // Enables animation layer and assigns the animation name in _selectedWeapon
    private void CurrentWeaponFireing() {
        switch (_currentWeaponIndex) {
            case 0:
                EnableAnimatorLayer(0);
                break;
            case 1:
                _selectedWeapon = _weaponList[1];
                EnableAnimatorLayer(1);
                break;
            case 2:
                _selectedWeapon = _weaponList[2];
                EnableAnimatorLayer(2);
                break;
        }
    }

    // Enable animation layer for merging idle | walk | run and shooting animation
    // Here if layerIndex == 0 then all animator layers from 1 to end, player weight will be set to 0
    private void EnableAnimatorLayer(int layerIndex) {

        for (int i = 1; i <= _weaponList.Length; i++) {
            if (i != layerIndex) {
                _animator.SetLayerWeight(i, 0);
            }
        }

        _animator.SetLayerWeight(layerIndex, 1);      
    }
}
