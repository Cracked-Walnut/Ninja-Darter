using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMap : MonoBehaviour {

    // private bool _canOpenCanvasMap = true;
    [SerializeField] private GameObject _canvasMapCamera;
    [SerializeField] private GameObject _minimapCamera;

    void Start() {
        _canvasMapCamera.SetActive(false);
        _minimapCamera.SetActive(true);
    }
    
    void Update() => CheckMapStatus();

    void CheckMapStatus() {
        if (Input.GetButtonDown("View (Back)")) {
            OpenCanvasMap();
            CloseMinimap();
        }

        if (Input.GetButtonDown("XboxB")) {
            OpenMinimap();
            CloseCanvasMap();
        }
    }

    void OpenCanvasMap() => _canvasMapCamera.SetActive(true);
    void OpenMinimap() => _minimapCamera.SetActive(true);

    void CloseCanvasMap() => _canvasMapCamera.SetActive(false);
    void CloseMinimap() => _minimapCamera.SetActive(false);
}
