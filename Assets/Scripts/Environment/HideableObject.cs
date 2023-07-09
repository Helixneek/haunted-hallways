using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HideableObject : MonoBehaviour
{
    [Header("Manually Assigned")]
    [SerializeField] private TextMeshProUGUI hideTextObject;
    [SerializeField] private string hideText;
    [SerializeField] private AudioClip startHideSound;
    [SerializeField] private AudioClip stopHideSound;
    [SerializeField] private GameObject hideCanvas;
    [SerializeField] private AStarChase enemyObject;

    [Header("Automatically Assigned")]
    [SerializeField] private GameObject player;
    private SpriteRenderer playerSprite;
    private PlayerMovement playerInput;
    private AudioSource audioSource;

    [SerializeField] private bool playerDetected = false;
    [SerializeField] private bool canHide = true;
    [SerializeField] private bool currentlyHiding = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        hideCanvas.gameObject.SetActive(false);

        hideTextObject.gameObject.SetActive(false);
        hideTextObject.text = hideText;
    }

    private void Update()
    {
        if (playerDetected && canHide && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Gonna hide now");
            HidePlayer();

        } else if(currentlyHiding && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(RevealPlayer());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            hideTextObject.gameObject.SetActive(true);
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
            hideTextObject.gameObject.SetActive(false);
            playerDetected = false;
        }
    }

    private void HidePlayer()
    {
        // Hide the player sprite
        playerSprite = player.GetComponent<SpriteRenderer>();

        if (playerSprite != null)
        {
            playerSprite.enabled = false;

        } else
        {
            Debug.Log("Player sprite renderer not found");
        }

        // Disable player controls or other relevant behaviors
        playerInput = player.GetComponent<PlayerMovement>();

        if (playerInput != null)
        {
            playerInput.Freeze();
            playerInput.SetHide();
            playerInput.enabled = false;

        }
        else
        {
            Debug.Log("Player movement not found");
        }

        // Play sound
        audioSource.clip = startHideSound;
        audioSource.Play();

        // Show black screen
        hideCanvas.gameObject.SetActive(true);

        // Notify ghost that player is hiding
        EnablePlayerHideFlag();

        canHide = false; // Prevent hiding again immediately
        currentlyHiding = true; // To be able to leave
    }

    private IEnumerator RevealPlayer()
    {
        // Show player and re-enable movement
        playerSprite.enabled = true;
        playerInput.enabled = true;
        playerInput.SetUnhide();

        // Play sound
        audioSource.clip = stopHideSound;
        audioSource.Play();

        // Hide black screen
        hideCanvas.gameObject.SetActive(false);

        yield return new WaitForSeconds(1);

        canHide = true;
        currentlyHiding = false;
    }

    // If ghost detects player hiding
    public void ForcefullyOpen()
    {
        playerSprite.enabled = true;
        playerInput.enabled = true;

        hideCanvas.gameObject.SetActive(false);

        audioSource.clip = stopHideSound;
        audioSource.Play();
    }

    private void EnablePlayerHideFlag()
    {
        enemyObject.SeenPlayerHide();
    }

    public bool GetPlayerHidingStatus()
    {
        Debug.Log("Player hiding: " + currentlyHiding);
        return currentlyHiding;
    }
}
