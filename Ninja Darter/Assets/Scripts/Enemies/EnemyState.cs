using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {

    public State _state;
    public enum State { Moving, Player_Sighted }

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _groundCheckPosition;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _whatIsGround;
    private bool _isMoving;
    private bool _isTouchingGround;
    private bool _isFacingRight;

    [SerializeField] private Transform _sightPosition;
    [SerializeField] private float _sightDistance;
    [SerializeField] private LayerMask _whatIsPlayer;
    private RaycastHit2D _sightInfo;

    public void SetState(State _state) => this._state = _state;
    public State GetState () { return _state; }

    void Start() { 
        _isMoving = true; 
        _isFacingRight = true;
    }

    void Update() { 
        CheckCurrentState();
        
        if (_isMoving)
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    private void CheckCurrentState() {
        Moving();
    }

    private void RunCodeBasedOnState() {
        switch(_state) {
            case State.Moving:
                CheckForGround();
                break;
            case State.Player_Sighted:
                break;
            default:
                Debug.Log("no state");
                break;
        }
    }

    bool Moving() {
        
        if (_isMoving) {
            SetState(State.Moving);
            return true;
        } else
            return false;
    }

    public bool PlayerSighted() {
        
        // if (_isFacingRight)
        _sightInfo = Physics2D.Raycast(_sightPosition.position, Vector2.right, _sightDistance, _whatIsPlayer);


        if (_sightInfo) {
            // fire three bullets after a 0.5 second delay, use a coroutine
            SetState(State.Player_Sighted);
            _isMoving = false;
            return true;
        } else {
            SetState(State.Moving);
            _isMoving = true;
            return false;
        }
    }

    void CheckForGround() {

        _isTouchingGround = Physics2D.OverlapCircle(_groundCheckPosition.position, _groundCheckRadius, _whatIsGround);

        if (!_isTouchingGround)
            transform.Rotate(0f, 180f, 0f);

    }

}