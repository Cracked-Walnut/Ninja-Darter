using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    /*Use the Rigidbody 2D and the arrow keys to contol the player
        I don't think I'll need a CharacterController class*/

    [SerializeField] CharacterController2D _characterController2D;
    [SerializeField] private float _runSpeed;
    private Rigidbody2D _rigidBody2D;
    private float _horizontalMove;
    private bool _isJumping = false;
    private bool _isCrouching = false;

    private State _state;
    private enum State { Idle, Running, Rolling, Crouching, Attacking, InAir, Ledge_Climb, Wall_Grab, Wall_Climbing, Taking_Damage, Dead }

    private void SetState(State _state) { this._state = _state; }
    private State GetState() { return _state; }

    void Start() {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _state = State.Idle;
    }

    void Update() {
         _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;

        if (Input.GetButtonDown("Jump")) {
            _isJumping = true;
        }

    }

    void FixedUpdate() {
        _characterController2D.Move(_horizontalMove * Time.fixedDeltaTime, _isCrouching, _isJumping); // fixedDeltaTime ensures we move the same amount no matter how many times Move() is called
        _isJumping = false;
        CheckState();
    }



    private void CheckState() {
        Idle();
        Running();
        InAir();
        Crouching();
        Debug.Log(_state);
    }

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

    // bool Rolling() {}

    bool Crouching() {
        if (Input.GetButton("Crouch")) {
            SetState(State.Crouching);
            _isCrouching = true;
            Debug.Log("Crouching: " + _isCrouching);
            return _isCrouching;
        } else/* if (Input.GetButtonUp("Crouch"))*/ {
            // SetState(State.Idle);
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

    // bool Taking_Damage() {}

    // bool Dead() {}

}

/*
Sources:
1) B., Brackeys, '2D Movement in Unity, 2018. [Online]. Available: https://www.youtube.com/watch?v=dwcT-Dch0bA [Accessed: Aug-08-2020].
*/
