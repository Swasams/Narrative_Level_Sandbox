using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private SceneUpdater sceneUpdater;

    private void Awake()
    {
        sceneUpdater = FindObjectOfType<SceneUpdater>();
    }

    public void MainMenu()
    {
        sceneUpdater.MainMenu();
    }

    public void RestartGame()
    {
        sceneUpdater.RestartGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
