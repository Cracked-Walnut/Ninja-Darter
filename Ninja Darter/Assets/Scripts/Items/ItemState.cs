using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemState : MonoBehaviour {

    public State _state;
    public enum State { Idling, Picked_Up, Destroyed }

    public void SetState(State _state) => this._state = _state;
    public State GetState () { return _state; }

    void Start() => _state = State.Idling;
    void Update() => CheckCurrentState();
    
    private void RunCodeBasedOnState() {
        switch(_state) {
            case State.Idling:
                break;
            case State.Picked_Up:
                break;
            case State.Destroyed:
                break;
            default:
                Debug.Log("NOT IN A STATE (Idling, Picked_Up, Destroyed)");
                break;
        }
    }

    void CheckCurrentState() {
        Idling();
        Picked_Up();
        Destroyed();
    }

    bool Idling() {
        SetState(State.Idling);
        return true;
    }

    bool Picked_Up() {
        SetState(State.Picked_Up);
        return true;
    }

    bool Destroyed() {
        SetState(State.Destroyed);
        return true;
    }
}
