using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _groundCheckPosition;
    [SerializeField] private int _groundCheckDistance;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _whatIsGround;
    private bool _isMoving;
    private bool _isTouchingGround;

    [SerializeField] private Transform _sightPosition;
    [SerializeField] private float _sightDistance;
    [SerializeField] private LayerMask _whatIsPlayer;
    private RaycastHit2D _sightInfo;

    void Start() => _isMoving = true;

    void Update() { 
        
        CheckForGround();
        
        if (_isMoving)
            transform.Translate(Vector2.right * _speed * Time.deltaTime);

        CheckForPlayer();
    }

    void CheckForGround() {
        _isTouchingGround = Physics2D.OverlapCircle(_groundCheckPosition.position, _groundCheckRadius, _whatIsGround);

        if (!_isTouchingGround)
            transform.Rotate(0f, 180f, 0f);
    }

    public bool CheckForPlayer() {
        
        _sightInfo = Physics2D.Raycast(_sightPosition.position, Vector2.right, _sightDistance, _whatIsPlayer);

        if (_sightInfo) {
            // fire three bullets after a 0.5 second delay, use a coroutine
            _isMoving = false;
            return true;
        } else {
            _isMoving = true;
            return false;
        }

    }
}
