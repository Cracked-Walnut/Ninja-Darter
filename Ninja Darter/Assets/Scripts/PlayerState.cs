using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    /*Use the Rigidbody 2D and the arrow keys to contol the player
        I don't think I'll need a CharacterController class*/

    private State _state;
    private enum State { Idle, Running, Rolling, Attacking, Jump_Up, Jump_Down, Ledge_Climb, Wall_Grab, Wall_Climbing, Taking_Damage, Dead }

    private Rigidbody2D _rigidBody2D;
    private float horizontalMove;
    private float _runSpeed;

    void Start() {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _runSpeed = 20;
        _state = State.Idle;
    }

    void FixedUpdate() {
        Debug.Log(_state);
        CheckState();
    }

    private void SetState(State _state) { this._state = _state; }

    private State GetState(State _state) { return _state; }

    private void CheckState() {
        Idle();
        Running();
    }

    void Idle() {
        /*Play idle animation*/
        if (_rigidBody2D.velocity.x > -1 && _rigidBody2D.velocity.x < 1 && _rigidBody2D.velocity.y > -1 && _rigidBody2D.velocity.y < 1)
            SetState(State.Idle);
    }

    void Running() {
        /*Play running anim
        Move Transform*/
        if (Input.GetKey(KeyCode.RightArrow)) {
            // _rigidBody2D.velocity = new Vector2(1, 0) * _runSpeed * Time.fixedDeltaTime;
            _rigidBody2D.AddForce(new Vector2(1, 0) * _runSpeed);
            SetState(State.Running);
        }
            
        else if (Input.GetKey(KeyCode.LeftArrow)) {
            // _rigidBody2D.velocity = new Vector2(-1, 0) * _runSpeed * Time.fixedDeltaTime;
            _rigidBody2D.AddForce(new Vector2(-1, 0) * _runSpeed);
            SetState(State.Running);
        }
    }

    void Rolling() {}

    void Attacking() {}

    void Jump_Up() {}

    void Jump_Down() {}

    void Ledge_Climb() {}

    void Wall_Grab() {}

    void Wall_Climbing() {}

    void Taking_Damage() {}

    void Dead() {}

}
