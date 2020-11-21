using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Sources:
    1) B., Brackeys, '2D Movement in Unity', 2018. [Online]. Available: https://www.youtube.com/watch?v=dwcT-Dch0bA [Accessed: Aug-08-2020].
    2) B.B., Bonk, 'Unity Ground Dash and Dash Jump Tutorial', 2019. [Online]. Available: https://www.youtube.com/watch?v=I4Ja5Ar63Pw [Accessed: Aug-09-2020].
    3) B., Blackthornprod, 'How to make a 2D Wall Jump & Wall Slide using Unity & C#!', 2020. [Online]. Available: https://www.youtube.com/watch?v=KCzEnKLaaPc [Accessed: Aug-10-2020].
    4) G., gamesplusjames, 'Wall Jumping in Unity Tutorial', 2020. [Online]. Available:  https://www.youtube.com/watch?v=uNJanDrjMgU [Accessed: Sep-06-2020].
    5) B., Bardent, 'Basic Combat - 2D Platformer Player Controller - Part 9 [Unity 2019.2.0f1]', 2020. [Online]. Available: https://www.youtube.com/watch?v=YaXcwc5Evjk [Accessed: Sep-07-2020].
    6) B., Brackeys, 'MELEE COMBAT in Unity', 2019. [Online]. Available: https://www.youtube.com/watch?v=sPiVz1k-fEs [Accessed: Sep-11-2020].
    7) S.P.G., Stuart's Pixel Games, 'How To Change Sprites Colour Or Transparency – Unity C#', 2019. [Online]. Available: https://stuartspixelgames.com/2019/02/19/how-to-change-sprites-colour-or-transparency-unity-c/ [Accessed: Oct-22-2020].
*/

/*A class used to control the various states the player can enter*/

public class PlayerState : MonoBehaviour {

    private Rigidbody2D _rigidBody2D;
    
    public State _state;
    public enum State { Idling, Running, Crouching, InAir, Wall_Sliding, Wall_Climbing, Wall_Jumping, On_Wall, Dashing, Blocking, Hurt, Dead }

    [SerializeField] private CharacterController2D _characterController2D; // reference to the script that gives our player movement
    private Inventory _inventory;
    private XP _xp;
    private CanvasMap _canvasMap;

    private int _hpLevel;
    private int _armourLevel;
    private int _swordAttackLevel;
    private int _bowAttackLevel;
    private int _fistsAttackLevel;
    private int _fireBallAttackLevel;

    private int _health = 10;
    private int _maxHealth = 10;

    [Header("Weapons")]
    [SerializeField] private Arrow _arrow;

    [Header("Armour")]
    private int _armour = 6;
    private int _maxArmour = 6;
    private bool _isArmourDepleted = false;

    [Header("Invincibility")]
    private bool _isInvincible;
    private SpriteRenderer _spriteRenderer;
    private float _invincibilityTime = 0.5f;

    [Header("Upgrades Screen")]
    [SerializeField] private GameObject _upgradesScreen;

    [Header("Weapon Switching")]
    [SerializeField] private DirectionalPad _directionalPad;

    [Header("Animator")]
    [SerializeField] private Animator _animator;

    [Header("Attacking")]
    private int _swordAttackDamage = 5;
    private int _bowAttackDamage = 5;
    private int _fistsAttackDamage = 3;
    private int _fireBallAttackDamage = 3;

    private bool _canAttack = true;
    private bool _canAirAttack = false;
    private bool _canCrouch = true;
    private bool _airAttacked = false;

    private float _swordSwingDelay = 0.25f; // a small delay between each attack for the sword
    private float _unarmedSwingDelay = 0.25f; // a small delay between each attack for the punches/kicks
    private float _timeBtwAirAttacks = 0.5f;
    private float _attackRange = 2f; // a circle used to detect enemies
    
    [SerializeField] private Transform _attackPoint; // the point at which the circle is drawn
    [SerializeField] private LayerMask _enemyLayers; // enemies we can hit within that circle
    [SerializeField] private LayerMask _projectileLayers; // projectiles we can hit within that circle
    
    private string[] _swordGroundAttacks = {"GroundAttack1", "GroundAttack2", "GroundAttack3"};
    private string[] _unarmedGroundAttacks = {"Fists1", "Fists2", "Fists3", "Kick1", "Kick2"};
    private string[] _airAttacks = {"AirAttack1"};

