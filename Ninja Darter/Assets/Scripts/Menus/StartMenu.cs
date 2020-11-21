using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {

    private SceneLoader _sceneLoader;

    void Awake() => _sceneLoader = GetComponent<SceneLoader>();

    void Update() {
        if (Input.GetButtonDown("Menu (Start)"))
            _sceneLoader.LoadLevel(1);
    }
}
