using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    [SerializeField] private GameObject[] _rooms;
    private GameObject _roomInstance;

    void Start() {
        if (_rooms == null || _rooms.Length == 0)
            return;
        else
            SpawnRoom();
    }
    
    private void SpawnRoom() {
        int _roomToSpawn = Random.Range(0, _rooms.Length);

        _roomInstance = (GameObject) Instantiate(_rooms[_roomToSpawn], transform.position, transform.rotation);
    }
}
