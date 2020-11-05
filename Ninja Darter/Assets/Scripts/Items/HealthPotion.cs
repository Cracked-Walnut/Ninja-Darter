﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour {

    [SerializeField] private bool _isShopItem; // you cannot collect an item if it's in a shop. You'll have to buy it
    [SerializeField] private int _replenishValue;
    [SerializeField] private GameObject _player;
    [SerializeField] private int _shopPrice;
    private bool _isPickedUp;
    private PlayerState _playerState;
    private Inventory _inventory;

    public int GetShopPrice() { return _shopPrice; }
    public int GetReplenishValue() { return _replenishValue; }
    
    // this logic is for making contact with the item in the game.
    // there will be other logic when buying the item in a shop.
    // that logic will be located in player state
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject == _player) {

            _playerState = _player.GetComponent<PlayerState>();

            if (!_isPickedUp) {
                if (_playerState.GetHealth() == _playerState.GetMaxHealth()) {
                    Debug.Log("Health is Full");
                    return;
                }
                else {

                    if (_playerState.GetHealth() < _playerState.GetMaxHealth())
                        _playerState.AddHealth(_replenishValue);
                    
                    if (_playerState.GetHealth() > _playerState.GetMaxHealth())
                        _playerState.SetHealth(_playerState.GetMaxHealth());

                    _isPickedUp = true;
                    Destroy(gameObject);
                }
            }
        }
    }

    // this function will be executed when the player presses the HP button in shop
    public void BuyHPInShop() {
        _inventory = _player.GetComponent<Inventory>();

        if (_inventory.GetCoins() > _shopPrice) {
            if (_playerState.GetHealth() >= _playerState.GetMaxHealth()) {
                // play error sound
                Debug.Log("Health Full");
                return;
            } else {
                // play Healing sound
                _playerState.AddHealth(_replenishValue);

                if (_playerState.GetHealth() > _playerState.GetMaxHealth())
                    _playerState.SetHealth(_playerState.GetMaxHealth());
            }
        }
        else {
            Debug.Log("Not enough coins to buy");
        }
    }
}
