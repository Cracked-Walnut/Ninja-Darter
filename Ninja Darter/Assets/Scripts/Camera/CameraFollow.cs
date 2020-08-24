using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    // Change the camera's coordinates to the player's every frame
    [SerializeField] private Transform _objectToFollow;
    [SerializeField] private int[] _boundaries;

    void Update() => FollowPlayer();

    void FollowPlayer() {
        if (_objectToFollow.position.x > _boundaries[0])
            transform.position = new Vector3(_boundaries[0], _objectToFollow.transform.position.y, -10f);
        
        else if (_objectToFollow.position.x < _boundaries[1])
            transform.position = new Vector3(_boundaries[1], _objectToFollow.transform.position.y, -10f);
        
        else
            transform.position = new Vector3(_objectToFollow.transform.position.x, _objectToFollow.transform.position.y, -10f);
    }
}
