﻿using UnityEngine;
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



    //all the reference section in instance is called a single tone
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
            TakeDamage(50);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            //check if player still alive
            if (currentHealth <= 0)
            {
                Death();
                return;
            }

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
    
    public void Death()
    {
        Debug.Log("player died");

        //lock player movement
        PlayerMovement.instance.enabled = false;

        //play elimination animation
        PlayerMovement.instance.animator.SetTrigger("playerDeath");//call a trigger inside the palayer animator to trigger the next animation

        // remove physics interaction with everything else
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.playerCol.enabled = false;
        PlayerMovement.instance.rb.velocity = Vector3.zero;

        //handle the menu
        GameOverManager.instance.OnPlayerDeath();
    }
    
    public void Respawn()
    {
        
        //enable player movement
        PlayerMovement.instance.enabled = true;

        //Enable animation system basicaly go to die animation to idle
        PlayerMovement.instance.animator.SetTrigger("Respawn"); //call a trigger inside the palayer animator to trigger the next animation

        //Enable physics interaction with everything else
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCol.enabled = true;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);

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
