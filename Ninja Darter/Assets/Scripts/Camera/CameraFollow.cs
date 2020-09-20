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
    [SerializeField] private int[] _boundaries;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Vector3 _offset;

    void Update() => FollowPlayer();

    void FixedUpdate() {
        Vector3 _desiredPosition = _objectToFollow.position + _offset;
        Vector3 _smoothedPosition = Vector3.Lerp(transform.position, _desiredPosition, _smoothSpeed);
        transform.position = _smoothedPosition;
    }

    void FollowPlayer() {
        if (_objectToFollow.position.x > _boundaries[0])
            transform.position = new Vector3(_boundaries[0], _objectToFollow.transform.position.y, -10f);
        
        else if (_objectToFollow.position.x < _boundaries[1])
            transform.position = new Vector3(_boundaries[1], _objectToFollow.transform.position.y, -10f);
    }
}
