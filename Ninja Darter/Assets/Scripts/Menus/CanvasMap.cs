using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMap : MonoBehaviour {

    // private bool _canOpenCanvasMap = true;
    [SerializeField] private GameObject _canvasMapCamera;
    [SerializeField] private GameObject _minimapCamera;
    private bool _canOpenMap;

    public bool GetCanOpenMap() { return _canOpenMap; }
    public void SetCanOpenMap(bool _map) => _canOpenMap = _map;

    void Start() {
        _canvasMapCamera.SetActive(false);
        _minimapCamera.SetActive(true);
        _canOpenMap = true;
    }

    void Update() => CheckMapStatus();

    void CheckMapStatus() {
        if (Input.GetButton("LB") && _canOpenMap)
            OpenCanvasMap();
        else
            CloseCanvasMap();

        // if (Input.GetButtonDown("LB"))
        //     CloseCanvasMap();
    }

    void OpenCanvasMap() => _canvasMapCamera.SetActive(true);

    void CloseCanvasMap() => _canvasMapCamera.SetActive(false);
}
