using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Upgrade : MonoBehaviour {

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _hpButton;
    [SerializeField] private GameObject _ui;
    private PlayerState _playerState;
    private XP _xp;

    void Awake() { 
        _playerState = _player.GetComponent<PlayerState>();
        _xp = _player.GetComponent<XP>();
    }

    void Start() { 
        EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(_hpButton);
        _playerState.EnablePlayer(false);
        _ui.SetActive(false);

    }
    
    void Update() {
        if (Input.GetButtonDown("XboxB"))
            gameObject.SetActive(false);
    }

    void InvestSP(string _category) {

        if (_xp.GetSkillPoints() > 0) {
            switch(_category) {
                case "HP":
                    break;
                case "Armour":
                    break;
                case "Attack":
                    break;
            }
        } else { /* play error sound*/ }
            
    }

    void OnDisable() => _playerState.EnablePlayer(true);
}
