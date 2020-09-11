using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private int _health = 100;
    private bool _facingRight;

    private void SetFacingRight(bool _facingRight) => this._facingRight = _facingRight;

    public void TakeDamage(int _damage) {
        
        _health -= _damage;

        if (_health <= 0)
            Die();
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
}
