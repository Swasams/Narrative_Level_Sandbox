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

    [Header("Debug")]
    [SerializeField] private bool debug = false;

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
                MusicManager.Instance.PlayMusic("StartMenu");
                Outside();
                break;
            case 1:
                MusicManager.Instance.PlayMusic("Wind");
                RoomManager.Instance.ResetInts();
                Outside();
                break;
            case 2:
                MusicManager.Instance.PlayMusic("MainStore");
                Inside();
                break;
            case 3:
                MusicManager.Instance.PlayMusic("BreakRoom");
                Inside();
                break;
            case 4:
                MusicManager.Instance.PlayMusic("Basement");
                DarkLighting();
                break;
            case 5:
                ChaseLighting();
                break;
            case 6:
                ChaseLighting();
                break;
            case 7:
                Outside();
                break;
            case 8:
                MusicManager.Instance.PlayMusic("EndMenu");
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

    private void DarkLighting()
    {
        if (stopDayCycle)
        {
            dayNightCycle.cycleTimer = false;
        }

        dayNightCycle.lightsStatus = true;
        dayNightCycle.FindGlobalLight();
        dayNightCycle.FindMapLights();
        dayNightCycle.DarkLighting();
    }

    private void ChaseLighting()
    {
        if (stopDayCycle)
        {
            dayNightCycle.cycleTimer = false;
        }

        if (debug)
        {
            dayNightCycle.lightsStatus = false;
        }
        else
        {
            dayNightCycle.lightsStatus = true;
        }
        dayNightCycle.FindGlobalLight();
        dayNightCycle.FindMapLights();
        dayNightCycle.ChaseLighting();
    }

    public void StartChaseMusic()
    {
        MusicManager.Instance.PlayMusic("Chase");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
