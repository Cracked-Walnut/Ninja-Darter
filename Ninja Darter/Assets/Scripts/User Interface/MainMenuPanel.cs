using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuPanel : MonoBehaviour {

    [SerializeField] private GameObject _button;

    void OnEnable() {
        Time.timeScale = 1.0f;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_button);
    }

    void OnDisable() {

    }

}
