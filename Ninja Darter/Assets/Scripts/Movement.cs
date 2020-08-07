using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    /*Use the Rigidbody 2D and the arrow keys to contol the player
        I don't think I'll need a CharacterController class*/

    private State state;
    private enum State { Idle, Running, Rolling, Attacking, Jump_Up, Jump_Down, Ledge_Climb, Wall_Grab, Wall_Climbing, Taking_Damage, Dead }

    private Rigidbody2D rigidbody2D;
    [SerializeField] private float runSpeed;

    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        state = State.Idle;
    }

    void Update() {
        Debug.Log(state);
        CheckState();
    }

    private void SetState(State state) { this.state = state; }

    private State GetState(State state) { return state; }

    private void CheckState() {
        Idle();
        Running();
    }

    void Idle() {
        /*Play idle animation*/
        if (rigidbody2D.velocity.x > -1 && rigidbody2D.velocity.x < 1 && rigidbody2D.velocity.y > -1 && rigidbody2D.velocity.y < 1)
            
            SetState(state.Idle);
    }

    void Running() {
        /*Play running anim
        Move Transform*/
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.position = new Vector2(1, 0) * runSpeed * Time.deltaTime;
            SetState(State.Running);
        }
            
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.position = new Vector2(-1, 0) * runSpeed * Time.deltaTime;
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
