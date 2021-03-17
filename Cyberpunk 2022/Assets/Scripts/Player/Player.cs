using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D _rb;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private float _moveX;       // Gets the movement vector; For flipping the player faceing direction

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Player movement code
        Movement();
    }


    // Player movement
    private void Movement() {

        _moveX = Input.GetAxisRaw("Horizontal") * _moveSpeed * Time.fixedDeltaTime;

        //_rb.MovePosition(new Vector2(_rb.position.x + _moveX, _rb.position.y));
    }
}
