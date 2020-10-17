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
    private Animator _animator;
    private bool _canThrow = true;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _wristBladePrefab;
    [SerializeField] private int _maxQuantity = 20;
    [SerializeField] private float _knifeThrowDelay = 0.8f;
    [SerializeField] private Transform _spawnPointRight, _spawnPointLeft; // spawn point right is used when on the ground, and left is used when wall sliding

    void Update () => CheckShuriken();

    void Awake() { 
        _playerState = FindObjectOfType<PlayerState>();
        _characterController2D = FindObjectOfType<CharacterController2D>();
        _inventory = GetComponent<Inventory>();
        _animator = _player.GetComponent<Animator>();
    }

    public void CheckShuriken() {

        if (Input.GetButtonDown("XboxY")) {

            if (_playerState._canDash && _playerState.Idling() && _inventory.GetWristBlades() > 0 && _canThrow) {
        
                _animator.SetTrigger("WristBladeThrow");
                // Instantiate(_wristBladePrefab, _spawnPointRight.position, _spawnPointRight.rotation);
                // _inventory.SetWristBlades(_inventory.GetWristBlades() - 1);
            }
            if (_playerState.Wall_Sliding() && _inventory.GetWristBlades() > 0) {
                Instantiate(_wristBladePrefab, _spawnPointLeft.position, _spawnPointLeft.rotation);
                _inventory.SetWristBlades(_inventory.GetWristBlades() - 1);
            }
        }
    }

    // this function is called when "WristBladeThrow" trigger is called
    public void CueBladeDelay() => StartCoroutine(BladeDelay());

    public void FireBlade() => Instantiate(_wristBladePrefab, _spawnPointRight.position, _spawnPointRight.rotation);
    
    void DecrementBlade() => _inventory.SetWristBlades(_inventory.GetWristBlades() - 1);

    IEnumerator BladeDelay() {
        _canThrow = false;
        DecrementBlade(); 
        // _inventory.SetWristBlades(_inventory.GetWristBlades() - 1);
        yield return new WaitForSeconds(_knifeThrowDelay);
        _canThrow = true;
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
