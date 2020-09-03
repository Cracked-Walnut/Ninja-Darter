﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Sources:
1) B., Brackeys, '2D Movement in Unity', 2018. [Online]. Available: https://www.youtube.com/watch?v=dwcT-Dch0bA [Accessed: Aug-08-2020].
2) B.B., Bonk, 'Unity Ground Dash and Dash Jump Tutorial', 2019. [Online]. Available: https://www.youtube.com/watch?v=I4Ja5Ar63Pw [Accessed: Aug-09-2020].
3) B., Blackthornprod, 'How to make a 2D Wall Jump & Wall Slide using Unity & C#!', 2020. [Online]. Available: https://www.youtube.com/watch?v=KCzEnKLaaPc [Accessed: Aug-10-2020].
*/

public class PlayerState : MonoBehaviour {

    private Rigidbody2D _rigidBody2D;
    
    public State _state;
    public enum State { Idling, Running, Crouching, Attacking, InAir, Wall_Sliding, Wall_Climbing, On_Wall, Dashing,  Hurt, Dead }

    [SerializeField] private CharacterController2D _characterController2D; // reference to the script that gives our player movement
    private WristBlade _wristBlade;

    [Header("Health")]
    [SerializeField] private int _health;
    [SerializeField] private LayerMask _whatIsEnemy;


    [Header("Animator")]
    [SerializeField] private Animator _animator;

    [Header("Jumping")]
    // used to contol the behaviour of our jump
    [Range(2f, 10f)] [SerializeField] private float _fallMultiplier = 2.5f;
    [Range(1f, 5f)] [SerializeField] private float _lowMultiplier = 1.5f;

    [Header("Running")]
    [SerializeField] private float _runSpeed;
    private const float DEFAULT_RUN_SPEED = 40f; // modify as needed
    private float _horizontalMove; // will equal 1 if moving right, -1 if moving left. Multipled with _runSpeed
    private float _horizontalXboxMove;

    [Header("Dashing")]
    [SerializeField] private bool _canDash = true;
    [Range(0, 200)] [SerializeField] private float _dashSpeed;
    [Range(0, 1)] [SerializeField] private float _dashTime; // the time you remain in a dash
    [Range(0, 1)] [SerializeField] private float _timeBtwDashes;

    [Header("Wall Logic")]
    [Range(0.05f, 1.2f)] [SerializeField] private float _wallCheckRadius; // The radius of the circle that detects walls
    [SerializeField] private Transform _wallCheckOriginTop, _wallCheckOriginBottom; // we draw a circle here to check for walls
    [Range(0, 10f)] [SerializeField] private float _wallSlideSpeed;
    [Range(0, 10f)] [SerializeField] private float _wallClimbSpeed;
    [SerializeField] private LayerMask _whatIsWall; // determines what we can wall slide off
   
    [Header("Misc Booleans")]
    [SerializeField] private bool _canMove = true;
    [SerializeField] private bool _isLateJump = true;
    // ensures we can't move during any potential cutscenes or other instances
    private bool _isJumping = false;
    private bool _isCrouching = false;
    private bool _isTouchingWallTop = false, _isTouchingWallBottom = false;
    private bool _isWallSliding;
    
    public void SetState(State _state) => this._state = _state;
    public State GetState () { return _state; }

    public void SetHealth(int _health) => this._health = _health;
    public int GetHealth() { return _health; }

    void Start() {
        _state = State.Idling;
        _runSpeed = 40;
    }

    void Awake() { 
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _wristBlade = FindObjectOfType<WristBlade>();
    }

    void Update() {
        CheckCurrentState();

        if (_canMove) {
            _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;
            _horizontalXboxMove = Input.GetAxisRaw("L-Stick-Horizontal") * _runSpeed;
        }

        if (Input.GetButtonDown("XboxA"))
            _isJumping = true;
        
        if (_rigidBody2D.velocity.y < 0)
            _rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
        if (_rigidBody2D.velocity.y > 0 && !Input.GetButton("XboxA"))
            _rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (_lowMultiplier - 1) * Time.deltaTime;
    }

    void FixedUpdate() {
        _characterController2D.Move(_horizontalMove * Time.fixedDeltaTime, _isCrouching, _isJumping);  // fixedDeltaTime ensures we move the same amount no matter how many times Move() is called
        _characterController2D.Move(_horizontalXboxMove * Time.fixedDeltaTime, _isCrouching, _isJumping);  // fixedDeltaTime ensures we move the same amount no matter how many times Move() is called
        _isJumping = false;
        CheckCurrentFixedState();
    }

    private void CheckCurrentState() { /*Check non-physics related States*/
        Idling();
        DashAbility();
        // Wall_Sliding();
        OnWall();
        // Hurt();
        Dead();
        RunCodeBasedOnState();
    }

    private void CheckCurrentFixedState() { /*Check physics related States*/
        InAir();
        Crouching();
        Running();
        RunFixedCodeBasedOnState();
    }

    /*<-------------->-Run extra non-physics related code-<------------------------------->*/
    private void RunCodeBasedOnState() {
        switch(_state) {
            case State.Idling:
                _animator.SetTrigger("Idling");
                break;
            case State.Wall_Sliding:
                // Debug.Log(_state);
                _animator.SetTrigger("WallSliding");
                break;
            case State.Wall_Climbing:
                _animator.SetTrigger("WallClimbing");
                // Debug.Log(_state);
                WallJump();
                break;
            case State.On_Wall:
                break;
            case State.Hurt:
                // Debug.Log(_state);
                break;
            case State.Dead:
                // Debug.Log(_state);
                break;
            default:
                break;
        }
    }

