using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightIntroduction : MonoBehaviour
{
    [SerializeField] private CanvasGroup introCanvas;
    [SerializeField] private AudioSource audioPlayer;

    private PlayerMovement playerObject;
    private PlayerSFXHandler playerSFX;

    private void Awake()
    {
        playerObject = FindObjectOfType<PlayerMovement>();
        playerSFX = playerObject.GetComponent<PlayerSFXHandler>();
    }

    private void Start()
    {
        playerObject.enabled = false;
        playerSFX.enabled = false;

        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        Debug.Log("Night intro playing");

        yield return new WaitForSeconds(2f);

        audioPlayer.Play();

        playerObject.enabled = true;
        playerSFX.enabled = true;

        LeanTween.alphaCanvas(introCanvas, 0, 1.5f);
    }
}
