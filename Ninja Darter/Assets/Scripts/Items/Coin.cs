using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField] private GameObject _player;
    [SerializeField] private int _coinAmount;
    private Inventory _inventory;

    private void Awake() => _inventory = _player.GetComponent<Inventory>();

    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.name == "Player") {
            _inventory.AddCoin(_coinAmount);
            Destroy(gameObject);
        }
    }
}
