using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FrontDoor : MonoBehaviour
{
    [Header("Main Actors")]
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject ghostObject;

    [Header("Exit")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI denyTextObject;
    [SerializeField] private string denyText;
    [SerializeField] private string nextLevel;

    [Header("Cutscene")]
    [SerializeField] private CanvasGroup transitionCanvas;
    [SerializeField] private CanvasGroup loreCanvas;

    private bool playerDetected;

    private void Start()
    {
        denyTextObject.gameObject.SetActive(false);
        denyTextObject.text = denyText;
    }

    private void Update()
    {
        if(playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            GetOut();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }

    private void GetOut()
    {
        if (gameManager.IsGoalComplete())
        {
            Debug.Log("you did it!");

            //SceneManager.LoadScene(nextLevel);
            StartCoroutine(LevelComplete());
        } else
        {
            Debug.Log("goal not completed yet mothefucker!");
            StartCoroutine(ShowDenyText());
        }
    }

    private IEnumerator ShowDenyText()
    {
        denyTextObject.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        denyTextObject.gameObject.SetActive(false);
    }

    private IEnumerator LevelComplete()
    {
        LeanTween.alphaCanvas(transitionCanvas, 1, 1.5f);

        yield return new WaitForSeconds(2f);

        playerObject.GetComponent<Rigidbody2D>().isKinematic = true;
        ghostObject.GetComponent<Rigidbody2D>().isKinematic = true;

        LeanTween.alphaCanvas(loreCanvas, 1, 2f);

        yield return new WaitForSeconds(10f);

        LeanTween.alphaCanvas(loreCanvas, 0, 2f);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
