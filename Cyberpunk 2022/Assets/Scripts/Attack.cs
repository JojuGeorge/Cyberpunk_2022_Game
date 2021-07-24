using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script to deal damage to the GO that collides with the GO with this script
public class Attack : MonoBehaviour
{
    [SerializeField]
    private int _damageAmount;                  // Damage to be done to the other GO
    private bool _canDamage = true;             // So that the damage wont be continous and used coroutine to give a counter to deal damage again after delay
    

    private void OnTriggerEnter2D(Collider2D other){

        IDamageable hit = other.GetComponent<IDamageable>();

        if(hit != null && _canDamage){
            hit.Damage(_damageAmount);
            _canDamage = false;
            StartCoroutine(ResetDamageCounter());
        }
    }

    IEnumerator ResetDamageCounter(){
        yield return new WaitForSeconds(0.5f);
        _canDamage = true;
    }
}
