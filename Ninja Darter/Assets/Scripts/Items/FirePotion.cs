using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePotion : MonoBehaviour {

    [SerializeField] private bool _isShopItem; // you cannot collect an item if it's in a shop. You'll have to buy it
    [SerializeField] private int _replenishValue;
    [SerializeField] private int _shopPrice;
    private GameObject _player;
    private bool _isPickedUp;
    private WristBladeWeapon _wristBladeWeapon;
    private Inventory _inventory;

    public int GetShopPrice() { return _shopPrice; }
    public int GetReplenishValue() { return _replenishValue; }

    void Awake() { 
        _player = GameObject.FindWithTag("Player");
        _wristBladeWeapon = _player.GetComponent<WristBladeWeapon>();
        _inventory = _player.GetComponent<Inventory>();
    }

    void OnTriggerEnter2D(Collider2D _collider) {

            if (!_isPickedUp) {
                if (_inventory.GetFireBalls() == _wristBladeWeapon.GetMaxQuantity()) {
                    Debug.Log("FireBall is Full");
                    return;
                }
                else {

                    if (_inventory.GetFireBalls() < _wristBladeWeapon.GetMaxQuantity())
                        _inventory.AddFireBalls(_replenishValue);
                    
                    if (_inventory.GetFireBalls() > _wristBladeWeapon.GetMaxQuantity())
                        _inventory.SetFireBalls(_wristBladeWeapon.GetMaxQuantity());

                    _isPickedUp = true;
                    Destroy(gameObject);
                }
            }
    }

    // this function will be executed when the player presses the HP button in shop
    public void BuyFPInShop() {
        _inventory = _player.GetComponent<Inventory>();

        if (_inventory.GetCoins() >= _shopPrice) {
            if (_inventory.GetFireBalls() == _wristBladeWeapon.GetMaxQuantity()) {
                // play error sound
                Debug.Log("FB Full");
            } else {
                // play Healing sound
                _inventory.AddFireBalls(_replenishValue);
                _inventory.AddCoin(-_shopPrice);
                Debug.Log(_inventory.GetCoins());

                if (_inventory.GetFireBalls() > _wristBladeWeapon.GetMaxQuantity())
                    _inventory.SetFireBalls(_wristBladeWeapon.GetMaxQuantity());
            }
        }
        else {
            Debug.Log("Not enough coins to buy");
        }
    }
}
