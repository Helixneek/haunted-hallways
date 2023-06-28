using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Key : MonoBehaviour
{

    [SerializeField] private GameObject pickUPText;
    private GameManager gameManager; // To update the game manager


    [SerializeField] private bool ableToBePickedUp; // If key can be picked up

    [Header("Set Key Type")]
    [SerializeField] private bool isPrincipalKey; // Check this if this is the principal key
    [SerializeField] private bool isStorageKey; // Check this if this is the storage key
    [SerializeField] private bool isSecurityKey;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        pickUPText.gameObject.SetActive(false);
        
    }

    void Update()
    {
        if (ableToBePickedUp && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ( collision.gameObject.name.Equals("Astolfo"))
        {
            pickUPText.gameObject.SetActive(true);
            ableToBePickedUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.name.Equals("Player"))
        //{
            pickUPText.gameObject.SetActive(false);
            ableToBePickedUp = false;
        //}
    }


    private void PickUp()
    {
        if(ableToBePickedUp)
        {
            if (isPrincipalKey)
            {
                gameManager.hasPrincipalKey = true;
            }
            else if (isStorageKey)
            {
                gameManager.hasStorageKey = true;
            }

            Destroy(gameObject);
        }
    }
}
    