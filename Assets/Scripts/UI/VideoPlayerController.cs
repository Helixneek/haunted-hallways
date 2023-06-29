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

    private void Start()
    {
        videoOutput.gameObject.SetActive(false);
    }

    public IEnumerator PlayVideoAndDisappear()
    {
        videoOutput.gameObject.SetActive(true);
        videoPlayer.Play();

        yield return new WaitForSeconds(duration);

        videoPlayer.Stop();
        videoOutput.gameObject.SetActive(false);
        gameObject.SetActive(false);
        
    }
}
