using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Teleporter01[] potentialTeleportSpots;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private VideoPlayerController videoPlayerController;

    public float stunDuration = 2f;

    private void Start()
    {
        videoPlayerController.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerStunHandler player = collider.GetComponent<PlayerStunHandler>();
            if (player != null)
            {
                player.Stun(stunDuration);

                videoPlayerController.gameObject.SetActive(true);
                StartCoroutine(videoPlayerController.PlayVideoAndDisappear());
            }
        }
    }

    private void TeleportAfterAttack()
    {
        EnemyTeleporter enemyTeleporter = GetComponent<EnemyTeleporter>();

        int randomValue = Random.Range(0, potentialTeleportSpots.Length);

        while (potentialTeleportSpots[randomValue].nextFloor != gameManager.GetAstolfoFloor())
        {
            if (potentialTeleportSpots[randomValue].nextFloor != gameManager.GetAstolfoFloor())
            {
                enemyTeleporter.Teleport(potentialTeleportSpots[randomValue]);
            }
            else
            {
                randomValue = Random.Range(0, potentialTeleportSpots.Length);
            }
        }
    }
}
