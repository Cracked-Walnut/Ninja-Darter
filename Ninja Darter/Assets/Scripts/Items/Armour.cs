using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : MonoBehaviour {

    [SerializeField] private bool _isShopItem; // you cannot collect an item if it's in a shop. You'll have to buy it
    [SerializeField] private int _replenishValue; // this is the value of armour that is restored upon contact/purchase
    [SerializeField] private GameObject _player;
    [SerializeField] private int _shopPrice;
    private bool _isPickedUp;
    private PlayerState _playerState;
    private Inventory _inventory;

    void Awake() => _playerState = _player.GetComponent<PlayerState>();

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject == _player) {

            if (_isShopItem)
                return;


            if (!_isPickedUp) {
                if (_playerState.GetArmour() == _playerState.GetMaxArmour()) {
                    Debug.Log("Armour is Full");
                    return;
                }
                else {

                    if (_playerState.GetArmour() < _playerState.GetMaxArmour()) {
                        _playerState.AddArmour(_replenishValue);
                        _isPickedUp = true;
                        Destroy(gameObject);
                    }

                    if (_playerState.GetArmour() > _playerState.GetMaxArmour()) {
                        _playerState.SetArmour(_playerState.GetMaxArmour());
                        _isPickedUp = true;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    // this function will be executed when the player presses the Armour button in shop
    public void BuyArmourInShop() {
        _inventory = _player.GetComponent<Inventory>();

        if (_inventory.GetCoins() >= _shopPrice) {
            if (_playerState.GetArmour() == _playerState.GetMaxArmour()) {
                // play error sound
                Debug.Log("Armour Full");
            } else {
                // play Healing sound
                _playerState.AddArmour(_replenishValue);
                _inventory.AddCoin(-_shopPrice);
                Debug.Log(_inventory.GetCoins());

                if (_playerState.GetArmour() > _playerState.GetMaxArmour())
                    _playerState.SetArmour(_playerState.GetMaxArmour());
            }
        }
        else {
            Debug.Log("Not enough coins to buy");
        }
    }

}
