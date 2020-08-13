using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/*
Sources:
1) B., Brackeys, 'SETTINGS MENU in Unity', 2017. [Online]. Available: https://www.youtube.com/watch?v=YOaYQrN1oYQ [Accessed: Aug-13-2020].
*/

public class SettingsMenu : MonoBehaviour {

    public AudioMixer _audioMixer;
    public Dropdown _resolutionDropdown;
    private Resolution[] _resolutions;

    void Start() {
        _resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();

        List<string> _resOptions = new List<string>();
        int _currentResIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++) {
            string _option = _resolutions[i].width + "x" + _resolutions[i].height;
            _resOptions.Add(_option);

            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
                _currentResIndex = i;
        }
        _resolutionDropdown.AddOptions(_resOptions);
        _resolutionDropdown.value = _currentResIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume) => _audioMixer.SetFloat("_masterVolume", volume);

    public void SetQuality(int _qualityIndex) => QualitySettings.SetQualityLevel(_qualityIndex);

    public void SetFullScreen(bool _isFullScreen) => Screen.fullScreen = _isFullScreen;

    public void SetResolution(int _resolutionIndex) {
        Resolution _resolution = _resolutions[_resolutionIndex];
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
    }

}
