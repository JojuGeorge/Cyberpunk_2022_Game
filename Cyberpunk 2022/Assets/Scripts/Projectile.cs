using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed;

    private Rigidbody2D _rb;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.right * _projectileSpeed;

    }


    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.tag);
        Destroy(gameObject);
    }

}
