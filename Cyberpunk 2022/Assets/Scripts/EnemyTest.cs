using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour, IDamageable
{
    public int Health { get; set; }
    public int enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        Health = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damageAmount)
    {
        if (Health < 1)
            return;
    
        Health -= damageAmount;

        if (Health < 1) {
            Debug.Log("enemy dead!!");
            Destroy(this.gameObject);
        }
    }
}
