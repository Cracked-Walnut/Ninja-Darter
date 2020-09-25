using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private float _selfDestructTimer;

    void Start() => _selfDestructTimer = 2.0f;

    void Update() {
        _rigidbody2D.velocity = transform.right * _speed;
        CheckSelfDestructTime();
    }

    void OnCollisionEnter2D(Collision2D _collision) => DestroyProjectile();
    
    void CheckSelfDestructTime() {
        _selfDestructTimer -= Time.deltaTime;
        if (_selfDestructTimer <= 0.0f)
            DestroyProjectile();
    }

    void DestroyProjectile() => Destroy(gameObject);

}
