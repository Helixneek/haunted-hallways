using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemyAttack : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Teleporter01[] potentialTeleportSpots;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private VideoPlayerController videoPlayerController;

    public float stunDuration = 2f;

    private AStarChase chase;
    private PlayerDeathSetter deathSetter;
    private EnemyPromixitySound promixitySound;
    private EnemyJumpscare jumpscare;
    [SerializeField] private HideableObject hideableObject;

    private void Awake()
    {
        jumpscare = GetComponent<EnemyJumpscare>();
        chase = GetComponent<AStarChase>();
        promixitySound = GetComponent<EnemyPromixitySound>();
        deathSetter = chase.player.GetComponent<PlayerDeathSetter>();
    }

    private void Start()
    {
        //videoPlayerController.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerStunHandler player = collider.GetComponent<PlayerStunHandler>();
            if (player != null)
            {
                if(chase.player.GetComponent<PlayerMovement>().GetPlayerHideStatus())
                {
                    if (chase.sawPlayerHide && chase.hideableObject.GetComponent<HideableObject>().GetPlayerHidingStatus())
                    {
                        player.Stun(stunDuration);
                        DamagePlayer(collider);

                        TeleportAfterAttack();

                        Debug.Log("currently attacking");

                    }
                    else if (!chase.sawPlayerHide)
                    {
                        Debug.Log("Ghost did not see player hide");

                        if(chase.hideableObject.GetComponent<HideableObject>().GetPlayerHidingStatus())
                        {
                            // ghost does nothing
                            Debug.Log("ignoring player");
                        }
                        

                    }

                } else
                {
                    player.Stun(stunDuration);
                    DamagePlayer(collider);

                    TeleportAfterAttack();

                    Debug.Log("currently attacking");
                } 
            }
        }
    }

    private void DamagePlayer(Collider2D collision)
    {
        PlayerHealthManagement.health--;
        gameManager.SetAstolfoHealth(PlayerHealthManagement.health);

        // Die
        if (gameManager.GetAstolfoHealth() <= 0)
        {
            // Jumpscare
            Debug.Log("Astolfo has died");

            deathSetter.DisableComponents();
            promixitySound.enabled = false;
            chase.enabled = false;

            StartCoroutine(jumpscare.Jumpscare());
            // Not dead
        } else
        {
            videoPlayerController.gameObject.SetActive(true);
            StartCoroutine(videoPlayerController.PlayVideoAndDisappear());
        }
        
    }

    private void TeleportAfterAttack()
    {
        Debug.Log("GHOST: Teleport away after attack");
        int randomValue = Random.Range(0, potentialTeleportSpots.Length);

            Debug.Log("GHOST: Start teleport calculation");

            if (potentialTeleportSpots[randomValue].currentFloor != gameManager.GetAstolfoFloor())
            {
                Transform newPos = potentialTeleportSpots[randomValue].GhostEnterRoom();

                if(potentialTeleportSpots[randomValue] != null)
                {
                    Debug.Log("GHOST: Succesfully teleported to " + newPos);
                    transform.position = newPos.position;
                }
            }
            else
            {
                Debug.Log("GHOST: Same floor detected. Looking for new door");
                TeleportAfterAttack();
            }
    }

    public Teleporter01[] GetTeleportSpots()
    {
        return potentialTeleportSpots;
    }
}
