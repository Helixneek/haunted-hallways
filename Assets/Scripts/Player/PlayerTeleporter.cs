using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField] private GameObject currentTeleporter;
    [SerializeField] private Animator fadeToBlack;

    void Update()
    {
        EnterRoom();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }

    private void EnterRoom()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTeleporter != null)
            {
                StartCoroutine(TeleportAfterFade());
                //Invoke("TeleportAfterFade", 0);
            }
        }
    }

    public IEnumerator TeleportAfterFade()
    {
        fadeToBlack.Play("fadeOut");

        Transform newPos = currentTeleporter.GetComponent<Teleporter01>().EnterRoom();
        Debug.Log(newPos);

        yield return new WaitForSeconds(0.2f);

        transform.position = newPos.position;

        yield return new WaitForSeconds(0.2f);

        fadeToBlack.Play("fadeIn");
    }
}

