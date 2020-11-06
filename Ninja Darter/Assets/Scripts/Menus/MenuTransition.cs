using UnityEngine;

public class MenuTransition : MonoBehaviour {

    // [SerializeField] private GameObject _mainMenuObject;
    // [SerializeField] private GameObject _optionsObject;
    // [SerializeField] private GameObject _loadObject;
    // [SerializeField] private GameObject _newGameObject;
    // [SerializeField] private GameObject _gameOverObject;

    [SerializeField] private GameObject[] _menuObjects;

    public void NewGameStart() {
        // fade out slowly, then disable all objects
        // load the next scene that starts the game
    }

    public void SetMainMenuActive() {
        _menuObjects[0].SetActive(true);
        _menuObjects[1].SetActive(false);
        _menuObjects[2].SetActive(false);
        _menuObjects[3].SetActive(false);
        _menuObjects[4].SetActive(false);
    }

    public void SetMainOptionsActive() {
        _menuObjects[0].SetActive(false);
        _menuObjects[1].SetActive(true);
        _menuObjects[2].SetActive(false);
        _menuObjects[3].SetActive(false);
        _menuObjects[4].SetActive(false);
    }

    public void SetControlsActive() {
        _menuObjects[0].SetActive(false);
        _menuObjects[1].SetActive(false);
        _menuObjects[2].SetActive(false);
        _menuObjects[3].SetActive(false);
        _menuObjects[4].SetActive(true);
    }

    public void SetMainLoadActive() {
        _menuObjects[0].SetActive(false);
        _menuObjects[1].SetActive(false);
        _menuObjects[2].SetActive(true);
        _menuObjects[3].SetActive(false);
        _menuObjects[4].SetActive(false);
    }

    public void SetNewGameActive() {
        _menuObjects[0].SetActive(false);
        _menuObjects[1].SetActive(false);
        _menuObjects[2].SetActive(false);
        _menuObjects[3].SetActive(true);
        _menuObjects[4].SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void SetGameOverActive() {
        _menuObjects[0].SetActive(false);
        _menuObjects[1].SetActive(false);
        _menuObjects[2].SetActive(false);
        _menuObjects[3].SetActive(false);
        _menuObjects[4].SetActive(false);
    }

    public void QuitGame() => Application.Quit();
    
}
