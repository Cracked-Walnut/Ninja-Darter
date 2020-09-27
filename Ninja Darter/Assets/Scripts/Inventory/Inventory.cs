using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    [SerializeField] private int _coins;
    [SerializeField] private int _wristBlades;
    [SerializeField] private int _cubeFragments;

    public int GetWristBlades() { return _wristBlades; }
    public void SetWristBlades(int _knives) => _wristBlades = _knives;

    public int GetCubeFragments() { return _cubeFragments; }
    
    public int AddFragment(int _num) => _cubeFragments += _num;
    
    public void AddCoin(int _coin) { 
        _coins += _coin; 
        Debug.Log(_coins);    
    }

    public int GetCoins() { return _coins; }
    public void SetCoins(int _thisCoin) => _thisCoin = _coins;
    
}
