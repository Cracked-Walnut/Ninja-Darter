using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Sources:
    1) B., Blackthornprod, 'ONE WAY COLLISION PLATFORMS - EASY UNITY TUTORIAL', 2018. [Online]. Available: https://www.youtube.com/watch?v=M_kg7yjuhNg [Accessed: Nov-21-2020].
*/

public class HorizontalPlatform : MonoBehaviour {

    private GameObject _foregroundEffector2DLayer;
    private PlatformEffector2D _platformEffector2D;
    private PlayerState _playerState;
    private Rigidbody2D _rigidbody2D;

    void Awake() { 

        _foregroundEffector2DLayer = GameObject.FindWithTag("ForegroundEffector2D");
        _platformEffector2D = _foregroundEffector2DLayer.GetComponent<PlatformEffector2D>();
    
        _playerState = GetComponent<PlayerState>();
        _rigidbody2D = _playerState.GetComponent<Rigidbody2D>();
    }

    void Update() {

        if (_playerState.Crouching() && (Input.GetButton("XboxA")))
            _platformEffector2D.rotationalOffset = 180f;
        else
            _platformEffector2D.rotationalOffset = 0f;
            
        if (!_playerState.Crouching() && Input.GetButtonUp("XboxA") && _rigidbody2D.velocity.y > 0.5f)
            _platformEffector2D.rotationalOffset = 0f;
    }

}
