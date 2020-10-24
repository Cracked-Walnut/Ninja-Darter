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
    private Arrow _arrow;

    void Awake() {
        _player = GameObject.Find("Player");
        _arrow = _player.GetComponent<Arrow>();
    }

    void Start() => _rigidbody2D.velocity = transform.right * _arrowSpeed;

    void ArrowBreakChance(int _breakChanceNum, int _minValue, int _maxValue) {
    int _breakChanceRandom = Random.Range(_minValue, _maxValue);

    if (_breakChanceRandom >= 1 && _breakChanceRandom <= _breakChanceNum)
        Destroy(gameObject);
    }

    void OnCollisionEnter2D (Collision2D collision) {
        
        switch(collision.gameObject.name) {
            case "Enemy":
                ArrowBreakChance(30, 1, 100);
                //damage them
                break;
            case "Player":
                _arrow.AddArrow(1);
                Destroy(gameObject);
                break;
            default:
                break;
        }
        // play sound
        // play spark animation
    }

}
