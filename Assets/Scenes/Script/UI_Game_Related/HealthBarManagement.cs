using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBarManagement : MonoBehaviour
{
    public int maxHealth = 3;                // Maximum health of the player
    public Image[] heartImages;              // Array of heart images representing each heart
    public int respawnSceneIndex;            // Index of the scene to respawn in

    private int currentHealth;               // Current health of the player

    void Start()
    {
        currentHealth = maxHealth;           // Initialize current health to maximum
        UpdateHearts();                      // Update the heart display
    }

    void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;       // Subtract damage from current health

        if (currentHealth <= 0)
        {
            // Player is dead, load the respawn scene
            SceneManager.LoadScene(respawnSceneIndex);
        }
        else
        {
            UpdateHearts();                  // Update the heart display
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < currentHealth)
            {
                // Set the heart sprite to the active sprite
                heartImages[i].gameObject.SetActive(true);
            }
            else
            {
                // Disable the heart sprite
                heartImages[i].gameObject.SetActive(false);
            }
        }
    }
}