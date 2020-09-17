using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Sources:
1) B., Brackeys, '2D Shooting in Unity (Tutorial)', 2018. [Online]. Available: https://www.youtube.com/watch?v=wkKsl1Mfp5M [Accessed: Aug-16-2020].
*/

public class WristBladeWeapon : MonoBehaviour {

    private PlayerState _playerState;
    private CharacterController2D _characterController2D;
    private Inventory _inventory;
    [SerializeField] private GameObject _wristBladePrefab;
    [SerializeField] private int _maxQuantity = 20;
    [SerializeField] private Transform _spawnPointRight, _spawnPointLeft; // spawn point right is used when on the ground, and left is used when wall sliding

    void Update () => CheckShuriken();

    void Awake() { 
        _playerState = FindObjectOfType<PlayerState>();
        _characterController2D = FindObjectOfType<CharacterController2D>();
        _inventory = GetComponent<Inventory>();
    }

    public void CheckShuriken() {

        if (Input.GetButtonDown("XboxY") && _playerState._canDash) {

            if (_playerState.Idling() || _playerState.Running()) {
                if (_inventory.GetWristBlades() > 0) {

                    Instantiate(_wristBladePrefab, _spawnPointRight.position, _spawnPointRight.rotation);
                    _inventory.SetWristBlades(_inventory.GetWristBlades() - 1);
                    Debug.Log(_inventory.GetWristBlades());
                    return;
                }
            }
            
            if (_playerState.Wall_Sliding() && _characterController2D.getFacingRight() && _inventory.GetWristBlades() > 0) {
                
                Instantiate(_wristBladePrefab, _spawnPointLeft.position, _spawnPointLeft.rotation);
                _inventory.SetWristBlades(_inventory.GetWristBlades() - 1);
               
            } else if (_playerState.Wall_Sliding() && !_characterController2D.getFacingRight() && _inventory.GetWristBlades() > 0) {
                Instantiate(_wristBladePrefab, _spawnPointLeft.position, _spawnPointLeft.rotation);
                _inventory.SetWristBlades(_inventory.GetWristBlades() - 1);
            }
        }
    }
    
    private void CheckKnifeQuantity() {
        if (_inventory.GetWristBlades() > _maxQuantity)
            _inventory.SetWristBlades(_maxQuantity);
    }

    public void AddKnife(int _knifeToAdd) { 
        _inventory.SetWristBlades(_inventory.GetWristBlades() + _knifeToAdd);
        CheckKnifeQuantity();
    } 

}
