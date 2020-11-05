using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowItem : MonoBehaviour {

    [SerializeField] private bool _isShopItem; // you cannot collect an item if it's in a shop. You'll have to buy it
    [SerializeField] private int _replenishValue; // this is the value of the arrow that is restored upon contact/purchase
    [SerializeField] private GameObject _player;
    private Inventory _inventory;
    private bool _isPickedUp;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject == _player) {

            if (_isShopItem)
                return;

            _inventory = _player.GetComponent<Inventory>();

            if (!_isPickedUp) {
                if (_inventory.GetArrows() == _inventory.GetMaxArrows()) {
                    Debug.Log("No room for more Arrows");
                    return;
                }
                else {

                    if (_inventory.GetArrows() < _inventory.GetMaxArrows()) {
                        _inventory.AddArrow(_replenishValue);
                        _isPickedUp = true;
                        Destroy(gameObject);
                    }

                    if (_inventory.GetArrows() > _inventory.GetMaxArrows()) {
                        _inventory.SetArrows(_inventory.GetMaxArrows());
                        _isPickedUp = true;
                        Destroy(gameObject);
                    }
                }

            }

        }
    }
}
