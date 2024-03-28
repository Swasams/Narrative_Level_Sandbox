using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonManger : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene(2);
    }
}