    [Header("Jumping")]
    private float _fallMultiplier = 7.0f; // The gravity used to bring the player down after a long jump (long jump button press)
    private float _lowMultiplier = 5.0f; // the gravuty used to bring the player down after a short jump (short jump button press)
    private bool _doubleJump;
    private bool _canJump;

    [Header("CheckForInteraction")]
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionRadius; // drawn at _interactionPoint, which is positioned at the center of the player
    [SerializeField] private LayerMask _whatIsInteractable; // determines what we are interacting with, in this case, treasure chests only

    [Header("Running")]
    [SerializeField] private float _runSpeed;
    private const float DEFAULT_RUN_SPEED = 50f; // modify as needed

    private float _horizontalMove; // keyboard movement

    // controller movement
    private float _horizontalXboxMove;

    [Header("Dashing")]
    public bool _canDash = true;
    private float _dashSpeed = 140f;
    private float _dashTime = 0.25f; // the time you remain in a dash
    private float _timeBtwDashes = 0.35f; // You cannot dash while this is ticking down

    [Header("Wall Logic")]
    [Range(0.05f, 1.2f)] [SerializeField] private float _wallCheckRadius; // The radius of the circle that detects walls
    [SerializeField] private Transform _wallCheckOriginTop, _wallCheckOriginBottom; // we draw a circle here to check for walls
    [Range(0, 10f)] [SerializeField] private float _wallSlideSpeed;
    [SerializeField] private LayerMask _whatIsWall; // determines what we can wall slide off
   
    [Header("Misc")]
    [SerializeField] private bool _canMove = true; // ensures we can't move during any potential cutscenes or other instances
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _endLevelMenu;
    private CameraShake _cameraShake;
    private bool _isJumping = false;
    private bool _isCrouching = false;
    public bool _isTouchingWallTop = false, _isTouchingWallBottom = false;
    private bool _isWallSliding;

    /*--------------------------------------------Getters/ Setters---------------------------------------------------------------------------------*/
    
    public void SetState(State _state) => this._state = _state;
    public State GetState () { return _state; }

    public int GetHealth() { return _health; }
    public void SetHealth(int _hp) => _health = _hp;
    public void AddHealth(int _hp) => _health += _hp;
    
    public int GetMaxHealth() { return _maxHealth; }
    public void UpgradeMaxHealthBy(int _mhp) => _maxHealth += _mhp;

    public void SetRunSpeed(float _r) => _runSpeed = _r;

    public int GetArmour() { return _armour; }
    public int GetMaxArmour() { return _maxArmour; }
    public void SetArmour(int _a) => _armour = _a;
    public void AddArmour(int _a) => _armour += _a;
    public void UpgradeMaxArmourBy(int _ma) => _maxArmour += _ma;

    public int GetMaxSwordAttackDamage() { return _swordAttackDamage; }
    public void SetMaxSwordAttackDamage(int _attack) => _swordAttackDamage = _attack;
    public void UpgradeMaxSwordAttackDamageBy(int _a) => _swordAttackDamage += _a;

    public int GetMaxBowAttackDamage() { return _bowAttackDamage; }
    public void SetMaxBowAttackDamage(int _attack) => _bowAttackDamage = _attack;
    public void UpgradeMaxBowAttackDamageBy(int _a) => _bowAttackDamage += _a;

    public int GetMaxFistsAttackDamage() { return _fistsAttackDamage; }
    public void SetMaxFistsAttackDamage(int _attack) => _fistsAttackDamage = _attack;
    public void UpgradeMaxFistsAttackDamageBy(int _a) => _fistsAttackDamage += _a;

    public int GetHPLevel() { return _hpLevel; }
    public int IncrementHPLevel(int _hp) => _hpLevel += _hp;

    public int GetArmourLevel() { return _armourLevel; }
    public int IncrementArmourLevel(int _ar) => _armourLevel += _ar;
    
    public int GetSwordAttackLevel() { return _swordAttackLevel; }
    public int IncrementSwordAttackLevel(int _sat) => _swordAttackLevel += _sat;

    public int GetBowAttackLevel() { return _bowAttackLevel; }
    public int IncrementBowAttackLevel(int _bat) => _bowAttackLevel += _bat;

    public int GetMaxFireBallAttackDamage() { return _fireBallAttackDamage; }
    public int GetFireBallAttackLevel() { return _fireBallAttackLevel; }
    public void UpgradeMaxFireBallAttackDamageBy(int _fb) => _fireBallAttackDamage += _fb;
    public int IncrementFireBallAttackLevel(int _fbat) => _fireBallAttackLevel += _fbat;
    
