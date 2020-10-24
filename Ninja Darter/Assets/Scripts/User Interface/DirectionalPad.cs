using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalPad : MonoBehaviour {

    [SerializeField] private GameObject _up, _down, _left, _right;
    [SerializeField] private GameObject _bowAndArrow, _fists, sword;
    
    [SerializeField] public bool _bowEquipped;
    [SerializeField] public bool _swordEquipped;
    [SerializeField] public bool _fistsEquipped;

    void Start() {
        _bowEquipped = false;
        _swordEquipped = true;
        _fistsEquipped = false;
    }

    void Update() => CheckDPadInput();

    void Awake() {

    }

    void CheckDPadInput() {
        switch(Input.GetAxis("DPADHorizontal")) {
            case -1f:
                EquipWeapon(false, false, true); // equip fists
                break;
            case 1f:
                EquipWeapon(false, true, false); // equip sword
                break;
        }

        switch(Input.GetAxis("DPADVertical")) {
            case 1f:
                EquipWeapon(true, false, false); // equip bow
                break;
        }
    }

    void EquipWeapon(bool _bow, bool _sword, bool _fists) {
        _bowEquipped = _bow;
        _swordEquipped = _sword;
        _fistsEquipped = _fists;

        CheckDPadArrows();
    }

    void CheckDPadArrows() { // up, left, right
        if (_bowEquipped)
            SetDPadArrows(true, false, false);

        else if (_swordEquipped)
            SetDPadArrows(false, false, true);

        else if (_fistsEquipped)
            SetDPadArrows(false, true, false);
    }

    void SetDPadArrows(bool _upArrow, bool _leftArrow, bool _rightArrow) {
        _up.SetActive(_upArrow);
        _left.SetActive(_leftArrow);
        _right.SetActive(_rightArrow);
    }


}
