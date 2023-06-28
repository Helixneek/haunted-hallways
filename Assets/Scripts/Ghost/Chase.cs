using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    [Header("Objects Related")]
    public GameObject player;
    public EnemyTeleporter enemyTeleporter;
    public Teleporter01 currentTarget;

    [Header("Stats")]
    public float speed;
    public float chaseDistance = 10f;

    [Header("ETC")]
    public bool currentlyChasing = false;
    public int lastAstolfoFloor;
    public string lastAstolfoRoom;

    private GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        enemyTeleporter = GetComponent<EnemyTeleporter>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.playerSoundMade)
        {
            //lastAstolfoFloor = gm.GetAstolfoFloor();
            //lastAstolfoRoom = gm.GetAstolfoRoom();

            currentlyChasing = true;
            ChasePlayer();
        } else
        {
            currentlyChasing = false;
            Wander();
        }
    }

    public void Wander()
    {
        // Should we use this?
        // or should the ghost just stay still
        // Honestly it aint gonna be sitting still
        // Unless the player somehow camps it out
        // But im leaving it here just in case
    }

    // Ghost chases player when they are close enough, otherwise, run FindPlayer
    public void ChasePlayer()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (gm.GetGhostFloor() == gm.GetAstolfoFloor() && gm.GetGhostRoom() == gm.GetAstolfoRoom() && distance <= chaseDistance)
        {
            MoveTowards(player.transform);
        
        } else
        {
            FindPlayer();
        }
    }

    // Is used when finding the player's position and going there
    public void FindPlayer()
    {
        // Gets every teleporter in scene
        Teleporter01[] doors = FindObjectsOfType<Teleporter01>();

        // Checks if ghost is currently the same floor as source
        if(gm.GetGhostFloor() != lastAstolfoFloor)
        {
            foreach(Teleporter01 obj in doors)
            {
                // Checks if the teleporter is stairs and Astolfo is above the ghost
                if(obj.nextFloor <= lastAstolfoFloor && obj.nextFloor > gm.GetGhostFloor() && obj.isStairs)
                {
                    MoveTowards(obj.transform);
                    PrepareTeleport(obj.transform);

                // Checks if the teleporter is stairs and Astolfo is below the ghost
                } else if(obj.nextFloor >= lastAstolfoFloor && obj.nextFloor < gm.GetGhostFloor() && obj.isStairs)
                {
                    MoveTowards(obj.transform);
                    PrepareTeleport(obj.transform);
                }
            }
            //currentTarget = FindFirstObjectByType<Teleporter01>();
            //MoveTowards(currentTarget.transform);
        
        // Checks if ghost is in the same floor as Astolfo
        } else if(gm.GetGhostFloor() == lastAstolfoFloor)
        {
            foreach(Teleporter01 obj in doors)
            {
                if(obj.nextRoomName == lastAstolfoRoom)
                {
                    MoveTowards(obj.transform);
                    PrepareTeleport(obj.transform);
                }
            }

        // Checks if ghost is in a room that is not the halls and the Astolfo isn't in that room.
        // Basically, this is so the ghost can get out of a room
        } else if(gm.GetGhostRoom() != "hall" && gm.GetGhostRoom() != lastAstolfoRoom) 
        {
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
        Vector3 scale = transform.localScale;

        if (target.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        else
        {
            scale.x = Mathf.Abs(scale.x);
            transform.Translate(speed * Time.deltaTime * -1, 0, 0);
        }
        transform.localScale = scale;
    }

    public void PrepareTeleport(Transform target)
    {
        if (target.transform.position.x - 1.5f <= transform.position.x && transform.position.x <= target.transform.position.x + 1.5f)
        {
            enemyTeleporter.teleportIncoming = true;
        }

        //if(target.transform.position.x == transform.position.x)
        //{
        //    enemyTeleporter.teleportIncoming = true;
        //}
    }
}
