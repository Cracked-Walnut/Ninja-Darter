using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Sources:
    1) B., Brackeys, 'How to make a HEALTH BAR in Unity!', 2020. [Online]. Available: https://www.youtube.com/watch?v=BLfNP4Sc_iA [Accessed: Oct-25-2020].
*/

public class XPBar : MonoBehaviour {

    [SerializeField] private Slider _slider;

    public void SetXP(int _xp) => _slider.value = _xp;
}
