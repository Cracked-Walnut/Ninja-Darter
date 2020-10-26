using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private int _health = 100;
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _animator;
    private BoxCollider2D _boxCollider2D;
    private bool _facingRight;
    private XP _xp;
    private Inventory _inventory;

    private void Awake() { 
        _xp = _player.GetComponent<XP>();
        _inventory = _player.GetComponent<Inventory>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void SetFacingRight(bool _facingRight) => this._facingRight = _facingRight;

    public void TakeDamage(int _damage) {
        
        _health -= _damage;

        if (_health <= 0)
            Dead();
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

    public void Dead() { 
        _animator.SetTrigger("Dead");
        _boxCollider2D.enabled = !_boxCollider2D.enabled;
        _xp.AddPoints(50);
        _inventory.AddKill(1);
    }
}
