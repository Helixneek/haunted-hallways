using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resumeButton : MonoBehaviour
{
    private void Start()
    {
        GameObject[] resumeButtons = GameObject.FindGameObjectsWithTag("Resume");

  
        foreach (GameObject button in resumeButtons)
        {
            Button resumeButton = button.GetComponent<Button>();
            if (resumeButton != null)
            {
                resumeButton.onClick.AddListener(UnpauseGame);
            }
        }
    }

    private void UnpauseGame()
    {
   
        Time.timeScale = 1f;
        Debug.Log("Game unpaused!");
    }
}