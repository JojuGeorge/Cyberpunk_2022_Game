using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inherited by scripts whose GO can be damaged
public interface IDamageable
{
    int Health { get; set;}
    void Damage(int damageAmount);
}
