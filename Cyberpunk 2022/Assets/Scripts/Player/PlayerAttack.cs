using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Gun auidio is played through event in animation
 * By default all animation layer except base layer weight layer = 0, and is set to weight=1 i.e enable on calling CurrentWeaponFireing()
 */
public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;
    private PlayerAttackFX playerAttackFX;

    [SerializeField] private int _currentWeaponIndex = 0;       // Adjusted using mouse scroll for now; Based on this animator layers are enabled or disabled
    [SerializeField] private int _maxWeaponTypes = 3;           // Player max weapon capacity
    [SerializeField] private GameObject[] _weaponList;          // List of weaponse GO
    [SerializeField] private string[] _weaponAnimList;          // Store list animation name for playing it based on _currentWeaponIndex
    [SerializeField] private string _selectedWeapon;            // Currently selected weapons animation name
    [SerializeField] private bool _automaticGun;                // True if _currentWeaponIndex > 1 (for now)   // When true hold down mouse for continous shots : else one click = one fire
    
    
    public ProjectileRayCast projectileRayCast = null;


    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _maxWeaponTypes = _weaponAnimList.Length - 1;               // -1 bcos 0 == idle in _weaponList, therefore only 1 less weapon
        playerAttackFX = FindObjectOfType<PlayerAttackFX>();
    }

    void Update()
    {
        WeaponSelector();               // _currentWeaponIndex is set

        if(Mathf.Abs(Input.GetAxisRaw("Mouse ScrollWheel")) > 0)
            CurrentWeaponFireing();         // Enables or disables animator layer based on the _currentWeaponIndex  // Also sets the _selecteWeapon - i.e animation name

    }

    private void FixedUpdate()
    {
        // Plays the fireing animation; if _automaticGun == false then single click else continous fire on holding down
        if (Input.GetButtonDown("Fire1") && !_automaticGun)
        {
            _animator.Play(_selectedWeapon);
            if(projectileRayCast != null )          // so that when in idel the projectileRayCast is null therefore no shoot option
                projectileRayCast.Shoot();
        }
 
        if (Input.GetButton("Fire1") && _automaticGun)
        {
            _animator.Play(_selectedWeapon);
            if (projectileRayCast != null)
                projectileRayCast.Shoot();
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

        // If not in Idle then, when the weapon enables (enabling done with animation) - then get the enabled weaponse projectileRayCast component
        if (_weaponList[_currentWeaponIndex] != null)
            projectileRayCast = _weaponList[_currentWeaponIndex].GetComponent<ProjectileRayCast>();
        else
            projectileRayCast = null;
    }

    // Enables animation layer and assigns the animation name in _selectedWeapon
    private void CurrentWeaponFireing() {
        switch (_currentWeaponIndex) {
            case 0:
                EnableAnimatorLayer(0);
                break;
            case 1:
                _selectedWeapon = _weaponAnimList[1];
                //_weaponList[1].SetActive(true);
                EnableAnimatorLayer(1);
                break;
            case 2:
                _selectedWeapon = _weaponAnimList[2];
                //_weaponList[2].SetActive(true);
                EnableAnimatorLayer(2);
                break;
        }
    }

    // Enable animation layer for merging idle | walk | run and shooting animation
    // Here if layerIndex == 0 then all animator layers from 1 to end, player weight will be set to 0
    private void EnableAnimatorLayer(int layerIndex) {

        for (int i = 1; i < _weaponAnimList.Length; i++) {
            if (i != layerIndex) {
                _animator.SetLayerWeight(i, 0);
            }
        }

        _animator.SetLayerWeight(layerIndex, 1);      
    }
}
