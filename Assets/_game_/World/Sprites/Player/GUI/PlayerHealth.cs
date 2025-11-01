using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    //Variables for player health
    public int currentHealth;
    public int maxHealth;

    //Variables for health UI
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    void Update()
    {
        UpdateHeartUI();

    }

    public void UpdateHeartUI()
    {
        //Display hearts
        for (int i = 0; i < hearts.Length; i++)
        {
            //Display either full or empty heart depending on current health
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;

            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            //Display health accordingly
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void ChangeHealth(int amount) //Function shared among other objects to affect health
    {
        currentHealth += amount;

        UpdateHeartUI();

        if (currentHealth <= 0)  //Placeholder for player death sequence
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(2);

        }


    }
}
