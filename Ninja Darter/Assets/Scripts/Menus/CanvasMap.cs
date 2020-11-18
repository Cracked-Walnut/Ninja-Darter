using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMap : MonoBehaviour {

    [SerializeField] private GameObject _canvasMapCamera;
    [SerializeField] private GameObject _ui;
    private bool _canOpenMap;

    public bool GetCanOpenMap() { return _canOpenMap; }
    public void SetCanOpenMap(bool _map) => _canOpenMap = _map;

    void Start() {
        _canvasMapCamera.SetActive(false);
        // _canvasMapCamera.SetActive(true);
        _canOpenMap = true;
    }
    
    void Update() => CheckMapStatus();

    // void OnDisable() => _ui.SetActive(true);

    void CheckMapStatus() {
        if (Input.GetButton("LB") && _canOpenMap)
            OpenCanvasMap();
        else
            CloseCanvasMap();
    }

    void OpenCanvasMap() => _canvasMapCamera.SetActive(true); 
        // _ui.SetActive(false);
    

    void CloseCanvasMap() => _canvasMapCamera.SetActive(false);
        // _ui.SetActive(true);
}