    public int GetFistsAttackLevel() { return _fistsAttackLevel; }
    public int IncrementFistsAttackLevel(int _fat) => _fistsAttackLevel += _fat;

    public bool GetInvincibility() { return _isInvincible; }
    public void SetInvincibility(bool _invincibile) => _isInvincible = _invincibile;

    public bool GetCanJump() { return _canJump; }
    public void SetCanJump(bool _jump) => _canJump = _jump;

    public bool SetCanJumpFalse() { return _canJump = false; }
    public bool SetCanJumpTrue() { return _canJump = true; }

    /*-----------------------------------------------------------------------------------------------------------------------------*/

    // used to disable the player when control is meant to be taken away (during pause menu, shop or upgrade menu)
    public void EnablePlayer(bool _isPlayerEnabled) {
        _isInvincible = _isPlayerEnabled;
        _canAttack = _isPlayerEnabled;
        _canAirAttack = _isPlayerEnabled;
        _canJump = _isPlayerEnabled;
        _doubleJump = _isPlayerEnabled;
        _canDash = _isPlayerEnabled;
        _canMove = _isPlayerEnabled;
        _canCrouch = _isPlayerEnabled;
        _canvasMap.SetCanOpenMap(_isPlayerEnabled);
    }

    void Start() {
        _doubleJump = true;
        _canJump = true;
        _runSpeed = 50;
        _isInvincible = false;
        _hpLevel = 1;
        _armourLevel = 1;
        _swordAttackLevel = 1;
        _bowAttackLevel = 1;
        _fistsAttackLevel = 1;
        _fireBallAttackLevel = 1;
    }

