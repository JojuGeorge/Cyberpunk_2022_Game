using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Raycast mechanics and deal damage to GO with IDamageable interface ie BULLET
public class ProjectileRayCast : MonoBehaviour
{
    [SerializeField] private Transform _fireingPoint;
    [SerializeField] private float _weaponRange;
    [SerializeField] private int _damageAmount;     // Damage to be done 
    [SerializeField] private float _resetDamageCounterDelay;

    private bool _canDamage = true;                 // So that the damage wont be continous and used coroutine to give a counter to deal damage again after delay
    private PlayerAttack _playerAttack;


    private void ondisable()
    {
        // When Idle the projectileRayCast is set to null i.e when disabled
        _playerAttack.projectileRayCast = null;     
    }

    void Start() {
        _playerAttack = FindObjectOfType<PlayerAttack>();

    }

    private void Update()
    {

    }

    public void Shoot() {
        RaycastHit2D hitInfo = Physics2D.Raycast(_fireingPoint.position, Vector2.right, _weaponRange);
        Debug.DrawRay(_fireingPoint.position, Vector2.right * _weaponRange, Color.red);

        // If raycast is hit on something ie within the range
        if (hitInfo)
        {
            IDamageable hit = hitInfo.transform.GetComponent<IDamageable>();


            // check if the raycast hit object has IDamageable Interface
            if (hit != null && _canDamage)
            {
                hit.Damage(_damageAmount);
                Debug.Log("Shots fired; damage amount = " + _damageAmount);
                _canDamage = false;
                StartCoroutine(ResetDamageCounter());
            }
        }
    }

    IEnumerator ResetDamageCounter()
    {
        yield return new WaitForSeconds(_resetDamageCounterDelay);
        _canDamage = true;
    }

}
