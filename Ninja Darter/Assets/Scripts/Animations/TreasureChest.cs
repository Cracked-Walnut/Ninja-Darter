using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour {

    [SerializeField] private Animator _animator;
    private Inventory _inventory;

    private void Awake() => _inventory = FindObjectOfType<Inventory>();

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
    }
    
}
