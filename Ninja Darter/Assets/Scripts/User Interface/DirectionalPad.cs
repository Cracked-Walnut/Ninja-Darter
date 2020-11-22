using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionalPad : MonoBehaviour {

    [SerializeField] private GameObject _up, _down, _left, _right;
    [SerializeField] private GameObject _bowAndArrow, _fists, _sword;
    private Image _bowImage, _fistsImage, _swordImage;
    
    [SerializeField] public bool _bowEquipped;
    [SerializeField] public bool _swordEquipped;
    [SerializeField] public bool _fistsEquipped;

    void Start() {
        _bowEquipped = false;
        _swordEquipped = true;
        _fistsEquipped = false;

        _bowImage.color = new Color(1, 1f, 1f, 0.3f);
        _fistsImage.color = new Color(1, 1f, 1f, 0.3f);
        _swordImage.color = new Color(1, 1f, 1f, 1f);

    }

    void Update() => CheckDPadInput();

    void Awake() {
        _bowImage = _bowAndArrow.GetComponent<Image>();
        _fistsImage = _fists.GetComponent<Image>();
        _swordImage = _sword.GetComponent<Image>();
    }

    void CheckDPadInput() {
        switch(Input.GetAxis("DPADHorizontal")) {
            case -1f:

                // fade out other weapons to signify equipped weapon
                _bowImage.color = new Color(1, 1f, 1f, 0.3f);
                _fistsImage.color = new Color(1, 1f, 1f, 1f);
                _swordImage.color = new Color(1, 1f, 1f, 0.3f);
                
                EquipWeapon(false, false, true); // equip fists
                break;

            case 1f:

                // fade out other weapons to signify equipped weapon
                _bowImage.color = new Color(1, 1f, 1f, 0.3f);
                _fistsImage.color = new Color(1, 1f, 1f, 0.3f);
                _swordImage.color = new Color(1, 1f, 1f, 1f);

                EquipWeapon(false, true, false); // equip sword
                break;
        }

        switch(Input.GetAxis("DPADVertical")) {
            case 1f:

                // fade out other weapons to signify equipped weapon
                _bowImage.color = new Color(1, 1f, 1f, 1f);
                _fistsImage.color = new Color(1, 1f, 1f, 0.3f);
                _swordImage.color = new Color(1, 1f, 1f, 0.3f);

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
