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
    [SerializeField] private int _fireBall;

    [Header("Kills")]
    [SerializeField] private int _enemyKills;
    [SerializeField] private int _totalEnemyKills;


    // dummy variable to get the checkmark showing in the inspector
    void Start() { int z = 0; }

    public int GetFireBalls() { return _fireBall; }
    public void SetFireBalls(int _fb) => _fireBall = _fb;
    public void AddFireBalls(int _fb) => _fireBall += _fb;

    public int GetArrows() { return _arrows; }
    public void SetArrows(int _arrow) => _arrows = _arrow;
    public void AddArrow(int _a) => _arrows += _a;

    public int GetMaxArrows() { return _maxArrows; }
    public void SetMaxArrows(int _maxArrow) => _maxArrows = _maxArrow;

    public int GetEmeralds() { return _emeralds; }
    public int AddEmerald(int _num) => _emeralds += _num;
    
    public void AddCoin(int _coin) {
        this._coins += _coin;
        Debug.Log(this._coins);
        // if (_coins < 0) _coins = 0;
    }
    public int GetCoins() { return this._coins; }
    public void SetCoins(int _thisCoin) => _thisCoin = _coins;

    public int AddKill(int _kill) => _enemyKills += _kill;

    public int TallyTotalKills() => _totalEnemyKills += _enemyKills;
    
}
