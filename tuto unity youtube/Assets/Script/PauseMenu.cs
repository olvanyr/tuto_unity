using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resumed();
            }
            else
            {
                Paused();
            }
        }
    }

    void Paused()
    {
        //activate our pause menu
        pauseMenuUI.SetActive(true);
        //stop other fonction of the game
        Time.timeScale = 0;
        PlayerMovement.instance.enabled = false;
        //change game status toggle pause game mod
        gameIsPaused = true;
    }
    
    public void Resumed()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        PlayerMovement.instance.enabled = true;
        gameIsPaused = false;
    }
    
    public void LoadMainMenu()
    {
        Resumed();
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        SceneManager.LoadScene("MainMenu");  
    }
}
