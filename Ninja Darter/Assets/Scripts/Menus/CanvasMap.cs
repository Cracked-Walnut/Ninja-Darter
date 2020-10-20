using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMap : MonoBehaviour {

    [SerializeField] private GameObject _canvasMapCamera;
    
    void Start() {
        _canvasMapCamera.SetActive(false);
    }
    
    void Update() => CheckMapStatus();

    void CheckMapStatus() {
        if (Input.GetButtonDown("View (Back)"))
            OpenCanvasMap();

        if (OpenCanvasMap() && Input.GetButtonDown("XboxB"))
            CloseCanvasMap();
    }

    bool OpenCanvasMap() {
        _canvasMapCamera.SetActive(true);
        return true;
    }

    void CloseCanvasMap() => _canvasMapCamera.SetActive(false);
}