    void Awake() { 
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _inventory = GetComponent<Inventory>();
        _xp = GetComponent<XP>();
        _canvasMap = GetComponent<CanvasMap>();
        _cameraShake = _mainCamera.GetComponent<CameraShake>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update() {

        // if (_health < _health * 0.3)
        //     StartCoroutine(LowHealth());

        if (!_characterController2D.GetGrounded() && !_airAttacked)
            _canAirAttack = true;
        else
            _canAirAttack = false;

        CheckNonStateFunctions();
        CheckCurrentState();
        CheckForInteraction();

        if (_canMove) {
            // _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;
            _horizontalXboxMove = Input.GetAxisRaw("L-Stick-Horizontal") * _runSpeed;
        }

        if (_canJump) {

            if (Input.GetButtonDown("XboxA"))
                _isJumping = true;
        }
        
        // this is used to control how high the player can jump based off how long they hold the jump button
        if (_rigidBody2D.velocity.y < 0)
            _rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
        
        if (_rigidBody2D.velocity.y > 0 && !Input.GetButton("XboxA"))
            _rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (_lowMultiplier - 1) * Time.deltaTime;
    }

    void FixedUpdate() {

        // fixedDeltaTime ensures we move the same amount no matter how many times Move() is called
        // _characterController2D.Move(_horizontalMove * Time.fixedDeltaTime, _isCrouching, _isJumping);
        _characterController2D.Move(_horizontalXboxMove * Time.fixedDeltaTime, _isCrouching, _isJumping);
        
        _isJumping = false;
        CheckCurrentFixedState();
    }

    private void CheckNonStateFunctions() {
        DoubleJump();
        ExecuteGroundAttack();
        ExecuteAirAttack();
        OpenUpgradeScreen();
        // AirborneGroundAttack();
    }

    private void CheckCurrentState() { /*Check non-physics related States*/
        // Blocking();
        Idling();
        Crouching();
        DashAbility();
        OnWall();
        Dead();
        RunCodeBasedOnState();
    }

    private void CheckCurrentFixedState() { /*Check physics related States*/
        InAir();
        Running();
        RunFixedCodeBasedOnState();
    }

    /*<-------------->-Run extra non-physics related code-<------------------------------->*/

    // this will run the appropriate animation for the current state the player is in,
    // as well as other things when needed
    private void RunCodeBasedOnState() {
        switch(_state) {
            case State.Idling:
                _animator.SetTrigger("Idling");
                _runSpeed = 50;
                break;
            case State.Crouching:
                _animator.SetTrigger("Crouching");
                break;
            case State.Wall_Sliding:
                _animator.SetTrigger("WallSliding");
                break;
            case State.Wall_Jumping:
                _animator.SetTrigger("WallJumping");
                break;
            case State.On_Wall:
                break;
            case State.Hurt:
                // _animator.SetTrigger("Hurt");
                // _animator.SetBool("IsHurt", true);
                break;
            case State.Dead:
                break;
            default:
                break;
        }
    }

    /*<-------------->-Run extra physics related code-<------------------------------->*/
    private void RunFixedCodeBasedOnState() {
        switch(_state) {
            case State.Running:
                _animator.SetTrigger("Running");
                break;
            case State.InAir:
                _animator.SetTrigger("Jumping");
                break;
            default:
                break;
        }
    }

    /*<------------------------------->-State Functions-<------------------------------->*/
    /*<--------->-These functions hold the bare minimum to achieve the desired state-<--------->*/
    public bool Idling() {
        if (_characterController2D.GetGrounded() && _horizontalXboxMove < 0.5f && _horizontalXboxMove > -0.5f) {
            _animator.SetFloat("VelocityY", 0f); // I need to explicitly reset the Y Velocity as Unity doesn't seem to do it
            SetState(State.Idling);
            return true;
        } else
            return false;
    }

    public bool Running() {
        
       if (_characterController2D.GetGrounded() && !Idling() && !Crouching()) {
            _animator.SetFloat("VelocityY", 0f); // I need to explicitly reset the Y Velocity as Unity doesn't seem to do it
            SetState(State.Running);
            return true;
        } else
            return false;
    }

    bool Crouching() {
        if (Input.GetAxis("L-Stick-Vertical") > 0.75 && _characterController2D.GetGrounded() && _canCrouch) {
            
            SetState(State.Crouching);
            _isCrouching = true;
            _animator.SetBool("IsCrouching", _isCrouching); // an extra condition for crouching
            return _isCrouching;
            
        } else {
            _isCrouching = false;
            _animator.SetBool("IsCrouching", _isCrouching);
            return _isCrouching;
        }
    }

    public bool Wall_Sliding() {
        if (GetState() == State.Wall_Sliding)
            return true;
        else
            return false;
    }

    public bool InAir() {
        if (!_characterController2D.GetGrounded() && !_isTouchingWallTop && !_isTouchingWallBottom) {
            
            _animator.SetFloat("VelocityY", _rigidBody2D.velocity.y);
            SetState(State.InAir);
            
            return true;
        } else
            return false;
    }

    void OnWall() {

        if (!_characterController2D.GetGrounded()) {

            // used to detect walls
            _isTouchingWallTop = Physics2D.OverlapCircle(_wallCheckOriginTop.position, _wallCheckRadius, _whatIsWall);
            _isTouchingWallBottom = Physics2D.OverlapCircle(_wallCheckOriginBottom.position, _wallCheckRadius, _whatIsWall);

            if (_isTouchingWallTop || _isTouchingWallBottom) {
                _doubleJump = false; // if you've double jumped already before touching a wall, you'll be able to double jump again after touching a wall

                if (Input.GetButtonDown("XboxA"))
                    WallJump(0, 20); // this will make the character jump up the wall they're touching, and only up
                else {
                    if (_rigidBody2D.velocity.y < 10) { // I use the value 10 to control which animation plays when wall sliding. The higher the value, the sooner the wall sliding animation plays
                        _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, Mathf.Clamp(_rigidBody2D.velocity.y, -_wallSlideSpeed, float.MaxValue));
                        _animator.SetFloat("VelocityY", _wallSlideSpeed);
                        SetState(State.Wall_Sliding);
                    }
                }
            }
        }
    }
    
    public void TakeDamage(int _damage, float _knockBackX, float _knockBackY) {

        /*First I check if the player isn't invincible (if they weren't hit). 
            Invincibility is used to run a post-hit invincibility mechanic to prevent the player from taking too much damage is a short time
            Search for "PostHitInvincibility()" to examine the function*/
        if (!_isInvincible) {
            if (_isArmourDepleted) {

                _health -= _damage;
                _xp.AddPoints(-5);
            } else
                _armour -= _damage;
        
        if (_armour <= 0) {
            if (_armour < 0) // make sure armour value isn't negative
                _armour = 0;
            _isArmourDepleted = true;
        } else
            _isArmourDepleted = false;
        
        // the player is knocked back depending on the direction they are facing
        // if (_characterController2D.GetFacingRight())
        //     ApplyForce(-_knockBackX, _knockBackY);
        // else
        //     ApplyForce(_knockBackY, _knockBackY);
        }
        
        StartCoroutine(PostHitInvincibility());
    }

