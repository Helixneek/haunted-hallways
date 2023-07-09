using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXHandler : MonoBehaviour
{
    [Header("SFX Clips")]
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioClip openDoorSounds;
    [SerializeField] private AudioClip stairsSounds;

    [Header("SFX Interval")]
    [SerializeField] private float footstepInterval = 0.5f;

    private AudioSource audioSource;
    private float footstepTimer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ActivateFootstepSound();
    }

    private void ActivateFootstepSound()
    {
        // Check if the player is moving
        if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
        {
            // Check if enough time has passed to play footstep sound
            if (footstepTimer <= 0f)
            {
                PlayFootstepSound();
                footstepTimer = footstepInterval;
            }
            else
            {
                footstepTimer -= Time.deltaTime;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepSounds.Length == 0)
        {
            Debug.LogWarning("No footstep sounds assigned to the player.");
            return;
        }

        // Play a random footstep sound from the footstepSounds array
        int randomIndex = Random.Range(0, footstepSounds.Length);
        audioSource.clip = footstepSounds[randomIndex];
        audioSource.Play();
    }

    public void PlayOpenDoorSound()
    {
        if (openDoorSounds == null)
        {
            Debug.LogWarning("No door sounds assigned to the player.");
            return;
        }

        audioSource.clip = openDoorSounds;
        audioSource.Play();
    }

    public void PlayStairsSound()
    {
        if (stairsSounds == null)
        {
            Debug.LogWarning("No stair sounds assigned to the player.");
            return;
        }

        audioSource.clip = stairsSounds;
        audioSource.Play();
    }
}
