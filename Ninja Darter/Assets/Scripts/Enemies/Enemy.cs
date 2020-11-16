using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private GameObject _player;
    [SerializeField] private int _health = 10;
    [SerializeField] private int _xpUponDeath = 2;
    [SerializeField] private string _startAnimation;
    [SerializeField] private int _emeraldDropAmount;
    [SerializeField] private int _coinDropAmount;
    [SerializeField] private int _dropPercentage;
    
    public bool _isDead;
    private float _hurtTime = 0.2f;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private bool _facingRight;
    private XP _xp;
    private Inventory _inventory;

    void Start() { 
        _isDead = false;
        _animator.SetTrigger(_startAnimation);
    }
    
    void Awake() { 
        _xp = _player.GetComponent<XP>();
        _inventory = _player.GetComponent<Inventory>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void SetFacingRight(bool _facingRight) => this._facingRight = _facingRight;

    public void TakeDamage(int _damage) {
        
        _health -= _damage;
        StartCoroutine(Hurt());

        if (_health <= 0)
            _animator.SetTrigger("Dead");
    }

    void Die() => Destroy(gameObject);
    
    bool isFacingRight() {

        if (_facingRight) {
            SetFacingRight(false); // turn left
            transform.Rotate(0f, 180f, 0f);
            return false;
            
        } else {
            SetFacingRight(true); // turn right
            transform.Rotate(0f, 0f, 0f);
            return true;
        }        
    }

    public IEnumerator Hurt() {
        _spriteRenderer.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(_hurtTime);
        _spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void RollDrops() {
        float _chanceDrop = Random.Range(1f, 100f);
        if (_chanceDrop  <= _dropPercentage) {
            float _coinOrEmerald = Random.Range(1, 8);

            if (_coinOrEmerald <= 2)
                _inventory.AddEmerald(_emeraldDropAmount);
            else
                _inventory.AddCoin(_coinDropAmount);
        }
    }

    public void Dead() {
        _isDead = true;
        _animator.SetBool("IsDead", _isDead);
        // _animator.SetTrigger("Dead");
        _boxCollider2D.enabled = !_boxCollider2D.enabled;
        _xp.AddPoints(_xpUponDeath);
        _inventory.AddKill(1);
        RollDrops();
    }
}
