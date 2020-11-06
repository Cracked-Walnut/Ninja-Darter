using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script is used to manage the Player State: Dead*/
public class GameOverMenu : MonoBehaviour {
    
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _player;
    private PlayerState _playerState;

    void Update() => CheckGameOver();

    void Start() => _gameOverMenu.SetActive(false);
    void Awake() => _playerState = _player.GetComponent<PlayerState>();

    public bool CheckGameOver() {

        if (_playerState.Dead()) {
            Time.timeScale = 0.0f;
            _ui.SetActive(false);
            _gameOverMenu.SetActive(true);

            return true;
        } else
            return false;
    }
}
