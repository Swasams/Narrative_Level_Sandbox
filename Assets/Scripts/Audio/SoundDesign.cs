using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDesign : MonoBehaviour
{
    public bool BGM, SFX;
    AudioSource gameAudio;

    void Start()
    {
        gameAudio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (BGM)
        {
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("BGM");
        }
        else
        {
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFX");
        }
    }
}
