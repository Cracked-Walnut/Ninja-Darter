using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script will hold all player related information, actions and items.

public class Inventory : MonoBehaviour {

    [SerializeField] private int _coins;
    [SerializeField] private int _wristBlades;
    [SerializeField] private int _cubeFragments;

    [SerializeField] private int _totalPoints;
    [SerializeField] private int _stagePoints;
    [SerializeField] private int _totalBonusPoints;
    [SerializeField] private int _stageBonusPoints;

    [SerializeField] private int _enemyKills;
    [SerializeField] private int _totalEnemyKills;

    public int GetWristBlades() { return _wristBlades; }
    public void SetWristBlades(int _knives) => _wristBlades = _knives;

    public int GetCubeFragments() { return _cubeFragments; }
    public int AddFragment(int _num) => _cubeFragments += _num;
    
    public void AddCoin(int _coin) => _coins += _coin;
    public int GetCoins() { return _coins; }
    public void SetCoins(int _thisCoin) => _thisCoin = _coins;

    public int GetPoints() { return _stagePoints; }

    public int AddPoints(int _points) => _stagePoints += _points;
    public int AddBonusPoints(int _bonusPoints) => _stageBonusPoints += _bonusPoints;

    public int AddKill(int _kill) => _enemyKills += _kill;
    
    // tallied at the end of each level
    public int TallyPoints() => _totalPoints += _stagePoints;
    public int TallyBonusPoints() => _totalBonusPoints += _stageBonusPoints;

    public int TallyTotalKills() => _totalEnemyKills += _enemyKills;
    
}
