using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunHandler : MonoBehaviour
{
    private bool isStunned = false;
    private PlayerMovement playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerMovement>();
    }

    public void Stun(float duration)
    {
        if (!isStunned)
        {
            isStunned = true;
            playerInput.Freeze();
            playerInput.enabled = false; // Disable player input
            Debug.Log("Player stunned for " + duration + " seconds");
            Invoke(nameof(EndStun), duration);
        }
    }

    private void EndStun()
    {
        isStunned = false;
        playerInput.enabled = true; // Re-enable player input
        //playerInput.Unfreeze();
        Debug.Log("Player stun ended");
    }
}
