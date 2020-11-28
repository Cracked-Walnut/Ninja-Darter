using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuPanel : MonoBehaviour {

    [SerializeField] private GameObject _button;

    void OnEnable() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_button);
    }

    void OnDisable() {

    }

}
