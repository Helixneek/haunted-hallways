using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Keep track of Astolfo's position
    [Header("Astolfo")]
    [SerializeField] private int astolfoFloor;
    [SerializeField] private string astolfoRoom;

    // Keep track of ghost's position
    [Header("Ghost")]
    [SerializeField] private int ghostFloor;
    [SerializeField] private string ghostRoom;

    // Keep track of keys owned
    public bool hasPrincipalKey;
    public bool hasStorageKey;
    public bool hasSecurityKey;

    // Player sound handling
    public bool playerSoundMade;

    private Chase chaser;

    private void Awake()
    {
        chaser = FindObjectOfType<Chase>().GetComponent<Chase>();
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

    // Getters for Astolfo
    public int GetAstolfoFloor()
    {
        return astolfoFloor;
    }

    public string GetAstolfoRoom()
    {
        return astolfoRoom;
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
}
