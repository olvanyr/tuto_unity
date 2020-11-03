using UnityEngine;

public class dontDestroyOnLoadScene : MonoBehaviour
{

    public GameObject[] objects;
    private void Awake()
    {
        foreach (var element in objects)
        {
            DontDestroyOnLoad(element);
        }
    }
}
