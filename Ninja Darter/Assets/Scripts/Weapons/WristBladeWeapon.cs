using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Sources:
1) B., Brackeys, '2D Shooting in Unity (Tutorial)', 2018. [Online]. Available: https://www.youtube.com/watch?v=wkKsl1Mfp5M [Accessed: Aug-16-2020].
*/

public class WristBladeWeapon : MonoBehaviour {

    [SerializeField] private GameObject _wristBladePrefab;
    [SerializeField] private int _quantity = 5;
    [SerializeField] private int _maxQuantity = 20;
    [SerializeField] private Transform _spawnPoint;

    void Update () {
        ThrowShuriken();
        CheckKnifeQuantity(); 
    } 

    public void ThrowShuriken() {
        if (Input.GetButtonDown("XboxY")) {
            if (_quantity > 0) {
            Instantiate(_wristBladePrefab, _spawnPoint.position, _spawnPoint.rotation);
            _quantity--;
            }
        }
    }

    private void CheckKnifeQuantity() {
        if (_quantity > _maxQuantity)
            _quantity = _maxQuantity; 
    }

    public void AddKnife(int _knifeToAdd) => _quantity += _knifeToAdd; 

}
