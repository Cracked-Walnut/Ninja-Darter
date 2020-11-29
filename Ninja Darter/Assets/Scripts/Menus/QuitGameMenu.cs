using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameMenu : MonoBehaviour {

    void Update() {
        if (Input.GetButtonDown("XboxA")) {
            Application.Quit();
            Debug.Log("Quit");
        }
    }

}
