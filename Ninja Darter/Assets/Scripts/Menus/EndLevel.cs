using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {

    private SceneLoader _sceneLoader;

    void Awake() {
        _sceneLoader = GetComponent<SceneLoader>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == "Player") {
            _sceneLoader.LoadLevel(1); // Ruins Biome Scene
        }
    }

}
