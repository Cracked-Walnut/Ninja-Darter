using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Sources:
1) B., Brackeys, '2D Shooting in Unity (Tutorial)', 2018. [Online]. Available: https://www.youtube.com/watch?v=wkKsl1Mfp5M [Accessed: Aug-16-2020].
*/

public class Bow : MonoBehaviour {

    [SerializeField] private float _arrowSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private GameObject _player;
    private PlayerState _playerState;
    private Arrow _arrow;

    void Awake() {
        _player = GameObject.Find("Player");
        _arrow = _player.GetComponent<Arrow>();
        _playerState = _player.GetComponent<PlayerState>();
    }

    void Start() => _rigidbody2D.velocity = transform.right * _arrowSpeed;

    void ArrowBreakChance(int _breakChanceNum, int _minValue, int _maxValue) {
    int _breakChanceRandom = Random.Range(_minValue, _maxValue);

    if (_breakChanceRandom >= 1 && _breakChanceRandom <= _breakChanceNum)
        Destroy(gameObject);
    }

    void OnCollisionEnter2D (Collision2D _collision) {

        switch(_collision.gameObject.name) {
            
            case "Earth Wisp":
                _collision.gameObject.GetComponent<Enemy>().TakeDamage(_playerState.GetMaxBowAttackDamage());
                ArrowBreakChance(30, 1, 100);
                break;

            case "Assault Droid":
                _collision.gameObject.GetComponent<Enemy>().TakeDamage(_playerState.GetMaxBowAttackDamage());
                ArrowBreakChance(30, 1, 100);
                break;

            case "Player":
                _arrow.AddArrow(1);
                Destroy(gameObject);
                break;
        }
        // play sound
        // play spark animation
    }

}
