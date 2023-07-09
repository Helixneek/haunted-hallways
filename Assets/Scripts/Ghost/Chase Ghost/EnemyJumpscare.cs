using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyJumpscare : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Canvas blackBG;
    [SerializeField] private CanvasGroup jumpscareCanvas;
    [SerializeField] private CanvasGroup gameoverCanvas;
    [SerializeField] private AudioClip jumpscareSound;
    [SerializeField] private float jumpscareTimer;

    private float timeElapsed;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        jumpscareCanvas.gameObject.SetActive(false);
        gameoverCanvas.gameObject.SetActive(false);
        blackBG.gameObject.SetActive(false);

        //audioSource.clip = jumpscareSound;
    }

    public IEnumerator Jumpscare()
    {
        audioSource.clip = jumpscareSound;

        audioSource.volume = 0.75f;
        audioSource.PlayOneShot(jumpscareSound);

        gameManager.mainMusicPlayer.Stop();

        blackBG.gameObject.SetActive(true);
        jumpscareCanvas.gameObject.SetActive(true);
        gameoverCanvas.gameObject.SetActive(true);

        yield return new WaitForSeconds(jumpscareTimer);

        LeanTween.alphaCanvas(jumpscareCanvas, 0, 2f);
        LeanTween.alphaCanvas(gameoverCanvas, 1, 3f);

        yield return new WaitForSeconds(8f);

        SceneManager.LoadScene(0);

    }
   
}
