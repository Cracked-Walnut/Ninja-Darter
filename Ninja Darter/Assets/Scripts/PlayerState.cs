using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    /*Use the Rigidbody 2D and the arrow keys to contol the player
        I don't think I'll need a CharacterController class*/

    [SerializeField] CharacterController2D _characterController2D;
    [SerializeField] private float _runSpeed;
    private Rigidbody2D _rigidBody2D;
    private float horizontalMove;
    private bool jump = false;
    private bool crouch = false;

    // private State _state;
    // private enum State { Idle, Running, Rolling, Attacking, Jump_Up, Jump_Down, Ledge_Climb, Wall_Grab, Wall_Climbing, Taking_Damage, Dead }


    void Start() {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        // _state = State.Idle;
    }

    void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch")) {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }
    }

    void FixedUpdate() {
        // Debug.Log(_state);
         _characterController2D.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump); // fixedDeltaTime ensures we move the same amount no matter how many times Move() is called
        jump = false;
        // CheckState();
    }

    // private void SetState(State _state) { this._state = _state; }

    // private State GetState(State _state) { return _state; }

    // private void CheckState() {
    //     Idle();
    //     Running();
    // }

    // void Idle() {
    //     /*Play idle animation*/
    //     // if (_rigidBody2D.velocity.x > -1 && _rigidBody2D.velocity.x < 1 && _rigidBody2D.velocity.y > -1 && _rigidBody2D.velocity.y < 1)
    //         SetState(State.Idle);
    // }

    // void Running() {
    //     /*Play running anim
    //     Move Transform*/
    //     // if (Input.GetKey(KeyCode.RightArrow)) {
    //         // _rigidBody2D.velocity = new Vector2(1, 0) * _runSpeed * Time.fixedDeltaTime;
    //         // _rigidBody2D.AddForce(new Vector2(1, 0) * _runSpeed);
           
    //         SetState(State.Running);
        // }
            
        // else if (Input.GetKey(KeyCode.LeftArrow)) {
            // _rigidBody2D.velocity = new Vector2(-1, 0) * _runSpeed * Time.fixedDeltaTime;
            // _rigidBody2D.AddForce(new Vector2(-1, 0) * _runSpeed);
            // SetState(State.Running);
        // }
    // }

    // void Rolling() {}

    // void Attacking() {}

    // void Jump_Up() {}

    // void Jump_Down() {}

    // void Ledge_Climb() {}

    // void Wall_Grab() {}

    // void Wall_Climbing() {}

    // void Taking_Damage() {}

    // void Dead() {}

}

/*
Sources:
1) B., Brackeys, '2D Movement in Unity, 2018. [Online]. Available: https://www.youtube.com/watch?v=dwcT-Dch0bA [Accessed: Aug-08-2020].
*/
