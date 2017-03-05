using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public AnimationClip rotation0;                     // Animation for rotation to 180°
    public AnimationClip rotation180;                   // Animation for rotation to 360°

    public bool startPosition = true;                   // variable to check if camera is in start position


    public void rotate180()
    {
        if (startPosition == true)                                          // if camera is in start position
        {
            gameObject.GetComponent<Animation>().clip = rotation0;          // set clip for rotation to 180°
            startPosition = false;
        }


        else                                                                // if camera is not in start position
        {
            gameObject.GetComponent<Animation>().clip = rotation180;        // set clip for rotation to 360°
            startPosition = true;
        }

            gameObject.GetComponent<Animation>().Play();                    // play animation
    }
}