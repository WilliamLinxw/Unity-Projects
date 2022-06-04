using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneControl : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.UnloadSceneAsync("StartScene");
        GlobalControl.Instance.StartGame();
    }
    public void OnStartLoadButton()
    {
        SceneManager.UnloadSceneAsync("StartScene");
        GlobalControl.Instance.StartLoad();
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    
}
