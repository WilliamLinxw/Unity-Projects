using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    // this scripts mainly controls the pause/start of the game and the save/load of game file
    public bool gamePaused {get { return _gamePaused;}}
    private bool _gamePaused = false;

    public List<GameObject> HiddenObjects;  // a series of objects that need to be hidden when calling the pause menu
    private bool propBarsState;

    private void Update() 
    {
        if (Input.GetButtonDown("Esc Menu"))
        {
            if (_gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;    
        AudioListener.pause = true;
        propBarsState = HiddenObjects[0].activeSelf;
        SwitchListActiveState();
        _gamePaused = true;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        HiddenObjects[0].SetActive(propBarsState);
        _gamePaused = false;
    }

    private void SwitchListActiveState()
    {
        foreach (GameObject obj in HiddenObjects)
        {
            obj.SetActive(false);
        }
    }
}
