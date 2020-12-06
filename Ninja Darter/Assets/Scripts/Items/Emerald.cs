using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emerald : MonoBehaviour {

    [SerializeField] private int _emeraldValue;
    [SerializeField] private GameObject _player;
    private Inventory _inventory;

    private void Awake() { 
        // _player = GameObject.FindWithTag("Player");
        _inventory = _player.GetComponent<Inventory>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        _inventory.AddEmerald(_emeraldValue);
        Destroy(gameObject);
    }
}
