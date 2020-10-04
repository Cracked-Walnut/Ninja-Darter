using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GameObject _player;
    private float _selfDestructTimer;
    private PlayerState _playerState;

    void Start() => _selfDestructTimer = 2.0f;

    void Awake() => _playerState = _player.GetComponent<PlayerState>();

    void Update() {
        _rigidbody2D.velocity = transform.right * _speed;
        CheckSelfDestructTime();
    }

    void OnCollisionEnter2D(Collision2D _collisionInfo) => DestroyProjectile();
    
    void CheckSelfDestructTime() {
        _selfDestructTimer -= Time.deltaTime;
        if (_selfDestructTimer <= 0.0f)
            DestroyProjectile();
    }

    public void DestroyProjectile() => Destroy(gameObject);

}
