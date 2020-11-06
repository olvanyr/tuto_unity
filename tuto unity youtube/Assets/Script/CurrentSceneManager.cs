using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public bool isPlayerPresentByDefault = false;
    public int coinPickedUpInThisSceneCount;

    //all the reference section in instance is called a single tone
    public static CurrentSceneManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de CurrentSceneManager dans la scéne");
            return;
        }
        instance = this;
    }

    
}
