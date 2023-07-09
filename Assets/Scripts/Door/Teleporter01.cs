using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Teleporter01 : MonoBehaviour
{
    [SerializeField] Transform posToGo;
    [SerializeField] private Transform destination;
    [SerializeField] private GameObject keyTxt;
    [SerializeField] private GameObject lockedText;

    private GameManager gameManager;
    private DoorChange doorChange;

    [Header("Set Door Destination Tag")]
    public bool isStairs;
    public bool isNormalDoor;
    public bool isPrincipalDoor;
    public bool isSecurityDoor;
    public bool isStorageDoor;
    public bool isSecretDoor;

    [Header("Set Door Destination Value")]
    public int currentFloor;
    public string currentRoomName;
    public int nextFloor;
    public string nextRoomName;

    bool playerDetected;
    private GameObject playerGO;
    private PlayerSFXHandler playerSFXHandler;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        doorChange = GetComponent<DoorChange>();
    }

    public Transform GetDestination()
    {
        return destination;
    }

    private void Start()
    {
        playerDetected = false;
        lockedText.SetActive(false);
        keyTxt.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            playerGO = collision.gameObject;
            playerSFXHandler = playerGO.GetComponent<PlayerSFXHandler>();

            keyTxt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
            keyTxt.SetActive(false);
        }
    }

    public Transform EnterRoom()
    {
        if (playerDetected)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                if (isPrincipalDoor && gameManager.hasPrincipalKey)
                {
                    Debug.Log("principal room entered");
                    StartCoroutine(doorChange.OpenDoor());
                    gameManager.SetAstolfoRoom(nextRoomName); // This has to be changed specifically if its a door or stairs
                    playerSFXHandler.PlayOpenDoorSound();

                    gameManager.SoundAlert();
                    playerDetected = false;
                    return posToGo;
                
                } else if (isSecurityDoor && gameManager.hasSecurityKey)
                {
                    Debug.Log("security room entered");
                    StartCoroutine(doorChange.OpenDoor());
                    gameManager.SetAstolfoRoom(nextRoomName); // This has to be changed specifically if its a door or stairs
                    playerSFXHandler.PlayOpenDoorSound();

                    gameManager.SoundAlert();
                    playerDetected = false;
                    return posToGo;
                
                } else if (isStorageDoor && gameManager.hasStorageKey)
                {
                    Debug.Log("storage room entered");
                    StartCoroutine(doorChange.OpenDoor());
                    gameManager.SetAstolfoRoom(nextRoomName); // This has to be changed specifically if its a door or stairs
                    playerSFXHandler.PlayOpenDoorSound();

                    gameManager.SoundAlert();
                    playerDetected = false;
                    return posToGo;
                
                } else if (isSecretDoor && gameManager.hasBlueprint)
                {
                    Debug.Log("secret room entered");
                    StartCoroutine(doorChange.OpenDoor());
                    gameManager.SetAstolfoRoom(nextRoomName); // This has to be changed specifically if its a door or stairs
                    playerSFXHandler.PlayOpenDoorSound();

                    gameManager.SoundAlert();
                    playerDetected = false;
                    return posToGo;
                }

                else if (isNormalDoor)
                {
                    Debug.Log("normal room entered");
                    StartCoroutine(doorChange.OpenDoor());
                    gameManager.SetAstolfoRoom(nextRoomName); // This as well, and all subsequent ifs
                    playerSFXHandler.PlayOpenDoorSound();

                    gameManager.SoundAlert();
                    playerDetected = false;
                    return posToGo;

                } else if (isStairs)
                {
                    Debug.Log("floor changed");
                    StartCoroutine(doorChange.OpenDoor());
                    gameManager.SetAstolfoFloor(nextFloor);
                    playerSFXHandler.PlayStairsSound();

                    gameManager.SoundAlert();
                    playerDetected = false;
                    return posToGo;
                }
                else {
                    // play the sound effect
                    // show display the ui text saying that you fucked up
                    StartCoroutine(LockedDoor());
                    // play locked door sfx here

                    return this.transform;
                }
            }
        }

        return this.transform;
    }

    public Transform GhostEnterRoom()
    {
        if(isStairs)
        {
            Debug.Log("ghost moved floors");
            //StartCoroutine(doorChange.OpenDoor());
            gameManager.SetGhostFloor(nextFloor);
            return posToGo;

        } else
        {
            Debug.Log("ghost moved rooms");
            //StartCoroutine(doorChange.OpenDoor());
            gameManager.SetGhostRoom(nextRoomName);
            return posToGo;
        }
          
    }

    IEnumerator LockedDoor()
    {
        if(lockedText != null && !isSecretDoor)
        {
            lockedText.SetActive(true);
            yield return new WaitForSeconds(1f);
            lockedText.SetActive(false);
        }   
    }
}
