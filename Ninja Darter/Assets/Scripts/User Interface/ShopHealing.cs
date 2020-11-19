using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopHealing : MonoBehaviour {

    private GameObject _player;
    [SerializeField] private GameObject _hpButton;
    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _weaponsPage;
    private PlayerState _playerState;

    void Awake() { 
        _player = GameObject.FindWithTag("Player");
        _playerState = _player.GetComponent<PlayerState>();
    }

    void Update() {
        if (Input.GetButtonDown("XboxX"))
            gameObject.SetActive(false);
            
        else if (Input.GetButtonDown("XboxY"))
            SwitchToWeaponsPage();

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

    void SwitchToWeaponsPage() {
        gameObject.SetActive(false);
        _weaponsPage.SetActive(true);
    }

}
