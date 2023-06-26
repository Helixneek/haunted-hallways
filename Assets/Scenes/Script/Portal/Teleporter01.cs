using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Teleporter01 : MonoBehaviour
{
    [SerializeField] Transform posToGo;
    [SerializeField] private Transform destination;
    [SerializeField] GameObject keyTxt;

    private GameManager gameManager;
    private GameObject lockedText;
    private DoorChange doorChange;

    [Header("Set Door Destination Tag")]
    public bool isStairs;
    public bool isNormalDoor;
    public bool isPrincipalDoor;

    [Header("Set Door Destination Value")]
    public int nextFloor;
    public string nextRoomName;

    bool playerDetected;
    GameObject playerGO;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        doorChange = GetComponent<DoorChange>();
        lockedText = GameObject.Find("Popup Text Canvas/Door Locked Text");
    }

    public Transform GetDestination()
    {
        return destination;
    }

    void Start()
    {
        playerDetected = false;
        lockedText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            playerGO = collision.gameObject;
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
                gameManager.SoundAlert();

                if (isPrincipalDoor && gameManager.hasPrincipalKey)
                {
                    Debug.Log("principal room entered");
                    StartCoroutine(doorChange.OpenDoor());
                    gameManager.SetAstolfoRoom(nextRoomName); // This has to be changed specifically if its a door or stairs
                    playerDetected = false;
                    return posToGo;
                }

                else if (isNormalDoor)
                {
                    Debug.Log("normal room entered");
                    StartCoroutine(doorChange.OpenDoor());
                    gameManager.SetAstolfoRoom(nextRoomName); // This as well, and all subsequent ifs
                    playerDetected = false;
                    return posToGo;

                } else if (isStairs)
                {
                    Debug.Log("floor changed");
                    StartCoroutine(doorChange.OpenDoor());
                    gameManager.SetAstolfoFloor(nextFloor);
                    playerDetected = false;
                    return posToGo;
                }
                else {
                    // play the sound effect
                    // show display the ui text saying that you fucked up
                    StartCoroutine(LockedDoor());
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
            StartCoroutine(doorChange.OpenDoor());
            gameManager.SetGhostFloor(nextFloor);
            playerDetected = false;
            return posToGo;

        } else
        {
            Debug.Log("ghost moved rooms");
            StartCoroutine(doorChange.OpenDoor());
            gameManager.SetGhostRoom(nextRoomName);
            playerDetected = false;
            return posToGo;
        }
          
    }

    IEnumerator LockedDoor()
    {
        if(lockedText != null)
        {
            lockedText.SetActive(true);
            yield return new WaitForSeconds(1f);
            lockedText.SetActive(false);
        }   
    }
}
