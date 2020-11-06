using UnityEngine;
using UnityEngine.EventSystems;

/*This script is used to navigate the menus when the player is in-game*/
public class PauseMenu : MonoBehaviour {

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _statusMenu;
    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _theMinimap;

    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private GameObject _statusBackButton;
    [SerializeField] private GameObject _optionsBackButton;
    [SerializeField] private GameObject _player;
    private PlayerState _playerState;
    
    void Update() => CheckPauseKey();

    void Awake() => _playerState = _player.GetComponent<PlayerState>();
    
    void CheckPauseKey() {
        if (Input.GetButtonDown("Menu (Start)") || Input.GetKeyDown(KeyCode.Escape))
            SetPauseOn();
    }

    public void SetPauseOn() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_resumeButton);
        _pauseMenu.SetActive(true);
        _optionsMenu.SetActive(false);
        _statusMenu.SetActive(false);
        _ui.SetActive(false);
        _theMinimap.SetActive(false);
    }

    // the functions below are called during button presses
    // I've tried using one function instead of four. I would need five boolean parameters but Unity's inspector only allows for one boolean maximum

    public void SetPauseOff() {
        _pauseMenu.SetActive(false);
        _ui.SetActive(true);
        _theMinimap.SetActive(true);
    }

    public void SetOptionsOn() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_optionsBackButton);

        _pauseMenu.SetActive(false);
        _optionsMenu.SetActive(true);
        _statusMenu.SetActive(false);
        _ui.SetActive(false);
        _theMinimap.SetActive(false);
    }

    public void SetInventoryOn() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_statusBackButton);
        
        _pauseMenu.SetActive(false);
        _optionsMenu.SetActive(false);
        _statusMenu.SetActive(true);
        _ui.SetActive(false);
        _theMinimap.SetActive(false);
    }

    public void QuitGame() => Application.Quit();
}