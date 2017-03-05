using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour {

    

    public AudioClip ButtonUp;              // Sound for Increase-, Forward-, Start-Button
    public AudioClip ButtonDown;            // Sound for Decrease-, Back-Button
    public AudioSource menuAudio;


    public void playButtonUp()
    {
        menuAudio.clip = ButtonUp;          // set Audioclip
        menuAudio.Play();                   // play Audioclip
    }

    public void playButtonDown()
    {
        menuAudio.clip = ButtonDown;        // set Audioclip
        menuAudio.Play();                   // play Audioclip
    }
}
