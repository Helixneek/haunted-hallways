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

    public void SoundAlert()
    {
        if(playerSoundMade)
        {
            Debug.Log("uh oh sound made!");
        }
    }

    // Setters
    public void SetAstolfoFloor(int floor)
    {
        astolfoFloor = floor;
    }

    public void SetAstolfoRoom(string name)
    {
        astolfoRoom = name;
    }

    // Getters
    public int GetAstolfoFloor()
    {
        return astolfoFloor;
    }

    public string GetAstolfoRoom()
    {
        return astolfoRoom;
    }
}
