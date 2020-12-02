using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
  Sources: B., Brackeys, 'How to make AWESOME Scene Transitions in Unity!', 2020. [Online]. Available: https://www.youtube.com/watch?v=CE9VOZivb3I [Accessed: Dec-01-2020].
*/

public class SceneLoader : MonoBehaviour {

    public Animator _transition;
    private float _transitionTime = 1.0f;

    public void LoadLevel(int _loadIndex) {
        
      StartCoroutine(ILoadLevel(_loadIndex));
      // SceneManager.LoadScene(_loadIndex);
      if (Time.timeScale == 0)
        Time.timeScale = 1;  
    }

    IEnumerator ILoadLevel(int _index) {
      _transition.SetTrigger("Start");

      yield return new WaitForSeconds(_transitionTime);

      SceneManager.LoadScene(_index); 
    }

    public void LoadLevel(string _levelName) {

      SceneManager.LoadScene(_levelName);
      if (Time.timeScale == 0)
        Time.timeScale = 1;
    }

    public void Quit() => Application.Quit();
}
