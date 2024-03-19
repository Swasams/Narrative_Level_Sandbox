using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIManagerV1 : MonoBehaviour
{
    TopDownPlayerMovement pm;

    [Header("Pause Menu UI")]
    [SerializeField] private GameObject pauseGroup;
    [SerializeField] private GameObject settingsGroup;
    [SerializeField] private GameObject controlsGroup;
    public GameObject player;

    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        pm = player.GetComponent<TopDownPlayerMovement>();

        LoadVolume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

        if (Time.timeScale == 0f)
        {
            pm.isPaused = true;
        }
        else
        {
            pm.isPaused = false;
        }
    }

    public void Pause()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;

            OpenMenu();
        }
        else
        {
            Time.timeScale = 1f;

            pauseGroup.SetActive(false);

            settingsGroup.SetActive(false);

            controlsGroup.SetActive(false);
        }
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

    public void StartMenu()
    {
        LevelManager.Instance.LoadScene(0, "CrossFade");
        Time.timeScale = 1f;
    }

    public void OpenMenu()
    {
        pauseGroup.SetActive(true);
    }

    public void CloseMenu()
    {
        pauseGroup.SetActive(false);

        Time.timeScale = 1f;
    }

    public void OpenSettings()
    {
        settingsGroup.SetActive(true);

        pauseGroup.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsGroup.SetActive(false);

        OpenMenu();
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
