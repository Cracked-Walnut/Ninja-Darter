using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Sources:
1) B., Brackeys, '2D Shooting in Unity (Tutorial)', 2018. [Online]. Available: https://www.youtube.com/watch?v=wkKsl1Mfp5M [Accessed: Aug-16-2020].
*/

public class WristBlade : MonoBehaviour {

    [SerializeField] private float _throwSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;
    private GameObject _playerObject;
    private WristBladeWeapon _wristBladeWeapon;

    void Awake() { 
        _playerObject = GameObject.Find("Player");
        _wristBladeWeapon = _playerObject.GetComponent<WristBladeWeapon>();
    }

    void Start() { 
        _rigidbody2D.velocity = transform.right * _throwSpeed;
        _animator.SetTrigger("Idling");
    }
    
    /*A random number between 1 and 100
    if the number falls between 1 and the break chance, break the knife*/

    void KnifeBreakChance(int _breakChanceNum, int _minValue, int _maxValue) {
        int _breakChanceRandom = Random.Range(_minValue, _maxValue);

        if (_breakChanceRandom >= 1 && _breakChanceRandom <= _breakChanceNum)
            Destroy(gameObject);
    }
    
    void OnCollisionEnter2D (Collision2D _collision) {
        
        switch(_collision.gameObject.tag) {
            case "Enemy":
                _collision.gameObject.GetComponent<Enemy>().TakeDamage(50);
                _animator.SetTrigger("Destroyed");
                break;
            case "Player":
                Destroy(gameObject);
                break;
            default:
                _animator.SetTrigger("Destroyed");
                break;
        }

        // play sound in Destroyed Trigger
    }

    public void DestroyProjectile() => Destroy(gameObject);
}
