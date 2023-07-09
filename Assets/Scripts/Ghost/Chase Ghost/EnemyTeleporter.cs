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
            TeleportNPCAfterDelay();
        }
    }

    private void TeleportNPCAfterDelay()
    {
        canTeleport = false;
        Transform newPos = currentTeleporter.GetComponent<Teleporter01>().GhostEnterRoom();
        //Debug.Log("Ghost NEW POS: " + newPos);

        if (currentTeleporter != null)
        {
            transform.position = newPos.position;
            teleportIncoming = false;

            //Debug.Log("Ghost Teleport Success");

        } else if(currentTeleporter == null)
        {
            //Debug.Log("No teleporter found");
        }

        canTeleport = true;
    }

    public void Teleport(GameObject destination)
    {
        Transform newPos = destination.GetComponent<Teleporter01>().GhostEnterRoom();

        if (destination != null)
        {
            transform.position = newPos.position;

            Debug.Log("GHOST: Teleport");

        }
        else if (destination == null)
        {
            Debug.Log("GHOST: Did not teleport");
        }
    }
}