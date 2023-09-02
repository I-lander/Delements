using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource clic;

    public void PlayClic()
    {
        if (!MuteButton.isMute)
        {
            clic.Play();
        }
    }
}
