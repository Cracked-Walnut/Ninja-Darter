using UnityEngine;

public class MenuTransition : MonoBehaviour {

    [SerializeField] private GameObject _mainMenuObject;
    [SerializeField] private GameObject _optionsObject;
    [SerializeField] private GameObject _loadObject;
    [SerializeField] private GameObject _newGameObject;

    public void NewGameStart() {
        // fade out slowly, then disable all objects
        // load the next scene that starts the game
    }

    public void SetMainMenuActive () {
        _mainMenuObject.SetActive(true);
        _optionsObject.SetActive(false);
        _loadObject.SetActive(false);
        _newGameObject.SetActive(false);
    }

    public void SetMainOptionsActive () {
        _mainMenuObject.SetActive(false);
        _optionsObject.SetActive(true);
        _loadObject.SetActive(false);
        _newGameObject.SetActive(false);
    }

    public void SetMainLoadActive () {
        _mainMenuObject.SetActive(false);
        _optionsObject.SetActive(false);
        _loadObject.SetActive(true);
        _newGameObject.SetActive(false);
    }

    public void SetNewGameActive() {
        _mainMenuObject.SetActive(false);
        _optionsObject.SetActive(false);
        _loadObject.SetActive(false);
        _newGameObject.SetActive(true);
    }

    public void QuitGame() => Application.Quit();
    
}
