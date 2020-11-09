using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;

    public GameObject settingsWindow;



    public void StartGameButton()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    
    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
    }
    public void CloseSettingsButton()
    {
        settingsWindow.SetActive(false);
    }
    
    public void QuitGameButton()
    {
        Application.Quit();
    }
}