    /*<-------------->-Run extra physics related code-<------------------------------->*/
    private void RunFixedCodeBasedOnState() {
        switch(_state) {
            case State.Running:
                _animator.SetTrigger("Running");
                // Debug.Log(_state);
                break;
            case State.Crouching:
                _animator.SetTrigger("Crouching");
                // Debug.Log(_state);
                break;
            case State.InAir:
                _animator.SetTrigger("Jumping");
                // Debug.Log(_state);
                break;
            default:
                break;
        }
    }

    /*<------------------------------->-State Functions-<------------------------------->*/
    /*<--------->-These functions hold the bare minimum to achieve the desired state-<--------->*/
    bool Idling() {
        if (_characterController2D.getGrounded() && _horizontalXboxMove < 0.5f && _horizontalXboxMove > -0.5f) {
            SetState(State.Idling);
            return true;    
        } else
            return false;
    }

    bool Running() {
        
        /*Play running anim*/
       if (_characterController2D.getGrounded() && !Idling() && !Crouching()) {
            SetState(State.Running);
            return true;
        } else
            return false;
    }

    bool Crouching() {
        // Debug.Log(_state);
        // if (Input.GetAxis("RT") > 0.5 && !InAir()) {
        if (Input.GetButton("XboxX") && !InAir()) {
            _isCrouching = true;
            SetState(State.Crouching);
            return _isCrouching;
        
        } else {
            _isCrouching = false;
            return _isCrouching;
        }
    }

    // bool Attacking() {}

    bool InAir() {
        if (!_characterController2D.getGrounded() && !_isTouchingWallTop && !_isTouchingWallBottom) {
            
            _animator.SetFloat("VelocityY", _rigidBody2D.velocity.y);
            SetState(State.InAir);
            
            return true;
        } else
            return false;
    }

    bool Wall_Sliding() {
        if (!_characterController2D.getGrounded()) {
            _isTouchingWallTop = Physics2D.OverlapCircle(_wallCheckOriginTop.position, _wallCheckRadius, _whatIsWall);
            _isTouchingWallBottom = Physics2D.OverlapCircle(_wallCheckOriginBottom.position, _wallCheckRadius, _whatIsWall);

            if (_isTouchingWallTop || _isTouchingWallBottom) {

                _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, Mathf.Clamp(_rigidBody2D.velocity.y, -1.5f, float.MaxValue));
                SetState(State.Wall_Sliding);
                return true;
            }
        }
        
        return false;
    }

    bool Wall_Climbing() {
        
        if (Input.GetAxis("L-Stick-Vertical") < -0.05) {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, Mathf.Clamp(_rigidBody2D.velocity.y, _wallClimbSpeed, float.MaxValue));
            SetState(State.Wall_Climbing);
            return true;
        }
        return false;
    }

    void OnWall() {

        if (!_characterController2D.getGrounded()) {

            _isTouchingWallTop = Physics2D.OverlapCircle(_wallCheckOriginTop.position, _wallCheckRadius, _whatIsWall);
            _isTouchingWallBottom = Physics2D.OverlapCircle(_wallCheckOriginBottom.position, _wallCheckRadius, _whatIsWall);

            if (_isTouchingWallTop || _isTouchingWallBottom) {

                if (Input.GetAxis("L-Stick-Vertical") < -0.05) {
                    _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, Mathf.Clamp(_rigidBody2D.velocity.y, _wallClimbSpeed, float.MaxValue));
                    SetState(State.Wall_Climbing);
                } else {
                _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, Mathf.Clamp(_rigidBody2D.velocity.y, -1.5f, float.MaxValue));
                SetState(State.Wall_Sliding);
                }
            }
        }
    }

    // bool Hurt() {
    //     TakeDamage(20);
    //     SetState(State.Hurt);
    //     return true;
    // }
    
    void TakeDamage(int _damage, float _knocBackX, float _knockBackY) {
        _health -= _damage; 
        
        if (_characterController2D.getFacingRight())
            ApplyForce(-_knocBackX, _knockBackY);
        else
            ApplyForce(_knockBackY, _knockBackY);
        
        SetState(State.Hurt);
    }  

    public bool Dead() {
        if (_health <= 0) {
            SetState(State.Dead);
            return true;
        } else
            return false;
    }
    /*<------------------------------->-End of State Functions-<------------------------------->*/

    void OnCollisionEnter2D(Collision2D _collisionInfo) {
        if (_collisionInfo.collider.name == "Spikes") {
            TakeDamage(20, 200, 900);
        }
    }

    void WallJump() {
        if (Input.GetButtonDown("XboxA"))
            ApplyForce(0, 600);
    }

    private void DashAbility() { 

        if (Input.GetButtonDown("XboxB") && Running())
            StartCoroutine(Dash());
    }

    IEnumerator Dash() {
        _canDash = false;
        SetState(State.Dashing);
        _runSpeed = _dashSpeed;
        _animator.SetTrigger("Dashing");
        // _canMove = false;

        yield return new WaitForSeconds(_dashTime);
        
        // _canMove = true;
        // _animator.SetTrigger("Idling");
        _runSpeed = DEFAULT_RUN_SPEED;
        yield return new WaitForSeconds(_timeBtwDashes);
        _canDash = true;
    }

    void ResetVelocity () => _rigidBody2D.velocity = new Vector2(0, 0);

    private void ApplyForce(float x, float y) {
        ResetVelocity();
        _rigidBody2D.AddForce(new Vector2(x, y));
    }
}

