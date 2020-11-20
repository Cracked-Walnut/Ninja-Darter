using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndLevelMenu : MonoBehaviour {

    [SerializeField] private GameObject _noButton;

    void OnEnable() {
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_noButton);
    }

    void OnDisable() => Time.timeScale = 1f;

}
