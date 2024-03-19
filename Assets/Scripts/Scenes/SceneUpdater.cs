using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUpdater : MonoBehaviour
{
    public static SceneUpdater Instance;

    [Header("Current Scene Number")]
    [SerializeField] private int buildIndex;
    private Scene currentScene;

    [Header("Pause Day Cycle")]
    [Tooltip("If you want to enable/disable the day/night cycle timer while inside a scene use this bool")]
    [SerializeField] private bool stopDayCycle = false;

    private GameObject outsideLighting;
    DayNightSystem2D dayNightCycle;


    private void Awake()
    {
        outsideLighting = GameObject.FindWithTag("DayCycle");
        dayNightCycle = outsideLighting.GetComponent<DayNightSystem2D>();
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        // Retrieves the index of the scene in the project's build settings.
        buildIndex = currentScene.buildIndex;

        // Check the scene name as a conditional.
        switch (buildIndex)
        {
            case 0:
                MusicManager.Instance.PlayMusic("Menus");
                Outside();
                break;
            case 1:
                RoomManager.Instance.ResetInts();
                Outside();
                break;
            case 2:
                MusicManager.Instance.PlayMusic("Wind");
                Inside();
                break;
            case 3:
                Outside();
                break;
            case 4:
                Outside();
                break;
            case 5:
                MusicManager.Instance.PlayMusic("Menus");
                Outside();
                break;
        }
    }

    private void Outside()
    {
        if (stopDayCycle)
        {
            dayNightCycle.cycleTimer = true;
        }

        dayNightCycle.lightsStatus = false;
        dayNightCycle.FindGlobalLight();
        dayNightCycle.FindMapLights();
    }

    private void Inside()
    {
        if (stopDayCycle)
        {
            dayNightCycle.cycleTimer = false;
        }

        dayNightCycle.lightsStatus = true;
        dayNightCycle.FindGlobalLight();
        dayNightCycle.FindMapLights();
    }
}
