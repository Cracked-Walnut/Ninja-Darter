using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [SerializeField] private float _stageTimeSeconds;
    private int _stageTimeMinutes;
    [SerializeField] private float _bestStageTime;
    [SerializeField] private float _totalGameTime;
    [SerializeField] private bool _isTimerRunning;

    void Start() {
        _stageTimeSeconds = 0.0f;
        _isTimerRunning = true;
    }

    // this will be called at the end of each stage
    public void CheckBestTime() {
        
        _isTimerRunning = false;

        if (_stageTimeSeconds < _bestStageTime) {
            _bestStageTime = _stageTimeSeconds;
            Debug.Log("New Best Time: " + _bestStageTime);
        }
        else
            Debug.Log("No New Best Time");
    }


    public void AddToTotalTime() { 
        _isTimerRunning = false;
        _totalGameTime += _stageTimeSeconds;
        Debug.Log("New Total Time: " + _totalGameTime); 
    }
    
    public void StartTimer() => _stageTimeSeconds += Time.deltaTime;

    public bool GetTimerRunning() { return _isTimerRunning; }
    public void SetTimerRunning(bool _timer) => _isTimerRunning = _timer;

    public float GetStageTimeSeconds() { return _stageTimeSeconds; }
    public void SetStageTimeSeconds(float _sTime) => _stageTimeSeconds = _sTime;

    public float GetStageTimeMinutes() { return _stageTimeMinutes; }
    public void AddStageTimeMinutes(int _mTime) => _stageTimeMinutes += _mTime;

}
