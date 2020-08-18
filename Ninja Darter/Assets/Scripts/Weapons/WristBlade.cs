using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*- Shuriken
	- Call its function in a coroutine so you can wait for the animation to finish and let the state play out normally
		- while in the wrist blade state, decrease the player's movement speed by half
*/

/*
Sources:
1) B., Brackeys, '2D Shooting in Unity (Tutorial)', 2018. [Online]. Available: https://www.youtube.com/watch?v=wkKsl1Mfp5M [Accessed: Aug-16-2020].
*/

public class WristBlade : MonoBehaviour {

    [SerializeField] private float _throwSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private GameObject _playerObject;
    private WristBladeWeapon _wristBladeWeapon;
    private int _randomDropChance = 0;
    private const int _MIN_DROP_CHANCE = 1, _MAX_DROP_CHANCE = 30;

    void Awake() { 
        _playerObject = GameObject.Find("Player");
        _wristBladeWeapon = _playerObject.GetComponent<WristBladeWeapon>();
    }

    void Start() => _rigidbody2D.velocity = transform.right * _throwSpeed;
    
    /*A random number between 1 and 100
    if the number falls between 1 and the break chance, break the knife*/

    void KnifeBreakChance(int _breakChanceNum, int _minValue, int _maxValue) {
        int _breakChanceRandom = Random.Range(_minValue, _maxValue);

        if (_breakChanceRandom >= 1 && _breakChanceRandom <= _breakChanceNum)
            Destroy(gameObject);
    }
    
    void OnCollisionEnter2D (Collision2D collision) {
        
        switch(collision.gameObject.name) {
            case "Enemy":
                KnifeBreakChance(30, 1, 100);
                //damage them
                break;
            case "Player":
                _wristBladeWeapon.AddKnife(1);
                Destroy(gameObject);
                break;
            default:
                break;
        }
        // play sound
        // play spark animation
    }
}
