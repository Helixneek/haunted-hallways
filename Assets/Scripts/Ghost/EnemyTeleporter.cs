using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleporter : MonoBehaviour
{
    [SerializeField] private GameObject currentTeleporter;
    private bool canTeleport = true;
    public bool teleportIncoming = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter") && canTeleport)
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

    private void Update()
    {
        if(teleportIncoming)
        {
            StartCoroutine(TeleportNPCAfterDelay());
        }
    }

    private IEnumerator TeleportNPCAfterDelay()
    {
        canTeleport = false;
        Transform newPos = currentTeleporter.GetComponent<Teleporter01>().GhostEnterRoom();

        yield return new WaitForSeconds(1f);

        if (currentTeleporter != null)
        {
            transform.position = newPos.position;

            Debug.Log("teleport succ");

        } else if(currentTeleporter == null)
        {
            Debug.Log("no teleporter found");
        }

        yield return new WaitForSeconds(1f);

        canTeleport = true;
        teleportIncoming = false;
    }
}