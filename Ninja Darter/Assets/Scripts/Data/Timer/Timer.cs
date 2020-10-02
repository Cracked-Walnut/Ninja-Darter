using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [SerializeField] private float _stageTime;
    [SerializeField] private float _bestStageTime;
    [SerializeField] private float _totalGameTime;
    [SerializeField] private bool _isTimerRunning;

    void Start() {
        _stageTime = 0.0f;
        _isTimerRunning = true;
    }

    // this will be called at the end of each stage
    public void CheckBestTime() {
        
        _isTimerRunning = false;

        if (_stageTime < _bestStageTime) {
            _bestStageTime = _stageTime;
            Debug.Log("New Best Time: " + _bestStageTime);
        }
        else
            Debug.Log("No New Best Time");
    }


    public void AddToTotalTime() { 
        _isTimerRunning = false;
        _totalGameTime += _stageTime;
        Debug.Log("New Total Time: " + _totalGameTime); 
    }
    
    public void StartTimer() => _stageTime += Time.deltaTime;

    public bool GetTimerRunning() { return _isTimerRunning; }
    public void SetTimerRunning(bool _timer) => _isTimerRunning = _timer;

    public float GetStageTime() { return _stageTime; }

}
