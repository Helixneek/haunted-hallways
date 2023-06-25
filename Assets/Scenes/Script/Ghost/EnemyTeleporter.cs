using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleporter : MonoBehaviour
{
    private Teleporter01 currentTeleporter;
    private bool canTeleport = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter") && canTeleport)
        {
            currentTeleporter = collision.gameObject.GetComponent<Teleporter01>();
            StartCoroutine(TeleportNPCAfterDelay());
        }
    }

    private IEnumerator TeleportNPCAfterDelay()
    {
        canTeleport = false;
        yield return new WaitForSeconds(3f);

        if (currentTeleporter != null)
        {
            transform.position = currentTeleporter.GetComponent<Teleporter01>().EnterRoom().position;
        }

        canTeleport = true;
    }
}