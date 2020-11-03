using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestSP : MonoBehaviour {

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _upgradePanelObject;
    private PlayerState _playerState;
    private UpgradePanel _upgradePanel;
    private XP _xp;

    void Awake() {
        _playerState = _player.GetComponent<PlayerState>();
        _upgradePanel = _upgradePanelObject.GetComponent<UpgradePanel>();
        _xp = _player.GetComponent<XP>();
    }

    public void AllocateSP(string _category) {

        if (_xp.GetSkillPoints() > 0) {
            switch(_category) {
                case "HP":
                    _xp.DecrementSkillPoints(-1);
                    _playerState.UpgradeMaxHealthBy(2);
                    _playerState.IncrementHPLevel(1);
                    break;
                case "Armour":
                    _xp.DecrementSkillPoints(-1);
                    _playerState.UpgradeMaxArmourBy(1);
                    _playerState.IncrementArmourLevel(1);
                    break;
                case "Attack":
                    _xp.DecrementSkillPoints(-1);
                    _playerState.UpgradeMaxAttackDamageBy(1);
                    _playerState.IncrementAttackLevel(1);
                    break;
            }
        } else { /* play error sound*/ }
            
    }

}
