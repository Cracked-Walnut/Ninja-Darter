using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    [SerializeField] private GameObject _hpObject;
    private HealthPotion _healthPotion;

    [SerializeField] private GameObject _armourObject;
    private Armour _armour;

    void Awake() {
        _healthPotion = _hpObject.GetComponent<HealthPotion>();
        _armour = _armourObject.GetComponent<Armour>();
    }

    public void BuyPotion() => _healthPotion.BuyHPInShop();

    public void BuyArmour() => _armour.BuyArmourInShop();

}
