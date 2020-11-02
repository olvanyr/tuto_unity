using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int coinsCount;
    public Text coinsCountText;

    public static Inventory instance; //static variable are like global variable, you can accesse them form any script by using scriptName.StaticVarName

    private void Awake()//the awake function is called when a scene start  so at the beggining of the scene, we creat store the instance id into a static var named instance
    {
        if(instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance d'inventory dans la scéne");
            return;
        }
        instance = this;
    }


    public void AddCoins(int count)
    {
        coinsCount += count;
        coinsCountText.text = coinsCount.ToString();
    }
}
