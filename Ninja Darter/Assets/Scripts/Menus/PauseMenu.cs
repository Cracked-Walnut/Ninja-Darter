using UnityEngine;

public class PauseMenu : MonoBehaviour {

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _statusMenu;

    
    void Update() => CheckPauseKey();
    
    // check for Pause key or start button
    void CheckPauseKey() {
        if (Input.GetKeyDown(KeyCode.Escape))
            SetPauseOn();
    }

    public void SetPauseOn() {
        _pauseMenu.SetActive(true);
        _optionsMenu.SetActive(false);
        _statusMenu.SetActive(false);
        if (Time.timeScale == 1.0f) Time.timeScale = 0.0f;
    }

    public void SetPauseOff() {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void SetOptionsOn() {
        _pauseMenu.SetActive(false);
        _optionsMenu.SetActive(true);
        _statusMenu.SetActive(false);
        if (Time.timeScale == 1.0f) Time.timeScale = 0.0f;
    }

    public void SetInventoryOn() {
        _pauseMenu.SetActive(false);
        _optionsMenu.SetActive(false);
        _statusMenu.SetActive(true);
        if (Time.timeScale == 1.0f) Time.timeScale = 0.0f;
    }

    public void QuitGame() => Application.Quit();
}