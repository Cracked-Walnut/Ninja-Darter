using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private PlayerState _playerState;
    private Inventory _inventory;
    private Animator _animator;
    private bool _canShoot = true;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private float _arrowShootDelay = 0.8f;
    [SerializeField] private Transform _spawnPointRight, _spawnPointLeft; // spawn point right is used when on the ground, and left is used when wall sliding

    void Awake() { 
        _playerState = FindObjectOfType<PlayerState>();
        _inventory = GetComponent<Inventory>();
        _animator = _player.GetComponent<Animator>();
    }

    public void CheckArrow() {

        if (Input.GetButtonDown("RB")) {

            if (_playerState._canDash && _playerState.Idling() && _inventory.GetArrows() > 0 && _canShoot) 
                _animator.SetTrigger("ArrowShoot");

            if (_playerState.Wall_Sliding() && _inventory.GetArrows() > 0) {
                Instantiate(_arrowPrefab, _spawnPointLeft.position, _spawnPointLeft.rotation);
                _inventory.SetArrows(_inventory.GetArrows() - 1);
            }
        }
    }

    // this function is called when "WristBladeThrow" trigger is called
    public void CueArrowDelay() => StartCoroutine(ArrowDelay());

    public void FireArrow() => Instantiate(_arrowPrefab, _spawnPointRight.position, _spawnPointRight.rotation);
    
    void DecrementArrows() => _inventory.SetArrows(_inventory.GetArrows() - 1);

    IEnumerator ArrowDelay() {
        _canShoot = false;
        DecrementArrows(); 
        // _inventory.SetArrows(_inventory.GetArrows() - 1);
        yield return new WaitForSeconds(_arrowShootDelay);
        _canShoot = true;
    }

    private void CheckArrowQuantity() {
        if (_inventory.GetArrows() > _inventory.GetMaxArrows())
            _inventory.SetArrows(_inventory.GetMaxArrows());
    }

    public void AddArrow(int _arrowToAdd) {
        _inventory.SetArrows(_inventory.GetArrows() + _arrowToAdd);
        CheckArrowQuantity();
    } 
}
