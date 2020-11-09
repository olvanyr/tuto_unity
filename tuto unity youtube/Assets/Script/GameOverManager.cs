using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;


    //all the reference section in instance is called a single tone
    public static GameOverManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de GameOverManager dans la scéne");
            return;
        }
        instance = this;
    }


    public void OnPlayerDeath()
    {
        if (CurrentSceneManager.instance.isPlayerPresentByDefault)
        {
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        }

        gameOverUI.SetActive(true);
    }

    public void RetryButton()
    {
        Inventory.instance.RemoveCoins(CurrentSceneManager.instance.coinPickedUpInThisSceneCount); //remove coin picked upd durring the last rune

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //reload the scene (wee need the build index to load the scene)

        PlayerHealth.instance.Respawn(); //set all the player varibale back to normal

        gameOverUI.SetActive(false); //hide the menu
    }

    public void MainMenuButton()
    {
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        Application.Quit(); //application is a ref to our game
    }
}