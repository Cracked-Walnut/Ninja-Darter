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
    private bool _isTouchingGround;

    void Update() { 
        
        // _rigidbody2D.velocity = transform.right * _speed;
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
        CheckForGround();
    }

    void CheckForGround() {
        _isTouchingGround = Physics2D.OverlapCircle(_groundCheckPosition.position, _groundCheckRadius, _whatIsGround);

        if (!_isTouchingGround)
            transform.Rotate(0f, 180f, 0f);            
    }
}
