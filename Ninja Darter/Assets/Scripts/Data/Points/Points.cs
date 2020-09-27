using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour {

    [SerializeField] private int _totalPoints;
    [SerializeField] private int _stagePoints;
    [SerializeField] private int _totalBonusPoints;
    [SerializeField] private int _stageBonusPoints;

    private int AddPoints(int _points) => _stagePoints += _points;
    private int AddBonusPoints(int _bonusPoints) => _stageBonusPoints += _bonusPoints;

    private int TallyPoints() => _totalPoints += _stagePoints;
    private int TallyBonusPoints() => _totalBonusPoints += _stageBonusPoints;

}
