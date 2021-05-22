using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [HideInInspector] public Rigidbody2D _rb;
    public float Health { get; set; }


    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private GameObject _playerBody;        // Main child of Player for flipping sprites and gun and fireing point

    private float _moveX;                                   // Gets the movement vector; For flipping the player faceing direction
    private bool _isGrounded;
    private bool _doubleJump = false;
    

    public float _startHealth;     // test : for now mush show in inspector ; temporarily setting it to health



    private int faceingDir = 1;      // 1 == right, -1 == left;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Health = _startHealth;      // test ; temp setting startHealth to health
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

        // To find which way is the player looking
        if (_moveX > 0)
        {
            Flip(true);             // for flipping the player dir
            faceingDir = 1;
        }
        else if (_moveX < 0)
        {
            Flip(false);
            faceingDir = -1;
        }
    }


    // Change player faceing dir based on movement
    private void Flip(bool faceingRight) {
        if (faceingRight)
        {
            _playerBody.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            _playerBody.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
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

    public void Damage( int damageAmount){     // From IDamageable interface; damageAmount from Attack.cs
        if (Health < 1)                        // so that the health wont be decreased on reaching 0 and go to minus value
            return;

        Health -= damageAmount;         
        
        if(Health < 1){
            Debug.Log("PLAYER IS DEAD!!!!!!!");

            LifeManager.Instance.TakeLife();
            if (LifeManager.Instance.lifeCounter > 1) {
                GameManager.Instance.RespawnPlayer();
            }
            else { 
                // reset player stat health to max health
            }

            // if there is more life for player then reset player in GameManager
        }
    }
}
