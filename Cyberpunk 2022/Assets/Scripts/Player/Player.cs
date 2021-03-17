using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D _rb;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _groundCheckDistance;

    private float _moveX;       // Gets the movement vector; For flipping the player faceing direction
    private bool _isGrounded;
    private bool _doubleJump = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _isGrounded = CheckIfGrounded();
    }

    private void FixedUpdate()
    {
        // Player movement code
        Movement();

        // Player Jump
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            Jump();
           // _doubleJump = true;
        }
        else if (Input.GetButtonDown("Jump") && !_isGrounded && _doubleJump) {
            _doubleJump = false;
            Jump();
        }
    }


    // Player movement
    private void Movement() {
        _moveX = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        _rb.velocity = new Vector2(_moveX, _rb.velocity.y);
    }

    private void Jump() {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }

    private bool CheckIfGrounded() {
        // Create a raycast from player position to under its feet
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _groundLayerMask.value);
        Debug.DrawRay(transform.position, Vector2.down * _groundCheckDistance, Color.green);        // Draws the raycast line

        // if on ground
        if (hitInfo.collider != null)
        {
            _doubleJump = true;
            return true;
        }
        else {
            return false;
        }
    }
}
