using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public bool BGM, SFX;
    Slider volumeSlider;

    void Start()
    {
        volumeSlider = gameObject.GetComponent<Slider>();
    }

    void Update()
    {
        if (BGM)
        {
            PlayerPrefs.SetFloat("BGM", volumeSlider.value);
        }
        else
        {
            PlayerPrefs.SetFloat("SFX", volumeSlider.value);
        }


    }
}
