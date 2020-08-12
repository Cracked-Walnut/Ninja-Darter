using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField] private GameObject[] _objects;
    private GameObject _instance;
    private int _rand;

    void Awake() {
        _rand = Random.Range(0, _objects.Length); }

    private void Start() {
        if (_objects == null || _objects.Length == 0)
            return;
        else
            SpawnObject();
        }

    void SpawnObject() {
        int _rand = Random.Range(0, _objects.Length);
        _instance = (GameObject) Instantiate(_objects[_rand], transform.position, Quaternion.identity);
        _instance.transform.parent = transform;
    }

    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.name == "Player") {
            // add item to inventory
            // play sound and /or particle system
            // destroy this game object
            
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void OnDisable() {
        Debug.Log("Disabled");
    }
}

/*
Sources:
1) B., Blackthornprod, 'ROGUE LIKE RANDOM LEVEL GENERATION - INTERMEDIATE C#/UNITY TUTORIAL - #1', 2018. [Online]. Available: https://www.youtube.com/watch?v=hk6cUanSfXQ
*/