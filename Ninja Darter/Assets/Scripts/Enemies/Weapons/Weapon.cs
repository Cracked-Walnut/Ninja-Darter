using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectilePosition;


    void FireBullet() => Instantiate(_projectilePrefab, _projectilePosition.position, Quaternion.identity); 

}
