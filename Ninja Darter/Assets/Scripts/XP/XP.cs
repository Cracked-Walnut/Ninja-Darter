using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour {

    [SerializeField] private GameObject _xp;
    private XPBar _xpBar;

    [Header("Points")]
    [SerializeField] private int _currentPoints; // the amount of points in the player's current level
    [SerializeField] private int _stagePoints; // the amount of points earned in a stage
    [SerializeField] private int _totalPoints; // the amount of points earned across all stages

    [Header("Upgrades")]
    [SerializeField] private int _skillPoints = 0;
    
    [Header("Levelling")]
    [SerializeField] private int _currentLevel = 0;
    [SerializeField] private const int MAX_POINTS_AMOUNT = 100;

    void Awake() => _xpBar = _xp.GetComponent<XPBar>();

    void LevelUp() {
        if (_currentPoints >= MAX_POINTS_AMOUNT) {
            _currentPoints = 0;
            _xpBar.SetXP(0);    
            _currentLevel++;
            _skillPoints++;
        }
    }

    // tallied at the end of each level
    public int TallyPoints() => _totalPoints += _stagePoints;

    public int GetPoints() { return _stagePoints; }

    public void AddPoints(int _points) { 
        _currentPoints += _points;
        _xpBar.SetXP(_currentPoints);
    }
    public int AddTotalPoints(int _totalPoints) => _stagePoints += _totalPoints;

}