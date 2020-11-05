﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour {

    [SerializeField] private bool _isShopItem; // you cannot collect an item if it's in a shop. You'll have to buy it
    [SerializeField] private int _replenishValue;
    [SerializeField] private GameObject _player;
    private bool _isPickedUp;
    private PlayerState _playerState;

    // void Awake() => _playerState = _player.GetComponent<PlayerState>();
    
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject == _player) {

            if (_isShopItem)
            return;

            _playerState = _player.GetComponent<PlayerState>();

            if (!_isPickedUp) {
                if (_playerState.GetHealth() == _playerState.GetMaxHealth()) {
                    Debug.Log("Health is Full");
                    return;
                }
                else {

                    if (_playerState.GetHealth() < _playerState.GetMaxHealth()) {
                        _playerState.AddHealth(_replenishValue);
                        _isPickedUp = true;
                        Destroy(gameObject);
                    }

                    if (_playerState.GetHealth() > _playerState.GetMaxHealth()) {
                        _playerState.SetHealth(_playerState.GetMaxHealth());
                        _isPickedUp = true;
                        Destroy(gameObject);
                    }
                }

            }

        }
    }
}
