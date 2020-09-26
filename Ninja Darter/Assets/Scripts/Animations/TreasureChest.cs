using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour {

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _player;
    private Inventory _inventory;
    private BoxCollider2D _boxCollider2D;

    private void Awake() { 
        // _inventory = FindObjectOfType<Inventory>();
        _inventory = _player.GetComponent<Inventory>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void SetTrigger(string _triggerName) => _animator.SetTrigger(_triggerName);

    public void AwardItem(string _item) {

        switch(_item) {
            case "Fragment":
                _inventory.AddFragment(1);
                Debug.Log(_inventory.GetCubeFragments());
                break;
            default:
                Debug.Log("No Item to Award. Please try a different name.");
                break;
        }

        DisableCollider();
    }

    private void DisableCollider() => _boxCollider2D.enabled = !_boxCollider2D.enabled;
    
}
