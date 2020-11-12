using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour {

    [SerializeField] private GameObject _xp;
    private XPBar _xpBar;

    [Header("Points")]
    [SerializeField] private int _currentPoints; // the amount of points in the player's current level
    private int _tempPoints;
    [SerializeField] private int _stagePoints; // the amount of points earned in a stage
    [SerializeField] private int _totalPoints; // the amount of points earned across all stages

    private int _skillPoints = 0;
    
    [Header("Levelling")]
    [SerializeField] public int _currentLevel = 0;
    [SerializeField] private const int MAX_POINTS_AMOUNT = 100;

    void Awake() => _xpBar = _xp.GetComponent<XPBar>();

    void Update() {
        if (_currentPoints >= MAX_POINTS_AMOUNT) {
            _tempPoints = _currentPoints - MAX_POINTS_AMOUNT; // trim the excess XP and store it in a variable
            LevelUp();
            _currentPoints = _tempPoints; // the trimmed XP, after levelling up, is now the current xp
            _tempPoints = 0; // reset this. I don't want its value messing anything up
            _xpBar.SetXP(_currentPoints);
        }
    }

    void LevelUp() {
            _currentLevel++;
            _skillPoints++;
    }

    // tallied at the end of each level
    public int TallyPoints() => _totalPoints += _stagePoints;

    public int GetPoints() { return _stagePoints; }
    public int GetSkillPoints() { return _skillPoints; }
    public void DecrementSkillPoints(int _sp) => _skillPoints += _sp;

    public void AddPoints(int _points) { 
        _currentPoints += _points;
        _xpBar.SetXP(_currentPoints);
    }
    public int AddTotalPoints(int _totalPoints) => _stagePoints += _totalPoints;

}