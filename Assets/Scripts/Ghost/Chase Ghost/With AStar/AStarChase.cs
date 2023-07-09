using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AStarChase : MonoBehaviour
{
    [Header("Objects Related")]
    public GameObject player;
    public GameObject hideableObject;
    public Teleporter01 detectedTeleporter;

    [Header("Chase")]
    public float moveSpeed;
    public float detectionDistance = 10f;

    [Header("Wander")]
    public float wanderTimer = 3f;
    public float wanderRadius = 5f;

    [Header("ETC")]
    public bool currentlyChasing = false;
    public bool currentlyDistracted = false;
    public bool sawPlayerHide = false;
    public int lastAstolfoFloor;
    public string lastAstolfoRoom;

    private Teleporter01[] potentialTeleportSpots;
    private GameManager gm;
    private Transform ghostTransform;
    private AIDestinationSetter destinationSetter;
    private AIPath aiPath;
    private EnemyTeleporter enemyTeleporter;

    private float distanceFromPlayer;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        ghostTransform = GetComponent<Transform>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        enemyTeleporter = GetComponent<EnemyTeleporter>();
        aiPath = GetComponent<AIPath>();
    }

    private void Start()
    {
        aiPath.canMove = false;
        aiPath.maxSpeed = moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hideable"))
        {
            hideableObject = collision.gameObject;
        }

        if (collision.CompareTag("Teleporter"))
        {
            detectedTeleporter = collision.gameObject.GetComponent<Teleporter01>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hideable"))
        {
            hideableObject = null;
        }

        if (collision.CompareTag("Teleporter"))
        {
            detectedTeleporter = null;
        }
    }

    private void Update()
    {
        IncreaseSpeedFromPages();

        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (gm.playerSoundMade)
        {
            currentlyChasing = true;
            aiPath.canMove = true;
            ChasePlayer();
        }
        else
        {
            currentlyChasing = false;
            //Wander();
        }
    }

    private void ChasePlayer()
    {
        // Gets every teleporter in scene
        Teleporter01[] doors = FindObjectsOfType<Teleporter01>();

        // Ghost isnt on the same floor as Astolfo
        if (gm.GetGhostFloor() != lastAstolfoFloor && gm.GetGhostFloor() != gm.GetAstolfoFloor())
        {
            Debug.Log("CHASE: Ghost is not on the same floor as Astolfo");

            foreach (Teleporter01 obj in doors)
            {
                // Checks if the teleporter is stairs and Astolfo is above the ghost
                if (obj.nextFloor <= lastAstolfoFloor && obj.nextFloor > gm.GetGhostFloor() && obj.isStairs)
                {
                    Debug.Log("CHASE: Ghost is below Astolfo");
                    destinationSetter.target = obj.transform;
                    PrepareTeleport(obj.transform);

                    // Checks if the teleporter is stairs and Astolfo is below the ghost
                }
                else if (obj.nextFloor >= lastAstolfoFloor && obj.nextFloor < gm.GetGhostFloor() && obj.isStairs)
                {
                    Debug.Log("CHASE: Ghost is above Astolfo");
                    destinationSetter.target = obj.transform;
                    PrepareTeleport(obj.transform);
                }
            }

            
        }
        // Ghost is on the same floor as Astolfo
        else if (gm.GetGhostFloor() == gm.GetAstolfoFloor())
        {
            Debug.Log("CHASE: Ghost is on the same floor as Astolfo");
            destinationSetter.target = player.transform;

            foreach(Teleporter01 obj in doors)
            {
                // Look for room with Astolfo
                // If Astolfo is in a different room
                if (gm.GetGhostRoom() != lastAstolfoRoom)
                {
                    Debug.Log("CHASE: Ghost isnt in the same room as Astolfo");
                    if (obj.nextRoomName == lastAstolfoRoom && !obj.isStairs) 
                    {
                        Debug.Log("CHASE: Ghost heading towards room with Astolfo");
                        destinationSetter.target = obj.transform;
                        PrepareTeleport(obj.transform);
                    }
                // Astolfo is in the same room
                } else
                {
                    Debug.Log("CHASE: Ghost is in the same room as Astolfo");
                    destinationSetter.target = player.transform;
                }
            }

            // Ghost saw the player hide
            if (sawPlayerHide && gm.currentlyHiding)
            {
                Debug.Log("CHASE: Ghost saw player hide");
                destinationSetter.target = player.transform;
                hideableObject.GetComponent<HideableObject>().ForcefullyOpen();
            }
            else if (!sawPlayerHide && gm.currentlyHiding)
            {
                Debug.Log("CHASE: Ghost didnt saw player hide");
                destinationSetter.target = player.transform;
                if(HasReachedTarget(player.transform))
                {
                    TeleportAfterReachHide();
                }
            }


            // Checks if ghost is in a room that is not the halls and the Astolfo isn't in that room.
            // Basically, this is so the ghost can get out of a room
        }
        else if (gm.GetGhostRoom() != "hall" && gm.GetGhostRoom() != lastAstolfoRoom)
        {
            Debug.Log("CHASE: Ghost is in a room");
            foreach (Teleporter01 obj in doors)
            {
                if (obj.nextRoomName == "hall")
                {
                    destinationSetter.target = obj.transform;
                    PrepareTeleport(obj.transform);
                }
            }
        }
    }

    private void Wander()
    {
        Debug.Log("GHOST: Start find target other than player");

        gm.playerSoundMade = false;
        Teleporter01[] potentialTargets = FindObjectsOfType<Teleporter01>();
        int randomValue = Random.Range(0, potentialTargets.Length);

        while (gm.GetGhostFloor() == gm.GetAstolfoFloor() || gm.GetGhostRoom() == gm.GetAstolfoRoom())
        {
            // Find random stairs
            if(potentialTargets[randomValue].currentFloor == gm.GetAstolfoFloor() &&
                potentialTargets[randomValue].nextFloor != gm.GetAstolfoFloor())
            {
                Debug.Log("GHOST: New stair target found");
                destinationSetter.target = potentialTargets[randomValue].transform;
                PrepareTeleport(potentialTargets[randomValue].transform);
            }

            // Find random stairs
            if(potentialTargets[randomValue].currentRoomName == gm.GetAstolfoRoom() && 
                potentialTargets[randomValue].nextRoomName != gm.GetAstolfoRoom()) {

                Debug.Log("GHOST: New door target found");
                destinationSetter.target = potentialTargets[randomValue].transform;
                PrepareTeleport(potentialTargets[randomValue].transform);
            }
        }
    }

    public void PrepareTeleport(Transform target)
    {
        // Ghost is pretty close to the target, but not in the middle of the collider
        if (HasReachedTarget(target))
        {
            enemyTeleporter.teleportIncoming = true;
        }
    }

    public bool HasReachedTarget(Transform target)
    {
        if (target.transform.position.x - 2f <= ghostTransform.position.x && ghostTransform.position.x <= target.transform.position.x + 2f)
        {
            return true;
        }

        return false;
    }

    public void SeenPlayerHide()
    {
        if (distanceFromPlayer >= detectionDistance)
        {
            sawPlayerHide = false;
        }
        else
        {
            sawPlayerHide = true;
        }
    }

    private void TeleportAfterReachHide()
    {
        Debug.Log("GHOST: Teleport away after hiding");
        int randomValue = Random.Range(0, potentialTeleportSpots.Length);

        Debug.Log("GHOST: Start teleport after hide calculation");

        if (potentialTeleportSpots[randomValue].currentFloor != gm.GetAstolfoFloor())
        {
            Transform newPos = potentialTeleportSpots[randomValue].GhostEnterRoom();

            if (potentialTeleportSpots[randomValue] != null)
            {
                Debug.Log("GHOST: Succesfully teleported to " + newPos);
                transform.position = newPos.position;
            }
        }
        else
        {
            Debug.Log("GHOST: Same floor detected. Looking for new door");
            TeleportAfterReachHide();
        }
    }

    private void IncreaseSpeedFromPages()
    {
        if(gm.levelNumber == Night.Night3)
        {
            aiPath.maxSpeed = moveSpeed + (gm.amountofPagesOwned * .5f);
        }
    }
}
