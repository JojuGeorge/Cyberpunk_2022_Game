using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    public string multiplePistolShotAnim;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();   
    }

    public void Walk(float walkSpeed) {
        _animator.SetFloat("WalkSpeed", Mathf.Abs(walkSpeed));
    }

    public void Run(bool run) {
        _animator.SetBool("Run", run);
    }

    public void AimGun(bool _value) {
        _animator.SetBool("AimGun", _value);
    }
}
