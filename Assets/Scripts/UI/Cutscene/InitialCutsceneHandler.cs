using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InitialCutsceneHandler : MonoBehaviour
{
    [SerializeField] private GameObject cutsceneCanvas;
    [SerializeField] private TextMeshProUGUI[] cutsceneText;
    [SerializeField] private float timerBetweenText;

    private void Start()
    {
        //LeanTween.alpha(cutsceneCanvas, 0, 0);
        foreach(TextMeshProUGUI text in cutsceneText)
        {
            text.gameObject.SetActive(false);
            //LeanTween.value(gameObject, updateValueExampleCallback, Color.white, Color.black, 0);
        }

        cutsceneCanvas.SetActive(false);
    }

    public void PlayIntroTextCutscene()
    {
        StartCoroutine(ShowIntroText());
    }

    private IEnumerator ShowIntroText()
    {

        cutsceneCanvas.SetActive(true);
        cutsceneCanvas.GetComponent<Animator>().Play("CSBG-FadeIn");

        yield return new WaitForSeconds(3f);

        // Display each text with a preset delay
        foreach (TextMeshProUGUI text in cutsceneText)
        {
            text.gameObject.SetActive(true);
            text.gameObject.GetComponent<Animator>().Play("CSText-FadeIn");
            yield return new WaitForSeconds(timerBetweenText);
        }

        // Start the game
        StartGame();
    }
    
    private void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
