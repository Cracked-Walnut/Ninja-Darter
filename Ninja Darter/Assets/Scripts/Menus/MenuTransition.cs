using UnityEngine;

public class MenuTransition : MonoBehaviour {

    [SerializeField] private GameObject _mainMenuObject;
    [SerializeField] private GameObject _optionsObject;

    public void SetMainMenuActive () {
        _mainMenuObject.SetActive(true);
        _optionsObject.SetActive(false);
    }

    public void SetMainOptionsActive () {
        _mainMenuObject.SetActive(false);
        _optionsObject.SetActive(true);
    }
    
}
