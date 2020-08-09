using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    [SerializeField] CharacterController2D _characterController2D;
    [SerializeField] private int _health;

    private float _runSpeed;
    private const float DEFAULT_RUN_SPEED = 40f;
    private float _horizontalMove;

    [Header("Dashing")]
    [SerializeField] private bool _canDash = true;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _timeBtwDashes;

    private Rigidbody2D _rigidBody2D;

    // ensures we can't move during any potential cutscenes or other instances
    private bool _isJumping = false;
    private bool _isCrouching = false;
    private bool _canMove = true;
    
    private State _state;
    private enum State { Idling, Running, Dashing, Crouching, Attacking, InAir, Ledge_Climb, Wall_Grab, Wall_Climbing, Hurt, Dead }

    private void SetState(State _state) { this._state = _state; }
    private State GetState() { return _state; }

    void Start() {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _state = State.Idling;
        _runSpeed = DEFAULT_RUN_SPEED;
    }

    void Update() {
        CheckCurrentState();
         _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;

        if (Input.GetButtonDown("Jump")) {
            _isJumping = true;
        }
    }

    void FixedUpdate() {
        _characterController2D.Move(_horizontalMove * Time.fixedDeltaTime, _isCrouching, _isJumping);  // fixedDeltaTime ensures we move the same amount no matter how many times Move() is called
        _isJumping = false;
        CheckCurrentFixedState();
    }

    private void CheckCurrentState() { /*Check non-physics related States*/
        Idling();
        Dashing();
        Hurt();
        Dead();
        RunCodeBasedOnState();
        Debug.Log(_state);
    }

    private void CheckCurrentFixedState() { /*Check physics related States*/
        InAir();
        Crouching();
        RunFixedCodeBasedOnState();
        Running();
        Debug.Log(_state);
    }

    /*<-------------->-Run extra non-physics related code-<------------------------------->*/
    private void RunCodeBasedOnState() {
        switch(_state) {
            case State.Idling:
                break;
            case State.Dashing:
                DashAbility();
                break;
            case State.Hurt:
                break;
            case State.Dead:
                break;
            default:
                Debug.Log("NOT IN A STATE");
                break;
        }
    }

    /*<-------------->-Run extra physics related code-<------------------------------->*/
    private void RunFixedCodeBasedOnState() {
        switch(_state) {
            case State.Running:
                break;
            case State.Crouching:
                break;
            case State.InAir:
                break;
            default:
                Debug.Log("NOT IN A STATE");
                break;
        }
    }

    /*<------------------------------->-State Functions-<------------------------------->*/
    /*<--------->-These functions hold the bare minimum to achieve the desired state-<--------->*/
    bool Idling() {
        SetState(State.Idling);
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

    bool Dashing() {
        if (_canMove && _canDash && Idling() || Running() || InAir()) {
            if (Input.GetKeyDown(KeyCode.RightShift)) {
                SetState(State.Dashing);
                return true;
            }
        }
        return false;
    }

    bool Crouching() {
        if (Input.GetButton("Crouch")) {
            SetState(State.Crouching);
            _isCrouching = true;
            return _isCrouching;
        } else  {
            _isCrouching = false;
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
    
    
    void TakeDamage(int damage) {  _health -= damage; }

    private void DashAbility() {
        StartCoroutine(Dash());
    }

    IEnumerator Dash() {
        _canDash = false;
        _runSpeed = _dashSpeed;
        yield return new WaitForSeconds(_dashTime);
        _runSpeed = DEFAULT_RUN_SPEED;
        yield return new WaitForSeconds(_timeBtwDashes);
        _canDash = true;
    }
}

/*
Sources:
1) B., Brackeys, '2D Movement in Unity, 2018. [Online]. Available: https://www.youtube.com/watch?v=dwcT-Dch0bA [Accessed: Aug-08-2020].
2) B.B., Bonk, 'Unity Ground Dash and Dash Jump Tutorial', 2019. [Online]. Available: https://www.youtube.com/watch?v=I4Ja5Ar63Pw [Accessed: Aug-09-2020].
*/
