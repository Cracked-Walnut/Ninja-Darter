using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Sources:
1) B., Brackeys, 'Smooth Camera Follow in Unity - Tutorial', 2017. [Online]. Available: https://www.youtube.com/watch?v=MFQhpwc6cKE [Accessed: Sep-18-2020].
*/

public class CameraFollow : MonoBehaviour {

    // Change the camera's coordinates to the player's every frame
    [SerializeField] private Transform _objectToFollow;
    [SerializeField] private float _lookSensitivityX;
    [SerializeField] private float _lookSensitivityY;
    [SerializeField] private float _lookUpAxis;
    [SerializeField] private float _lookDownAxis;


    void FixedUpdate() {
        if (Input.GetAxis("R-Stick-Horizontal") != 0.0 || Input.GetAxis("R-Stick-Vertical") != 0.0) {

            transform.position = new Vector3 (
                Input.GetAxis("R-Stick-Horizontal") * _lookSensitivityX + _objectToFollow.transform.position.x,
                Input.GetAxis("R-Stick-Vertical") * _lookSensitivityY + _objectToFollow.transform.position.y,
                -10f
            );
        } else
            transform.position = new Vector3(_objectToFollow.transform.position.x, _objectToFollow.transform.position.y, -10f);
    }
}