    IEnumerator PostHitInvincibility() {
        
        _spriteRenderer.color = new Color(1, 0f, 0f, 1f);
        yield return new WaitForSeconds(0.05f);
        
        _isInvincible = true;

        _spriteRenderer.color = new Color(1, 1, 1, 0.5f); // I fade out the player's sprite by a little to indicate the PH Invincibility (Alpha 50%)
        // _canMove = false;
        // SetState(State.Hurt);
        // ResetVelocity();
        yield return new WaitForSeconds(_invincibilityTime); // after this time is up, the player is no longer invincibile. This value is 0.5
        // _animator.SetBool("IsHurt", false);
        // _canMove = true;
        _isInvincible = false;
        _spriteRenderer.color = new Color(1, 1, 1, 1); // Alpha is back to 100% to indicate normal, undamaged state
    }

    // IEnumerator LowHealth() {
    //     _spriteRenderer.color = new Color(1, 0f, 0f, 1f);
    //     yield return new WaitForSeconds(0.5f);
    //     _spriteRenderer.color = new Color(1, 1f, 1f, 1f);
    //     yield return new WaitForSeconds(0.5f);
    // }

    public bool Dead() {
        if (_health <= 0) {
            if (_health < 0)
                _health = 0;
            SetState(State.Dead);
            return true;
        } else
            return false;
    }
    /*<------------------------------->-End of State Functions-<------------------------------->*/

    void OpenUpgradeScreen() {
        if (Input.GetButtonDown("View (Back)") && Idling())
            _upgradesScreen.SetActive(true);
    }

    // NOT being used at the moment. This is meant to be an airborne strike to the ground, dealing massive damage
    void AirborneGroundAttack() {
        if (InAir() && Input.GetAxis("L-Stick-Vertical") > 0.75 && Input.GetButtonDown("XboxA")) {
            ApplyForce(0, -1000);
        }
    }

    void DoubleJump() {
        if (!_doubleJump && InAir() && Input.GetButtonDown("XboxA")) {
            ApplyForce(0, _characterController2D.GetJumpForce());
            _doubleJump = true;
        }

        // the player can now double jump since they've touched the ground
        if (_characterController2D.GetGrounded())
            _doubleJump = false;
    }

    void ExecuteGroundAttack() {
        // After some simple checks to make sure the player is on the ground when attacking...
        if (Input.GetButtonDown("XboxX") && _characterController2D.GetGrounded() && _canAttack && !Crouching()) {

            // ... check which weapon they have equipped, and then display the correct animation
            if (_directionalPad._swordEquipped)
                StartCoroutine(SwordGroundAttack());
            
            else if (_directionalPad._bowEquipped)
                _arrow.CheckArrowGroundAttack();

            else if (_directionalPad._fistsEquipped)
                StartCoroutine(UnarmedGroundAttack());
        }
    }

    IEnumerator UnarmedGroundAttack() {
        int _randomGroundAttack = Random.Range(0, _unarmedGroundAttacks.Length);
        _canAttack = false;
        _animator.SetTrigger(_unarmedGroundAttacks[_randomGroundAttack]);
        yield return new WaitForSeconds(_unarmedSwingDelay);
        _canAttack = true;
    }

    IEnumerator SwordGroundAttack() {
        int _randomGroundAttack = Random.Range(0, _swordGroundAttacks.Length);
        _canAttack = false;
        _animator.SetTrigger(_swordGroundAttacks[_randomGroundAttack]);
        yield return new WaitForSeconds(_swordSwingDelay);
        _canAttack = true;
    }

    void ExecuteAirAttack() {
        if (Input.GetButtonDown("XboxX") && !_characterController2D.GetGrounded() && _canAirAttack) {

            if (_directionalPad._swordEquipped)
                StartCoroutine(SwordAirAttack());
            
            else if (_directionalPad._bowEquipped)
                _arrow.CheckArrowAirAttack();

            else if (_directionalPad._fistsEquipped)
                UnarmedGroundAttack();
        }

    }

    IEnumerator SwordAirAttack() {
        int _randomAirAttack = Random.Range(0, _airAttacks.Length);
        _canAirAttack = false;
        _airAttacked = true;
        _animator.SetBool("IsAirAttacking", true);
        _animator.SetTrigger(_airAttacks[_randomAirAttack]);
        
        yield return new WaitForSeconds(_timeBtwAirAttacks);
        
        _canAirAttack = true;
        _airAttacked = false;
        _animator.SetBool("IsAirAttacking", false);
    }

