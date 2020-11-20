using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour {

    [SerializeField] private GameObject _endLevelMenu;

    public void ClosePanel() => _endLevelMenu.SetActive(false);

}
