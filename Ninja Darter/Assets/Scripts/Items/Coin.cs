using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField] private GameObject _player;
    [SerializeField] private int _coinValue;
    private Inventory _inventory;
    private Points _points;

    private void Awake() {
        _inventory = _player.GetComponent<Inventory>();
        _points = _player.GetComponent<Points>();
    }

    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.name == "Player") {
            _points.AddPoints(_coinValue * 2);
            _inventory.AddCoin(_coinValue);
            Destroy(gameObject);
        }
    }
}