    public void ScanForEnemies() { // this function is executed during sword, bow and martial arts attack animations
        Collider2D[] _hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        foreach(Collider2D _enemiesHit in _hitEnemies) {
            StartCoroutine(_cameraShake.Shake(.1f, .05f));

            // bow is not included in this list as it is a projectile based weapon
            if (_directionalPad._swordEquipped) {
                _enemiesHit.GetComponent<Enemy>().TakeDamage(_swordAttackDamage); // damage the enemy
                //    playArrSound(swordDamageList); // play a damage sound
            }
            else if (_directionalPad._fistsEquipped) {
                _enemiesHit.GetComponent<Enemy>().TakeDamage(_fistsAttackDamage);
            }
          
        }
    }

    // if the player swings sword while a projectile is in the way, the projectile will be destroyed
    public void ScanForProjectiles() {
        Collider2D[] _hitProjectiles = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _projectileLayers);

        foreach(Collider2D _projectilesHit in _hitProjectiles) {
            StartCoroutine(_cameraShake.Shake(.1f, .1f));
            _projectilesHit.GetComponent<Projectile>().DestroyProjectile();
            //    playArrSound(swordDamageList); // play a damage sound
          }
    }

    // a quick contact damage function for the player
    void OnCollisionEnter2D(Collision2D _collisionInfo) {
        if (_collisionInfo.collider.name == "EarthWispProjectile(Clone)" || _collisionInfo.collider.name == "WindWispProjectile(Clone)" || _collisionInfo.collider.name == "AssaultDroidProjectile(Clone)")
            TakeDamage(2, 900, 600);
        else if (_collisionInfo.collider.name == "Spikes")
            TakeDamage(1, 0, 1400);
        else if (_collisionInfo.collider.name == "Earth Wisp" || _collisionInfo.collider.name == "Assault Droid")
            TakeDamage(1, 900, 600);
    }

    // this is used to interact with the world
    bool CheckForInteraction() {
            
        Collider2D[] _objectsWithinRange = Physics2D.OverlapCircleAll(_interactionPoint.position, _interactionRadius, _whatIsInteractable);

        if (Input.GetButtonDown("RB")) {

            foreach(Collider2D _object in _objectsWithinRange) {
                
                switch(_object.name) {

                    case "TreasureChest":
                        _object.GetComponent<TreasureChest>().SetTrigger("ChestOpen"); // Chest Item is awarded in the ChestOpen animation
                        break;
                    case "Merchant":
                        _shopPanel.SetActive(true);
                        break;
                    case "End_Level":
                        _endLevelMenu.SetActive(true);
                        break;
                    default:
                        Debug.Log("Nothing");
                        break;
                }
            }

            return true;
         }
         return false;
    }

    // NOT being used at the moment
    bool Blocking() {
        if (Input.GetButton("LB") && Idling()) {
            SetState(State.Blocking);
            _animator.SetTrigger("Blocking");
            _animator.SetBool("IsBlocking", true);
            _canMove = false;
            _canAttack = false;
            _canDash = false;

            return true;
        }
        else {
            _animator.SetBool("IsBlocking", false);
            _canMove = true;
            _canAttack = true;
            _canDash = true;
            return false;
        }
    }

    void WallJump(float x, float y) {

        if (_characterController2D.GetFacingRight()) {
            _rigidBody2D.velocity = new Vector2(x, y);

        }
        else
            _rigidBody2D.velocity = new Vector2(-x, y);
            
        SetState(State.Wall_Jumping);
        _animator.SetFloat("VelocityY", 20);
    }

    private void DashAbility() {
        if (Input.GetButtonDown("XboxB") && Running() && _canDash)
            StartCoroutine(Dash());
    }

    IEnumerator Dash() {
        SetState(State.Dashing);
        _runSpeed = _dashSpeed;
        _animator.SetTrigger("Dashing");
        _canDash = false;
        yield return new WaitForSeconds(_dashTime);
        _runSpeed = DEFAULT_RUN_SPEED;
        yield return new WaitForSeconds(_timeBtwDashes);
        _canDash = true;
    }

    void ResetVelocity () => _rigidBody2D.velocity = new Vector2(0, 0);

    private void ApplyForce(float x, float y) {
        ResetVelocity();
        _rigidBody2D.AddForce(new Vector2(x, y));
    }

    // this provides a small jump to the player when air attacking
    public void AirAttackForce() {
        ResetVelocity();
        _rigidBody2D.AddForce(new Vector2(0, 500));
    }
}