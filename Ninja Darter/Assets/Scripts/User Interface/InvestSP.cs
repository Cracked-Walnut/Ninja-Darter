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
                
                case "Sword&MMA":
                    _xp.DecrementSkillPoints(-1);
                    _playerState.UpgradeMaxSwordAttackDamageBy(1);
                    _playerState.IncrementSwordAttackLevel(1);
                    
                    _xp.DecrementSkillPoints(-1);
                    _playerState.UpgradeMaxFistsAttackDamageBy(1);
                    _playerState.IncrementFistsAttackLevel(1);
                    break;
                
                case "Bow&FireBall":
                    _xp.DecrementSkillPoints(-1);
                    _playerState.UpgradeMaxBowAttackDamageBy(1);
                    _playerState.IncrementBowAttackLevel(1);

                    _xp.DecrementSkillPoints(-1);
                    _playerState.UpgradeMaxFireBallAttackDamageBy(1);
                    _playerState.IncrementFireBallAttackLevel(1);
                    break;
            }
        } else { /* play error sound*/ }
    }
}
