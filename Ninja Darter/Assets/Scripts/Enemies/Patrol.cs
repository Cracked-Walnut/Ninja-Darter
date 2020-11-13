using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;
    private const float DEFAULT_SPEED = 2f;
    [SerializeField] private float _waitTime;
    [SerializeField] private Transform _groundCheckPosition;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _whatIsGround;
    private bool _isMoving;
    private bool _isTouchingGround;

    [SerializeField] private Transform _sightPosition;
    [SerializeField] private float _sightDistance;
    [SerializeField] private LayerMask _whatIsPlayer;
    private RaycastHit2D _sightInfo;
    private Enemy _enemy;

    public bool GetMoving() { return _isMoving; }
    public void SetMoving(bool _boolMove) => _boolMove = _isMoving;

    void Start() => _isMoving = true;

    void Awake() => _enemy = GetComponent<Enemy>();

    void Update() {
        
        CheckForGround();
        
        if (!_isMoving)
            return;
        else
            transform.Translate(Vector2.left * _speed * Time.deltaTime);

        // if (!_enemy._isDead)
            // SpotPlayer();
    }

    void CheckForGround() {
        _isTouchingGround = Physics2D.OverlapCircle(_groundCheckPosition.position, _groundCheckRadius, _whatIsGround);

        if (!_isTouchingGround) {
            transform.Rotate(0f, 180f, 0f);
        }
    }

    public void SpotPlayer() {
        
        _sightInfo = Physics2D.Raycast(_sightPosition.position, Vector2.right, _sightDistance, _whatIsPlayer);

        if (_sightInfo)
            _isMoving = false;
        else
            _isMoving = true;
    }
}
