using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D _rb;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private float _moveX;       // Gets the movement vector; For flipping the player faceing direction
    private Vector2 direction;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // Player movement code
        Movement();
    }


    // Player movement
    private void Movement() {
        _moveX = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        _rb.velocity = new Vector2(_moveX, _rb.velocity.y);
    }

    private void Jump() {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }
}
