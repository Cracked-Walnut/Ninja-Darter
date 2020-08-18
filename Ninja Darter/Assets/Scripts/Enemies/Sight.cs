using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour {

    [SerializeField] private Transform _sightPosition;
    [SerializeField] private float _sightDistance;
    [SerializeField] private LayerMask _whatIsPlayer;
    private RaycastHit2D _sightInfo;
    private Patrol _patrol;

    void Awake() => _patrol = GetComponent<Patrol>();

    public bool SpotPlayer() {
        
        _sightInfo = Physics2D.Raycast(_sightPosition.position, Vector2.right, _sightDistance, _whatIsPlayer);

        if (_sightInfo) {
            // fire three bullets after a 0.5 second delay, use a coroutine
            _patrol.SetMoving(false);
            return true;
        } else {
            _patrol.SetMoving(true);
            return false;
        }
    }
}
