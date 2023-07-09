using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MMContinue : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject greyContinueButton;
    [SerializeField] private GameObject night2ContinueButton;
    [SerializeField] private GameObject night3ContinueButton;
    [SerializeField] private CanvasGroup transitionCanvas;

    private string currentNight;
    private Action<int> OnContinueGame;

    public void LoadData(GameData data)
    {
        switch (data.currentNight)
        {
            case "Night1":
                this.currentNight = "Night1";
                greyContinueButton.SetActive(true);
                night2ContinueButton.SetActive(false);
                night3ContinueButton.SetActive(false);
                break;

            case "Night2":
                this.currentNight = "Night2";
                greyContinueButton.SetActive(false);
                night2ContinueButton.SetActive(true);
                night3ContinueButton.SetActive(false);
                break;

            case "Night3":
                this.currentNight = "Night3";
                greyContinueButton.SetActive(false);
                night2ContinueButton.SetActive(false);
                night3ContinueButton.SetActive(true);
                break;
        }
    }

    public void SaveData(ref GameData data)
    {
        Debug.Log("No save needed here");
    }

    private void Start()
    {
        transitionCanvas.alpha = 0;
        transitionCanvas.gameObject.SetActive(false);

        OnContinueGame += LoadScene;
    }

    public void ContinueGame(string night)
    {
        switch(night)
        {
            case "Night2":
                if(night == currentNight)
                {
                    LeanTween.alphaCanvas(transitionCanvas, 1, 2f).setOnComplete(() => OnContinueGame.Invoke(2));
                    //SceneManager.LoadScene(2);
                }
                break;

            case "Night3":
                if (night == currentNight)
                {
                    LeanTween.alphaCanvas(transitionCanvas, 1, 2f).setOnComplete(() => OnContinueGame.Invoke(3));
                    //SceneManager.LoadScene(3);
                }
                break;
        }
    }

    private void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
