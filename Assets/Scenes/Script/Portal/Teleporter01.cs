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

    [Header("Set Door Destination Tag")]
    [SerializeField] private bool isStairs;
    [SerializeField] private bool isNormalDoor;
    [SerializeField] private bool isPrincipalDoor;

    [Header("Set Door Destination Value")]
    [SerializeField] private int nextFloor;
    [SerializeField] private string nextRoomName;

    bool playerDetected;
    GameObject playerGO;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
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
                    gameManager.SetAstolfoFloor(nextFloor); // This has to be changed specifically if its a door or stairs
                    playerDetected = false;
                    return posToGo;
                }

                else if (isNormalDoor)
                {
                    Debug.Log("normal room entered");
                    gameManager.SetAstolfoFloor(nextFloor); // This as well, and all subsequent ifs
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
