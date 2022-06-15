using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour
{
    // this scripts mainly controls the pause/start/scene switch of the game, the save/load of game file and the win/death of the player
    public static GlobalControl Instance;
    public bool gamePaused {get { return _gamePaused;}}
    private bool _gamePaused = false;
    public bool videoPlayed = false;
    // private bool toGetRef = false;

    private Vector3 iniPos = new Vector3(3566.32f, 1.96f, -2620.85f);  // initial spawn position
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
        /*
        things to do in the restart (undo the changes the player made to the env):
        (1) move the player back to the starting point
        (2) reset player properties
        (3) reset the inventory, crafting and craftes slots
        (4) reset the bars
        (5) re-generate the pickup objects
        (6) reset the escape rocket state
        */
        Player.Instance.disabled = true;

        Player.Instance.SetPos(iniPos);
        InventoryManager.Instance.Items = new List<Item>();
        FindObjectOfType<InventoryUI>().UpdateUI();
        CraftingManager.Instance.Crafting = new List<Item>();
        CraftingManager.Instance.Crafted = new List<Item>();
        CraftingManager.Instance.DropItems();
        PlayerProperty.Instance.InitializeProperties();

        Transform generated = FindObjectOfType<PickupGenerator>().parentTF;
        foreach (Transform child in generated)
        {
            GameObject.Destroy(child.gameObject);  // destroy all generated pickups
        }
        foreach (Transform preDefinedPickupTF in generated.parent)
        {
            if (!preDefinedPickupTF.gameObject.activeSelf)
            {
                preDefinedPickupTF.gameObject.SetActive(true);  // re-active the predefined pickups
            }
        }
        FindObjectOfType<PickupGenerator>().SpawnPickups(true);

        FindObjectOfType<EscapeRocket>().LoadSetFuel(0);

        Player.Instance.disabled = false;
    }

    public void StartLoad()
    {
        // load game
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
        // quit game
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
}
