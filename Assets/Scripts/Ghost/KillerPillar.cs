using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillerPillar : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealthManagement.health--;
            if (PlayerHealthManagement.health <= 0)
            {
                collision.gameObject.SetActive(false);
                SceneChanges(sceneName);
            }
            else
            {
                // Play hurt animation/sound or apply any other desired effects
            }
        }
    }

    public void SceneChanges(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
