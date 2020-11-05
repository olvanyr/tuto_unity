using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;


    public bool isInvincible = false;
    public float invicibilityFlashDelay;
    public float invicibilityDelay;


    public SpriteRenderer graphics;


    public static PlayerHealth instance; //static variable are like global variable, you can accesse them form any script by using scriptName.StaticVarName

    private void Awake()//the awake function is called when a scene start  so at the beggining of the scene, we creat store the instance id into a static var named instance
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de PlayerHealth dans la scéne");
            return;
        }
        instance = this;
    }


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }

    }

    public void HealPlayer(int heal)
    {
        if ((currentHealth + heal) > maxHealth) 
        { 
            currentHealth = maxHealth; 
        }else
        {
            currentHealth += heal;
            healthBar.SetHealth(currentHealth);
        }
        

    }


    public IEnumerator InvincibilityFlash()
    { 
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }
    }
    public IEnumerator HandleInvincibilityDelay()
    { 
       yield return new WaitForSeconds(invicibilityDelay);
       isInvincible = false;
 
    }
}
