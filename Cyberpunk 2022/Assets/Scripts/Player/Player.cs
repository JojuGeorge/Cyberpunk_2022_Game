using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [HideInInspector] public Rigidbody2D _rb;
    public int Health { get; set; }                         // Player Health is initialized in the GameManager.LoadFromPlayerSaveData()

    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private GameObject _playerBody;        // Main child of Player for flipping sprites and gun and fireing point

    private float _moveX;                                   // Gets the movement vector; For flipping the player faceing direction
    private bool _isGrounded;
    private bool _doubleJump = false;
    private PlayerAnimation _playerAnimation;               // For managing player animations - script attached to the same playerGO

    private int faceingDir = 1;      // 1 == right, -1 == left;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimation = FindObjectOfType<PlayerAnimation>();
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


        // Shooting animation
        if (Input.GetButton("Fire1")) {
            _playerAnimation.ShootingPistol();
        }
    }


    // Player movement
    private void Movement() {
        _moveX = Input.GetAxisRaw("Horizontal") * _walkSpeed;
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

        _playerAnimation.Walk(_moveX);      // for plaer walk animation

        // For Running. Player speed is increased and Run animation is played when on pressing down the shift button continously
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _rb.velocity = new Vector2(_moveX * _runSpeed, _rb.velocity.y);
            _playerAnimation.Run(true);
        }
        else {
            _playerAnimation.Run(false);
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
        Debug.DrawRay(transform.position, Vector2.down * _groundCheckDistance, Color.red);        // Draws the raycast line

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
            if (LifeManager.Instance.life >= 1) {
                GameManager.Instance.RespawnPlayer();
            }
            else {
               // _playerData.health = _playerData.maxHealth;             // reset player health on death
            }

            // if there is more life for player then reset player in GameManager
        }
        GameManager.Instance.PopulateToPlayerSaveData();
    }
}
