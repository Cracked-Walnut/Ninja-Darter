using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour {

    [SerializeField] private int _totalPoints;
    [SerializeField] private int _stagePoints;
    [SerializeField] private int _totalBonusPoints;
    [SerializeField] private int _stageBonusPoints;

    public int AddPoints(int _points) => _stagePoints += _points;
    public int AddBonusPoints(int _bonusPoints) => _stageBonusPoints += _bonusPoints;

    // tallied at the end of each level
    public int TallyPoints() => _totalPoints += _stagePoints;
    public int TallyBonusPoints() => _totalBonusPoints += _stageBonusPoints;

}
