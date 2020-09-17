using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour {

    [SerializeField] private Animator _animator;

    public void SetTrigger(string _triggerName) => _animator.SetTrigger(_triggerName);

}
