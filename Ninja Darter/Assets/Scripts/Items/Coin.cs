using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField] private GameObject _player;
    [SerializeField] private int _coinValue;
    private Inventory _inventory;

    private void Awake() => _inventory = _player.GetComponent<Inventory>();

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == "Player") {
            _inventory.AddPoints(_coinValue * 2);
            _inventory.AddCoin(_coinValue);
            Destroy(gameObject);
        }
    }
}
