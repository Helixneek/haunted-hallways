using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonChanges : MonoBehaviour
{
    public void SceneChanges(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}