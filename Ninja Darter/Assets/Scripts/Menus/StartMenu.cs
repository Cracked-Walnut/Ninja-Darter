using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {

    private SceneLoader _sceneLoader;

    void Awake() => _sceneLoader = GetComponent<SceneLoader>();

    void Update() {
        if (Input.GetButtonDown("Menu (Start)"))
            StartCoroutine(StartGame());
    }

    IEnumerator StartGame() {
        FindObjectOfType<AudioManager>().Play("Start-Button-Sound");
        yield return new WaitForSeconds(0.75f);
        _sceneLoader.LoadLevel(1);
    }
}
