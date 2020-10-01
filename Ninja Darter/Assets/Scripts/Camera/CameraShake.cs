using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Sources:
1) B., Brackeys, 'CAMERA SHAKE in Unity', 2018. [Online]. Available: https://www.youtube.com/watch?v=9A9yj8KnM8c [Accessed: 30-Sep-2020].
*/

public class CameraShake : MonoBehaviour {

    public IEnumerator Shake (float duration, float magnitude) {
            
            Vector3 originalPos = transform.position;
        
            float elapsed = 0.0f;

            while (elapsed < duration) {
                // Generate a random x & y value, then add it to the current camera x & y coordinate, creating a shake effect
                float x = (Random.Range(-1f, 1f) * magnitude) + transform.position.x; 
                float y = (Random.Range(-1, 1f) * magnitude) + transform.position.y;

                transform.position = new Vector3(x ,y, originalPos.z); // apply the shake to the current frame

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.position = originalPos; // revert the camera to its original position
        }


}
