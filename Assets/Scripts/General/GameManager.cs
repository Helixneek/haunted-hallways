using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum Night
{
    Night1, Night2, Night3
}

public class GameManager : MonoBehaviour, IDataPersistence
{
    // Keep track of Astolfo's position
    [Header("Astolfo")]
    [SerializeField] private int astolfoFloor;
    [SerializeField] private string astolfoRoom;
    [SerializeField] private int astolfoHealth;

    // Keep track of ghost's position
    [Header("Chaser Ghost")]
    [SerializeField] private GameObject ghostObject;
    [SerializeField] private int ghostFloor;
    [SerializeField] private string ghostRoom;

    // Keep track of keys owned
    [Header("Keys")]
    public bool hasPrincipalKey;
    public bool hasStorageKey;
    public bool hasSecurityKey;
    public bool hasBlueprint;
    public GameObject secretDoorObject;

    [Header("Nights")]
    // Night tracking
    public Night levelNumber;
    public bool hasSecurityFootage;
    public bool hasDocuments;
    public int amountofPagesOwned;

    [Header("Player Status")]
    public bool playerSoundMade;
    public bool currentlyHiding;

    public AudioSource mainMusicPlayer; 

    private AStarChase chaser;

    private void Awake()
    {
        chaser = ghostObject.GetComponent<AStarChase>();   
    }

    private void Start()
    {
        secretDoorObject.GetComponent<Teleporter01>().enabled = false;
    }

    private void Update()
    {
        if(hasBlueprint)
        {
            secretDoorObject.GetComponent<Teleporter01>().enabled = true;
        }
    }

    public void LoadData(GameData data)
    {
        switch(data.currentNight)
        {
            case "Night1":
                this.levelNumber = Night.Night1;
                break;

            case "Night2":
                this.levelNumber = Night.Night2;
                break;

            case "Night3":
                this.levelNumber = Night.Night3;
                break;
        }
    }

    public void SaveData(ref GameData data)
    {
        switch (this.levelNumber)
        {
            case Night.Night1:
                data.currentNight = "Night1";
                break;

            case Night.Night2:
                data.currentNight = "Night2";
                break;

            case Night.Night3:
                data.currentNight = "Night3";
                break;
        }
    }

    public void SoundAlert()
    {
        playerSoundMade = true;
        chaser.lastAstolfoFloor = astolfoFloor;
        chaser.lastAstolfoRoom = astolfoRoom;
        Debug.Log("uh oh sound made!");
    }

    // Setters for Astolfo
    public void SetAstolfoFloor(int floor)
    {
        astolfoFloor = floor;
    }

    public void SetAstolfoRoom(string name)
    {
        astolfoRoom = name;
    }

    public void SetAstolfoHealth(int health)
    {
        astolfoHealth = health;
    }

    // Getters for Astolfo
    public int GetAstolfoFloor()
    {
        return astolfoFloor;
    }

    public string GetAstolfoRoom()
    {
        return astolfoRoom;
    }

    public int GetAstolfoHealth()
    {
        return astolfoHealth;
    }

    // -------------------------------------------------------------------------
    // Setters for Ghost
    public void SetGhostFloor(int floor)
    {
        ghostFloor = floor;
    }

    public void SetGhostRoom(string name)
    {
        ghostRoom = name;
    }

    // Getters for Ghost
    public int GetGhostFloor()
    {
        return ghostFloor;
    }

    public string GetGhostRoom()
    {
        return ghostRoom;
    }

    public void AddPagesObtained()
    {
        amountofPagesOwned++;
    }

    public bool IsGoalComplete()
    {
        switch(levelNumber)
        {
            case Night.Night1:
                if (hasSecurityFootage)
                {
                    return true;
                } else
                {
                    return false;
                }

            case Night.Night2:
                if(hasDocuments)
                {
                    return true;
                } else
                {
                    return false;
                }

            case Night.Night3:
                if(amountofPagesOwned >= 7)
                {
                    return true;
                } else
                {
                    return false;
                }

            default:
                return false;

        }
    }
}
