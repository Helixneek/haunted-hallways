using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincipalDoorSettings : MonoBehaviour
{
    [SerializeField] private Transform posToGo;
    [SerializeField] private Transform destination;
    [SerializeField] private GameObject keyTxt;

    private bool playerDetected;
    private bool keyTaken;
    private GameObject playerGO;

    public Transform GetDestination()
    {
        return destination;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerDetected = false;
        keyTaken = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected && keyTaken)
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

    public void SetKeyTaken(bool taken)
    {
        keyTaken = taken;
    }
}