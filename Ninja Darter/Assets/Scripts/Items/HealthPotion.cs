using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour {

    [SerializeField] private int _replenishValue;
    [SerializeField] private GameObject _player;
    private PlayerState _playerState;

    void Awake() => _playerState = _player.GetComponent<PlayerState>();

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == "Player") {

            if (_playerState.GetHealth() < _playerState._maxHealth)
                _playerState.AddHealth(_replenishValue);

            if (_playerState.GetHealth() > _playerState._maxHealth)
                _playerState.SetHealth(_playerState._maxHealth);
        }
    }

    void Update() {
        Debug.Log(_playerState.GetHealth());
    }
}
