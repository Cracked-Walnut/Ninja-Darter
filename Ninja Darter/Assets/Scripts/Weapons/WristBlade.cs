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

    void Awake() { 
        _playerObject = GameObject.Find("Player");
        _wristBladeWeapon = _playerObject.GetComponent<WristBladeWeapon>();
    }

    void Start() => _rigidbody2D.velocity = transform.right * _throwSpeed;
    
    void OnCollisionEnter2D (Collision2D collision) {
        
        switch(collision.gameObject.name) {
            case "Enemy":
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
