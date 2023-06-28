using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorAnimation : MonoBehaviour
{
    public Animator doorAnimator; // Reference to the Animator component of the door

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            doorAnimator.SetTrigger("Open"); // Trigger the "Open" animation in the door's Animator
        }

        else if (collision.CompareTag("Door"))
        {
            doorAnimator.SetTrigger("Closed");
        }
    }
}
