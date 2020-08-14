using UnityEngine;

public class PauseMenu : MonoBehaviour {

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _optionsMenu;

    
    void Update() => CheckPauseKey();
    
    // check for Pause key or start button
    void CheckPauseKey() {
        if (Input.GetKeyDown(KeyCode.Escape))
            SetPauseOn();
    }

    public void SetPauseOn() {
        _pauseMenu.SetActive(true);
        _optionsMenu.SetActive(false);
        if (Time.timeScale == 1.0f) Time.timeScale = 0.0f;
    }

    public void SetPauseOff() {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OpenOptionsMenu() {
        _pauseMenu.SetActive(false);
        _optionsMenu.SetActive(true);
    }

    public void QuitGame() => Application.Quit();
}