using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void StartMenu()
    {
        LevelManager.Instance.LoadScene(0, "CrossFade");
        Time.timeScale = 1f;
    }

    public void Entrance()
    {
        LevelManager.Instance.LoadScene(1, "CrossFade");
        SoundManager.Instance.PlaySound2D("Rain");
        MusicManager.Instance.PlayMusic("Gameplay");
    }

    public void End()
    {
        LevelManager.Instance.LoadScene(5, "CrossFade");
    }
}
