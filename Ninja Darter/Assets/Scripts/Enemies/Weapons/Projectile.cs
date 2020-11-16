using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy projectile to damage the player

public class Projectile : MonoBehaviour {

    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GameObject _player;
    private Animator _animator;
    private float _selfDestructTimer;
    private PlayerState _playerState;

    void Start() {
        _selfDestructTimer = 6.0f;
        _animator.SetTrigger("Idling");
    } 

    void Awake() { 
        _playerState = _player.GetComponent<PlayerState>();
        _animator = GetComponent<Animator>();
    }

    void Update() {
        _rigidbody2D.velocity = transform.right * _speed;
        CheckSelfDestructTime();
    }

    void OnCollisionEnter2D(Collision2D _collisionInfo) {
        _animator.SetTrigger("Destroyed");
    }
    
    void CheckSelfDestructTime() {
        _selfDestructTimer -= Time.deltaTime;
        if (_selfDestructTimer <= 0.0f) {;
            _animator.SetTrigger("Destroyed");
        }
    }

    public void DestroyProjectile() => Destroy(gameObject);

}
