using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    [SerializeField] private int _coins;
    [SerializeField] private int _wristBlades;
    [SerializeField] private int _cubeFragments;

    public int GetWristBlades() { return _wristBlades; }
    public void SetWristBlades(int _knives) => _wristBlades = _knives;

}
