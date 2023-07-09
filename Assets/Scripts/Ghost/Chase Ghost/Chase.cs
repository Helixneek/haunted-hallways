using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Random = UnityEngine.Random;

public class Chase : MonoBehaviour
{
    [Header("Objects Related")]
    public GameObject player;
    public EnemyTeleporter enemyTeleporter;
    public Teleporter01 currentTarget;
    public GameObject hideableObject;

    [Header("Chase")]
    public float speed;
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

    private GameManager gm;
    private AIPath aiPath;
    private Transform ghostTransform;
    private float distanceFromPlayer;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        ghostTransform = GetComponent<Transform>();
        enemyTeleporter = GetComponent<EnemyTeleporter>();
    }

    private void Start()
    {
        aiPath.canMove = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hideable"))
        {
            hideableObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hideable"))
        {
            hideableObject = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (gm.playerSoundMade)
        {
            currentlyChasing = true;
            aiPath.canMove = true;
            ChasePlayer();
        } else
        {
            currentlyChasing = false;
            Wander();
        }
        
    }

    public void Wander()
    {

    }

    // Ghost chases player when they are close enough, otherwise, run FindPlayer
    public void ChasePlayer()
    {
        if (gm.GetGhostFloor() == gm.GetAstolfoFloor() && gm.GetGhostRoom() == gm.GetAstolfoRoom())
        {
            MoveTowards(player.transform);
        
        } else if(currentlyDistracted)
        {
            GetDistracted();

        } else if(!currentlyDistracted)
        {
            FindPlayer();
        }
    }

    // Is used when finding the player's position and going there
    public void FindPlayer()
    {
        // Gets every teleporter in scene
        Teleporter01[] doors = FindObjectsOfType<Teleporter01>();

        // Ghost isnt on the same floor as Astolfo
        if(gm.GetGhostFloor() != lastAstolfoFloor && gm.GetGhostFloor() != gm.GetAstolfoFloor())
        {
            Debug.Log("CHASE: Ghost is not on the same floor as Astolfo");

            foreach(Teleporter01 obj in doors)
            {
                // Checks if the teleporter is stairs and Astolfo is above the ghost
                if(obj.nextFloor <= lastAstolfoFloor && obj.nextFloor > gm.GetGhostFloor() && obj.isStairs)
                {
                    Debug.Log("CHASE: Ghost is below Astolfo");
                    MoveTowards(obj.transform);
                    PrepareTeleport(obj.transform);

                // Checks if the teleporter is stairs and Astolfo is below the ghost
                } else if(obj.nextFloor >= lastAstolfoFloor && obj.nextFloor < gm.GetGhostFloor() && obj.isStairs)
                {
                    Debug.Log("CHASE: Ghost is above Astolfo");
                    MoveTowards(obj.transform);
                    PrepareTeleport(obj.transform);
                }
            }
        
        // Ghost is on the same floor as Astolfo
        } else if(gm.GetGhostFloor() != lastAstolfoFloor && gm.GetGhostFloor() == gm.GetAstolfoFloor())
        {
            Debug.Log("CHASE: Ghost is on the same floor as Astolfo");

            // Ghost saw the player hide
            if(sawPlayerHide)
            {
                Debug.Log("CHASE: Ghost saw player hide");
                MoveTowards(player.transform);
                ForcefullyUnhide(player.transform);

            // Ghost didnt saw the player hide
            } else if(!sawPlayerHide)
            {
                Debug.Log("CHASE: Ghost didnt saw player hide");
                int randomIndex = Random.Range(0, doors.Length);
                MoveTowards(FindDoor(doors[randomIndex].nextFloor, doors[randomIndex].nextRoomName).transform);
                PrepareTeleport(FindDoor(doors[randomIndex].nextFloor, doors[randomIndex].nextRoomName).transform);
            }
            

        // Checks if ghost is in a room that is not the halls and the Astolfo isn't in that room.
        // Basically, this is so the ghost can get out of a room
        } else if(gm.GetGhostRoom() != "hall" && gm.GetGhostRoom() != lastAstolfoRoom) 
        {
            Debug.Log("CHASE: Ghost is in a room");
            foreach (Teleporter01 obj in doors)
            {
                if (obj.nextRoomName == "hall")
                {
                    MoveTowards(obj.transform);
                    PrepareTeleport(obj.transform);
                }
            }
        } 
        
    }

    public void MoveTowards(Transform target)
    {
        Vector3 scale = ghostTransform.localScale;

        if (target.transform.position.x > ghostTransform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
            ghostTransform.Translate(speed * Time.deltaTime, 0, 0);
        }

        else
        {
            scale.x = Mathf.Abs(scale.x);
            ghostTransform.Translate(speed * Time.deltaTime * -1, 0, 0);
        }
        transform.localScale = scale;
    }

    public void PrepareTeleport(Transform target)
    {
        // Ghost is pretty close to the target, but not in the middle of the collider
        if (HasReachedTarget(target))
        {
            enemyTeleporter.teleportIncoming = true;
        }
    }

    private void ForcefullyUnhide(Transform player)
    {
        HideableObject obj = hideableObject.GetComponent<HideableObject>();

        // Ghost is pretty close to the target, but not in the middle of the collider
        if (HasReachedTarget(player))
        {
            obj.ForcefullyOpen();
        }
    }

    // Check if ghost is around the target, but not exactly (some position shit causes them to be offset so this is to deal with that)
    public bool HasReachedTarget(Transform target)
    {
        if (target.transform.position.x - 1.5f <= ghostTransform.position.x && ghostTransform.position.x <= target.transform.position.x + 1.5f)
        {
            return true;
        }

        return false;
    }

    public void GetDistracted()
    {
        FindPlayer();

        // TODO - find the object making noise and head there
        currentlyDistracted = false;
    }

    public Teleporter01 FindDoor(int floor, string name)
    {
        Teleporter01[] doors = FindObjectsOfType<Teleporter01>();
        Teleporter01 defaultChoice = doors[0];

        foreach(Teleporter01 obj in doors)
        {
            if(obj.nextFloor == floor && obj.nextRoomName == name)
            {
                return obj;
            }
        }

        return defaultChoice;
    }

    public void SeenPlayerHide()
    {
        if(distanceFromPlayer >= detectionDistance)
        {
            sawPlayerHide = false;
        } else
        {
            sawPlayerHide = true;
        }
    }
}
