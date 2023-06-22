using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincipalDoorSettings : MonoBehaviour
{
    [SerializeField] private Transform posToGo;
    [SerializeField] private Transform destination;
    [SerializeField] private GameObject keyTxt;

    private GameManager gameManager;
    private bool playerDetected;
    private GameObject playerGO;

    public Transform GetDestination()
    {
        return destination;
    }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        playerDetected = false;
    }

    void Update()
    {
        if (playerDetected && gameManager.hasPrincipalKey)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerGO.transform.position = posToGo.position;
                playerDetected = false;
            }
        }
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
}