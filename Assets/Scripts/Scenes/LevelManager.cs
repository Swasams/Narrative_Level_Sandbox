using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public Slider progressBar;
    public GameObject transitionsContainer;

    private SceneTransition[] transitions;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        transitions = transitionsContainer.GetComponentsInChildren<SceneTransition>();
    }

    public void LoadScene(int sceneNumber, string transitionName)
    {
        StartCoroutine(LoadSceneAsync(sceneNumber, transitionName));
    }

    private IEnumerator LoadSceneAsync(int sceneNumber, string transitionName)
    {
        SceneTransition transition = transitions.First(t => t.name == transitionName);

        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneNumber);
        scene.allowSceneActivation = false;

        yield return transition.AnimateTransitionIn();

        progressBar.gameObject.SetActive(true);

        do
        {
            progressBar.value = scene.progress;
            yield return null;
        } while (scene.progress < 0.9f);

        yield return new WaitForSeconds(1f);

        scene.allowSceneActivation = true;

        progressBar.gameObject.SetActive(false);

        yield return transition.AnimateTransitionOut();
    }
}
