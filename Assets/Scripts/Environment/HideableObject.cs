using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableObject : MonoBehaviour
{
    public GameObject player;
    public float hideDuration = 5f;
    public float hideCooldown = 10f;

    private bool canHide = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (canHide)
            {
                HidePlayer();
            }
        }
    }

    private void HidePlayer()
    {
        player.SetActive(false); // Hide the player

        // Disable player controls or other relevant behaviors

        canHide = false; // Prevent hiding again immediately

        Invoke(nameof(RevealPlayer), hideDuration);
        Invoke(nameof(ResetHideCooldown), hideCooldown);
    }

    private void RevealPlayer()
    {
        player.SetActive(true); // Reveal the player

        // Enable player controls or other relevant behaviors
    }

    private void ResetHideCooldown()
    {
        canHide = true; // Allow hiding again
    }
}
