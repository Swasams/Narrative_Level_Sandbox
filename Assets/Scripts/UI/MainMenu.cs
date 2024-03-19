using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [Header("Menu UI")]
    [SerializeField] private GameObject settingsGroup;
    [SerializeField] private GameObject controlsGroup;

    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        LoadVolume();
    }

    public void Play()
    {
        LevelManager.Instance.LoadScene(1, "CrossFade");
        MusicManager.Instance.PlayMusic("Gameplay");
    }

    public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void UpdateSoundVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void SaveVolume()
    {
        audioMixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        audioMixer.GetFloat("SFXVolume", out float sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void OpenSettings()
    {
        settingsGroup.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsGroup.SetActive(false);
    }

    public void OpenControls()
    {
        controlsGroup.SetActive(true);

        settingsGroup.SetActive(false);
    }

    public void CloseControls()
    {
        controlsGroup.SetActive(false);

        settingsGroup.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
