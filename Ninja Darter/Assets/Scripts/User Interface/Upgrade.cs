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

    void Update() {
        if (Input.GetButtonDown("XboxB"))
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

    void InvestSP(string _category) {

        if (_xp.GetSkillPoints() > 0) {
            switch(_category) {
                case "HP":
                    _playerState.UpgradeMaxHealthBy(2);
                    break;
                case "Armour":
                    _playerState.UpgradeMaxArmourBy(1);
                    break;
                case "Attack":
                    _playerState.UpgradeMaxAttackDamageBy(1);
                    break;
            }
        } else { /* play error sound*/ }
            
    }
}
