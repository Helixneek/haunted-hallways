using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySFXHandler : MonoBehaviour
{
    public AudioClip[] soundEffects;
    public float minDelay = 2f;
    public float maxDelay = 5f;

    private AudioSource audioSource;
    private float soundTimer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        soundTimer = Random.Range(minDelay, maxDelay);
    }

    private void Update()
    {
        soundTimer -= Time.deltaTime;

        if (soundTimer <= 0f)
        {
            audioSource.volume = 0.25f;
            PlayRandomSoundEffect();
            soundTimer = Random.Range(minDelay, maxDelay);
        }

        audioSource.volume = 0.5f;
    }

    private void PlayRandomSoundEffect()
    {
        if (soundEffects.Length > 0)
        {
            AudioClip randomClip = soundEffects[Random.Range(0, soundEffects.Length)];
            audioSource.PlayOneShot(randomClip);
        }
    }
}
