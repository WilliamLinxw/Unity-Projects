using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour
{
    // this scripts mainly controls the pause/start of the game, the save/load of game file and the win/death of the player
    public static GlobalControl Instance;
    public bool gamePaused {get { return _gamePaused;}}
    private bool _gamePaused = false;
    public bool videoPlayed = false;
    // private bool toGetRef = false;

    public SaveLoadSystem sls;  // enable to start from load
    public GameObject tutorialBox1;
    public GameObject escapeRocket;
    public GameObject deathMenu;
    public GameObject[] ObjForStartVideo;
    public GameObject[] ObjForWinVideo;
    

    public List<GameObject> HiddenObjects;  // a series of objects that need to be hidden when calling the pause menu
    private bool propBarsState;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        SceneManager.LoadScene("StartScene", LoadSceneMode.Additive);
    }

    private void Update() 
    {
        // if (toGetRef)
        // {
        //     GetReferences();
        //     return;
        // }
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
        // get the current velocity of the player (during jumping)
        if (Player.Instance.isJumping)
        {
            Player.Instance.GetVel();
        }
        
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

    public void StartGame()
    {
        Player.Instance.disabled = true;
        Cursor.visible = false;
        PlayStartVideo();
        Time.timeScale = 1f;
        AudioListener.pause = false;
        FindObjectOfType<PickupGenerator>().SpawnPickups(true);
        tutorialBox1.SetActive(true);
        Player.Instance.disabled = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
        Debug.Log("New scene is loaded!");
        FindObjectOfType<PickupGenerator>().SpawnPickups(true);
        escapeRocket.GetComponent<EscapeRocket>().LoadSetFuel(0);
        _gamePaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        // toGetRef = true;
        Player.Instance.disabled = false;
    }

    public void StartLoad()
    {
        videoPlayed = true;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        sls.LoadPlayer();
        FindObjectOfType<PickupGenerator>().SpawnPickups(false);
        tutorialBox1.SetActive(false);
        Player.Instance.disabled = false;
    }

    private void SwitchListActiveState()
    {
        foreach (GameObject obj in HiddenObjects)
        {
            obj.SetActive(false);  // set all bars to inactive
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Win()
    {
        Cursor.visible = false;
        PlayWinVideo();
    }

    public void Death()
    {
        Time.timeScale = 0.1f;
        Player.Instance.disabled = true;
        deathMenu.SetActive(true);
    }

    private void PlayStartVideo()
    {
        foreach (GameObject obj in ObjForStartVideo)
        {
            obj.SetActive(true);
        }
        ObjForStartVideo[1].GetComponent<StreamVideo>().enabled = true;
    }

    private void PlayWinVideo()
    {
        foreach (GameObject obj in ObjForWinVideo)
        {
            obj.SetActive(true);
        }
        ObjForWinVideo[1].GetComponent<StreamVideo>().enabled = true;
    }

    public void BackToStartScene()
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Additive);
    }

    // since after reload the references would be missing; deprecated!
    /*
    private void GetReferences()
    {
        Debug.Log("Load references");
        sls = GameObject.Find("SaveLoadSystem").GetComponent<SaveLoadSystem>();
        
        tutorialBox1 = GameObject.Find("Help 1");
        escapeRocket = GameObject.Find("SM_Veh_Shuttle_02");
        deathMenu = GameObject.Find("DeathMenu");
        ObjForStartVideo = new GameObject[2];
        ObjForStartVideo[0] = GameObject.Find("RawImage");
        ObjForStartVideo[1] = GameObject.Find("Video Player - Start");
        ObjForWinVideo = new GameObject[2];
        ObjForWinVideo[0] = GameObject.Find("RawImage");
        ObjForWinVideo[1] = GameObject.Find("Video Player - Win");

        HiddenObjects = new List<GameObject>();
        HiddenObjects.Add(GameObject.Find("PropertyBars"));
        HiddenObjects.Add(GameObject.Find("Inventory"));
        HiddenObjects.Add(GameObject.Find("Crafting"));

        toGetRef = false;  // flag off 
    }
    */
}
