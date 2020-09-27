using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemState : MonoBehaviour {

    [SerializeField] private State _state;
    [SerializeField] private enum State { Idling, Picked_Up, Destroyed }

    private void SetState(State _state) => this._state = _state;
    private State GetState () { return _state; }

    void Start() => _state = State.Idling;
    
    void Update() { 
        
        CheckCurrentState(); 
        RunCodeBasedOnState();
    }
    
    private void RunCodeBasedOnState() {
        switch(_state) {
            case State.Idling:
                break;
            case State.Picked_Up:
                Destroy(gameObject);
                break;
            case State.Destroyed:
                Destroy(gameObject);
                break;
            default:
                Debug.Log("NOT IN A STATE (Idling, Picked_Up, Destroyed)");
                break;
        }
    }

    void CheckCurrentState() {
        Idling();
        // Picked_Up();
        Destroyed();
    }

    bool Idling() {
        SetState(State.Idling);
        return true;
    }

    // bool Picked_Up() {
    //     void OnCollisionEnter2D(Collision2D collider) {
    //         if (collider.gameObject.name == "Player") {
    //             SetState(State.Picked_Up);
    //             return true;
    //         }
    //     return false;
    //     }
    // }

    bool Destroyed() {
        SetState(State.Destroyed);
        return true;
    }
}
