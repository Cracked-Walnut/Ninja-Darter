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

    void HandleMenu(bool _isPauseMenuOn, bool _isOptionsMenuOn, bool _isStatusMenuOn, bool _isUIOn, bool _isMinimapOn) {
        
        _pauseMenu.SetActive(_isPauseMenuOn);
        _optionsMenu.SetActive(_isOptionsMenuOn);
        _statusMenu.SetActive(_isStatusMenuOn);
        _ui.SetActive(_isUIOn);
        _theMinimap.SetActive(_isMinimapOn);

        if (Time.timeScale == 1.0f)
            Time.timeScale = 0.0f;
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