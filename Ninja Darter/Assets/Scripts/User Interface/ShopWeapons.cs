using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopWeapons : MonoBehaviour {

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _healingItemsPage;
    private PlayerState _playerState;

    void Awake() => _playerState = _player.GetComponent<PlayerState>();

    void Update() {
        if (Input.GetButtonDown("XboxX"))
            gameObject.SetActive(false);
                    
        else if (Input.GetButtonDown("XboxY"))
            SwitchToHealingItemsPage();
    }

    void OnEnable() {
        _playerState.EnablePlayer(false);
        _ui.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_button);
    }

    void OnDisable() {
        _playerState.EnablePlayer(true);
        _ui.SetActive(true);
    }

    void SwitchToHealingItemsPage() {
        gameObject.SetActive(false);
        _healingItemsPage.SetActive(true);
    }

}
