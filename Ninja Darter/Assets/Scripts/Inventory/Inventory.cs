using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script will hold all player related information, actions and items.

public class Inventory : MonoBehaviour {

    [Header("Items")]
    [SerializeField] private int _coins;
    [SerializeField] private int _arrows;
    [SerializeField] private int _maxArrows;
    [SerializeField] private int _emeralds;
    [SerializeField] private int _wristBlades;

    [Header("Kills")]
    [SerializeField] private int _enemyKills;
    [SerializeField] private int _totalEnemyKills;

    public int GetWristBlades() { return _wristBlades; }
    public void SetWristBlades(int _knives) => _wristBlades = _knives;

    public int GetArrows() { return _arrows; }
    public void SetArrows(int _arrow) => _arrows = _arrow;
    public void AddArrow(int _a) => _arrows += _a;

    public int GetMaxArrows() { return _maxArrows; }
    public void SetMaxArrows(int _maxArrow) => _maxArrows = _maxArrow;

    public int GetEmeralds() { return _emeralds; }
    public int AddEmerald(int _num) => _emeralds += _num;
    
    public void AddCoin(int _coin) {
        _coins += _coin;
        if (_coins < 0) _coin = 0;
    }
    public int GetCoins() { return _coins; }
    public void SetCoins(int _thisCoin) => _thisCoin = _coins;

    public int AddKill(int _kill) => _enemyKills += _kill;

    public int TallyTotalKills() => _totalEnemyKills += _enemyKills;
    
}
