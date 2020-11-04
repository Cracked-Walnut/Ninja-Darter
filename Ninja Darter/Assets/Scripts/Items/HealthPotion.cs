using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour {

    [SerializeField] private int _replenishValue;
    [SerializeField] private GameObject _player;
    private bool _isPickedUp;
    private PlayerState _playerState;

    // void Awake() => _playerState = _player.GetComponent<PlayerState>();
    
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject == _player) {

            _playerState = _player.GetComponent<PlayerState>();
            
            Debug.Log("_playerState.GetHealth() = " + _playerState.GetHealth());

            if (!_isPickedUp) {
                if (_playerState.GetHealth() == _playerState.GetMaxHealth()) {
                    Debug.Log("Health is Full");
                    return;
                }
                else {

                    if (_playerState._health < _playerState._maxHealth) {
                        _playerState.AddHealth(_replenishValue);
                        _isPickedUp = true;
                        Destroy(gameObject);
                    }

                    if (_playerState._health > _playerState._maxHealth) {
                        _playerState.SetHealth(_playerState._maxHealth);
                        _isPickedUp = true;
                        Destroy(gameObject);
                    }
                }

            }

        }
    }
}
