using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradePanel : MonoBehaviour {

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _hpButton;
    [SerializeField] private GameObject _ui;
    private PlayerState _playerState;

    void Awake() => _playerState = _player.GetComponent<PlayerState>();

    void Update() {
        if (Input.GetButtonDown("XboxX"))
            gameObject.SetActive(false);
    }

    void OnEnable() {
        _playerState.EnablePlayer(false);
        _ui.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_hpButton);
    }

    void OnDisable() {
        _playerState.EnablePlayer(true);
        _ui.SetActive(true);
    }

    
}
