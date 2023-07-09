using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public float duration = 5f;
    public RawImage videoOutput;

    [SerializeField] private AudioClip staticSounds;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        videoOutput.gameObject.SetActive(false);
    }

    public IEnumerator PlayVideoAndDisappear()
    {
        videoOutput.gameObject.SetActive(true);
        videoPlayer.Play();
        PlayStaticSound();

        yield return new WaitForSeconds(duration);

        videoPlayer.Stop();
        videoOutput.gameObject.SetActive(false);
        gameObject.SetActive(false);
        
    }

    public void PlayStaticSound()
    {
        if (staticSounds == null)
        {
            Debug.LogWarning("No pickup sounds assigned to the item.");
            return;
        }

        audioSource.clip = staticSounds;
        audioSource.Play();
    }
}
