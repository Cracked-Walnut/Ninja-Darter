using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : MonoBehaviour {

    [SerializeField] private bool _isShopItem; // you cannot collect an item if it's in a shop. You'll have to buy it
    [SerializeField] private int _replenishValue; // this is the value of armour that is restored upon contact/purchase
    [SerializeField] private GameObject _player;
    private PlayerState _playerState;
    private bool _isPickedUp;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject == _player) {

            if (_isShopItem)
                return;

            _playerState = _player.GetComponent<PlayerState>();

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

}
