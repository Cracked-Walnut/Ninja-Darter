using UnityEngine;

/*This script is used to navigate the menus when the player is in-game*/
public class PauseMenu : MonoBehaviour {

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _statusMenu;
    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _theMinimap;

    
    void Update() => CheckPauseKey();
    
    // check for Pause key or start button
    void CheckPauseKey() {
        // if (Input.GetKeyDown(KeyCode.Escape))
        //     SetPauseOn();
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
        _pauseMenu.SetActive(true);
        _optionsMenu.SetActive(false);
        _statusMenu.SetActive(false);
        _ui.SetActive(false);
        _theMinimap.SetActive(false);
        if (Time.timeScale == 1.0f) Time.timeScale = 0.0f;
    }

    public void SetPauseOff() {
        _pauseMenu.SetActive(false);
        _ui.SetActive(true);
        _theMinimap.SetActive(true);
        if (Time.timeScale == 0.0f) Time.timeScale = 1.0f;
    }

    public void SetOptionsOn() {
        _pauseMenu.SetActive(false);
        _optionsMenu.SetActive(true);
        _statusMenu.SetActive(false);
        _ui.SetActive(false);
        _theMinimap.SetActive(false);
        if (Time.timeScale == 1.0f) Time.timeScale = 0.0f;
    }

    public void SetInventoryOn() {
        _pauseMenu.SetActive(false);
        _optionsMenu.SetActive(false);
        _statusMenu.SetActive(true);
        _ui.SetActive(false);
        _theMinimap.SetActive(false);
        if (Time.timeScale == 1.0f) Time.timeScale = 0.0f;
    }

    public void QuitGame() => Application.Quit();
}