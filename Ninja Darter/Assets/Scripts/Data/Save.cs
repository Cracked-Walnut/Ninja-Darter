using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save : MonoBehaviour  {

    private PlayerData _playerData;
    private PlayerState _playerState;
    private string _jsonSave, _jsonLoad;

    void Awake() => _playerData = new PlayerData();

    void SaveGame() {
        _playerData.SetPlayerHealth(_playerState.GetHealth());

        _jsonSave = JsonUtility.ToJson(_playerData);
        Debug.Log(_jsonSave);

        try {
            File.WriteAllText("D:\\Github-Repos\\Ninja-Darter\\Ninja Darter\\Assets" + "DummySaveFile", _jsonSave);
            Debug.Log("saved to file");
        } 
        catch (IOException e) {
            Debug.Log("Error occured while saving!");
        }
    }

    void LoadGame() {
        _jsonLoad = File.ReadAllText("D:\\Github-Repos\\Ninja-Darter\\Ninja Darter\\Assets" + "DummySaveFile");
        PlayerData _loadedPlayerData = JsonUtility.FromJson<PlayerData>(_jsonLoad);
        Debug.Log("Loaded Data: \n" + _loadedPlayerData.GetPlayerHealth());
    }

    private class PlayerData {
        private int _playerHealth;

        public int GetPlayerHealth() { return _playerHealth; }
        public void SetPlayerHealth(int _playerHealth) { this._playerHealth = _playerHealth; }

    }
}
