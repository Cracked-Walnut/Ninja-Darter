using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    [SerializeField] CharacterController2D _characterController2D;
    [Range(20, 60)] [SerializeField] private float _runSpeed;
    [SerializeField] private int _health;
    private Rigidbody2D _rigidBody2D;
    private float _horizontalMove;
    private bool _isJumping = false;
    private bool _isCrouching = false;

    private State _state;
    private enum State { Idle, Running, Dashing, Crouching, Attacking, InAir, Ledge_Climb, Wall_Grab, Wall_Climbing, Hurt, Dead }

    private void SetState(State _state) { this._state = _state; }
    private State GetState() { return _state; }

    void Start() {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _state = State.Idle;
    }

    void Update() {
        CheckCurrentState();
         _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;

        if (Input.GetButtonDown("Jump")) {
            _isJumping = true;
        }
    }

    void FixedUpdate() {
        _characterController2D.Move(_horizontalMove * Time.fixedDeltaTime, _isCrouching, _isJumping); // fixedDeltaTime ensures we move the same amount no matter how many times Move() is called
        _isJumping = false;
        CheckCurrentFixedState();
    }

    private void CheckCurrentState() { /*Check non-physics related States*/
        Idle();
        Hurt();
        Dead();
        Debug.Log(_state);
    }

    private void CheckCurrentFixedState() { /*Check physics related States*/
        Running();
        InAir();
        Crouching();
        Debug.Log(_state);
    }

    /*<-------------->-This function will run any code based on the current state-<------------------------------->*/
    /*<------->-This function serves to hold extra code so as to not crowd the State Functions-<------->*/
    private void RunCodeBasedOnState() {
        switch(_state) {
            case State.Idle:
                break;
            case State.Running:
                break;
            case State.Crouching:
                break;
            case State.InAir:
                break;
            case State.Hurt:
                break;
            case State.Dead:
                break;
            default:
                Debug.Log("Waiting to run code...");
                break;
        }
    }

    /*<------------------------------->-State Functions-<------------------------------->*/
    /*<--------->-These functions hold the bare minimum to achieve the desired state-<--------->*/
    bool Idle() {
        SetState(State.Idle);
        return true;    
    }

    bool Running() {
        /*Play running anim*/
       if (_horizontalMove > 0.5f || _horizontalMove < -0.5f) {
            SetState(State.Running);
            return true;
        } else
            return false;
    }

    // bool Dashing() {}

    bool Crouching() {
        if (Input.GetButton("Crouch")) {
            SetState(State.Crouching);
            _isCrouching = true;
            Debug.Log("Crouching: " + _isCrouching);
            return _isCrouching;
        } else  {
            _isCrouching = false;
            Debug.Log("Crouching: " + _isCrouching);
            return _isCrouching;
        }
    }

    // bool Attacking() {}

    bool InAir() {
        if (!_characterController2D.getGrounded()) {
            SetState(State.InAir);
            return true;
        } else
            return false;
    }

    // bool Ledge_Climb() {}

    // bool Wall_Grab() {}

    // bool Wall_Climbing() {}

    void TakeDamage(int damage) {  _health -= damage; }

    bool Hurt() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            TakeDamage(20);
            SetState(State.Hurt);
            return true;
        } else
            return false;
    }

    bool Dead() {
        if (_health <= 0) {
            SetState(State.Dead);
            Time.timeScale = 0.0f;
            return true;
        } else
            return false;
    }
    /*<------------------------------->-End of State Functions-<------------------------------->*/
}

/*
Sources:
1) B., Brackeys, '2D Movement in Unity, 2018. [Online]. Available: https://www.youtube.com/watch?v=dwcT-Dch0bA [Accessed: Aug-08-2020].
*/
