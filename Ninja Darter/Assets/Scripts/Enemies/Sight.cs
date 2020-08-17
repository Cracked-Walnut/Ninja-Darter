using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour {

    
    [SerializeField] private Transform _sightPosition;
    [SerializeField] private float _sightDistance;
    [SerializeField] private LayerMask _whatisPlayer;
    private RaycastHit2D _sightInfo;
    
    // if detected, stop moving
    // make a function that returns a boolean, use it in Patrol to stop and move rigidbody2D
    public bool CheckForPlayer() {
        
        _sightInfo = Physics2D.Raycast(_sightPosition.position, Vector2.right, _sightDistance, _whatisPlayer);

        if (_sightInfo) {
            // fire three bullets after a 0.5 second delay, use a coroutine
            return true;
        }

        return false;
    }


}
