using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    private Rigidbody2D _rigidBody2D;
    
    private State _state;
    private enum State { Idling, Running, Dashing, Crouching, Attacking, InAir, Ledge_Climb, Wall_Sliding, Wall_Climbing, Hurt, Dead }

    [SerializeField] CharacterController2D _characterController2D;
    [SerializeField] private int _health;

    [Header("Running")]
    [Range(0, 200)] [SerializeField] private float _runSpeed;
    private const float DEFAULT_RUN_SPEED = 40f; // modify as needed
    private float _horizontalMove; // will equal 1 if moving right, -1 if moving left. Multipled with _runSpeed

    [Header("Dashing")]
    [SerializeField] private bool _canDash = true;
    [Range(0, 200)] [SerializeField] private float _dashSpeed;
    [Range(0, 1)] [SerializeField] private float _dashTime;
    [Range(0, 1)] [SerializeField] private float _timeBtwDashes;

    [Header("Wall Logic")]
    [SerializeField] private Transform _wallCheckOrigin;
    [Range(0, 1.2f)] [SerializeField] private float _wallCheckLength;
    [Range(0, 10f)] [SerializeField] private float _wallSlidingSpeed;
    [SerializeField] private LayerMask _whatIsWall; // determined what we can wall slide off of

    // ensures we can't move during any potential cutscenes or other instances
    private bool _isJumping = false;
    private bool _isCrouching = false;
    private bool _canMove = true;
    private bool _isTouchingWall = false;
    private bool _isWallSliding;
    

    private void SetState(State _state) { this._state = _state; }
    private State GetState() { return _state; }

    void Start() {
        _state = State.Idling;
        _runSpeed = DEFAULT_RUN_SPEED;
    }

    void Awake() {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        // _wallGrabInfoRight = Physics2D.Raycast(_wallCheckOrigin.position, Vector2.right, _wallCheckLength);
        
    }

    void Update() {
        CheckCurrentState();
         _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;

        if (Input.GetButtonDown("Jump")) {
            _isJumping = true;
        }
    }

    void FixedUpdate() {
        _characterController2D.Move(_horizontalMove * Time.fixedDeltaTime, _isCrouching, _isJumping);  // fixedDeltaTime ensures we move the same amount no matter how many times Move() is called
        _isJumping = false;
        CheckCurrentFixedState();
    }

    private void CheckCurrentState() { /*Check non-physics related States*/
        Idling();
        Dashing();
        Wall_Sliding();
        Hurt();
        Dead();
        RunCodeBasedOnState();
        Debug.Log(_state);
    }

    private void CheckCurrentFixedState() { /*Check physics related States*/
        InAir();
        Crouching();
        Running();
        RunFixedCodeBasedOnState();
        Debug.Log(_state);
    }

    /*<-------------->-Run extra non-physics related code-<------------------------------->*/
    private void RunCodeBasedOnState() {
        switch(_state) {
            case State.Idling:
                break;
            case State.Wall_Sliding:
                break;
            case State.Dashing:
                DashAbility();
                break;
            case State.Hurt:
                break;
            case State.Dead:
                break;
            default:
                Debug.Log("NOT IN A STATE (Idling, Dashing, Hurt, Dead)");
                break;
        }
    }

    /*<-------------->-Run extra physics related code-<------------------------------->*/
    private void RunFixedCodeBasedOnState() {
        switch(_state) {
            case State.Running:
                break;
            case State.Crouching:
                break;
            case State.InAir:
                break;
            case State.Wall_Climbing:
                break;
            default:
                Debug.Log("FIXED NOT IN A STATE (Running, Crouching, InAir or Wall_Sliding)");
                break;
        }
    }

    /*<------------------------------->-State Functions-<------------------------------->*/
    /*<--------->-These functions hold the bare minimum to achieve the desired state-<--------->*/
    bool Idling() {
        SetState(State.Idling);
        return true;    
    }

    bool Running() {
        /*Play running anim*/
       if (_horizontalMove > 0.5f || _horizontalMove < -0.5f) {
            SetState(State.Running);
            return true;
        } else
            return false;
    }

    bool Dashing() {
        if (_canMove && _canDash && Running()) {
            if (Input.GetKeyDown(KeyCode.F)) {
                SetState(State.Dashing);
                return true;
            }
        }
        return false;
    }

    bool Crouching() {
        if (Input.GetButton("Crouch")) {
            SetState(State.Crouching);
            _isCrouching = true;
            return _isCrouching;
        } else  {
            _isCrouching = false;
            return _isCrouching;
        }
    }

    // bool Attacking() {}

    bool InAir() {
        if (!_characterController2D.getGrounded()) {
            SetState(State.InAir);
            return true;
        } else
            return false;
    }

    // bool Ledge_Climb() {}

    bool Wall_Sliding() {
        if (InAir()) {
            _isTouchingWall = Physics2D.OverlapCircle(_wallCheckOrigin.position, _wallCheckLength, _whatIsWall);

            if (_isTouchingWall && Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) {
                _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, Mathf.Clamp(_rigidBody2D.velocity.y, -_wallSlidingSpeed, float.MaxValue));
                SetState(State.Wall_Sliding);
                return true;
            }
        }
        return false;
    }

    // bool Wall_Climbing() {}

    bool Hurt() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            TakeDamage(20);
            SetState(State.Hurt);
            return true;
        } else
            return false;
    }

    bool Dead() {
        if (_health <= 0) {
            SetState(State.Dead);
            Time.timeScale = 0.0f;
            return true;
        } else
            return false;
    }
    /*<------------------------------->-End of State Functions-<------------------------------->*/
    
    
    void TakeDamage(int damage) {  _health -= damage; }

    private void DashAbility() {
        StartCoroutine(Dash());
    }

    IEnumerator Dash() {
        _canDash = false;
        _runSpeed = _dashSpeed;
        yield return new WaitForSeconds(_dashTime);
        _runSpeed = DEFAULT_RUN_SPEED;
        yield return new WaitForSeconds(_timeBtwDashes);
        _canDash = true;
    }

    private void WallClimb() {
        
     }

    private void ApplyForce(float x, float y) {
        _rigidBody2D.velocity = new Vector2(0, 0);
        _rigidBody2D.AddForce(new Vector2(x, y));
    }
}

/*
Sources:
1) B., Brackeys, '2D Movement in Unity, 2018. [Online]. Available: https://www.youtube.com/watch?v=dwcT-Dch0bA [Accessed: Aug-08-2020].
2) B.B., Bonk, 'Unity Ground Dash and Dash Jump Tutorial', 2019. [Online]. Available: https://www.youtube.com/watch?v=I4Ja5Ar63Pw [Accessed: Aug-09-2020].
*/
