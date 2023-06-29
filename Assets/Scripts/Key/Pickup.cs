using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum PickupType
{
    PrincipalKey, SecurityKey, StorageKey, JournalPage, Blueprint
} 

public class Pickup : MonoBehaviour
{
    [Header("Basic")]
    [SerializeField] private PickupType pickup;
    [SerializeField] private TextMeshProUGUI pickUpTextObject;
    [SerializeField] private string pickUpText;
    [SerializeField] private bool ableToBePickedUp; // If key can be picked up
    [SerializeField] private GameManager gameManager; // To update the game manager

    [Header("UI when Picked Up")]
    [SerializeField] private Sprite pickupImage;
    [SerializeField] private GameObject pickupNotification;

    //private Image pickupBG;
    private Image pickupIcon;
    private TextMeshProUGUI pickupText;
    private Animator pickupNotificationAnimator;
    private SpriteRenderer itemSprite;
    

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        //pickupBG = pickupNotification.transform.GetChild(0).GetComponent<Image>();
        pickupIcon = pickupNotification.transform.GetChild(1).GetComponent<Image>();
        pickupText = pickupNotification.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        pickupNotificationAnimator = pickupNotification.GetComponent<Animator>();

        itemSprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        pickUpTextObject.gameObject.SetActive(false);
        pickUpTextObject.text = pickUpText;

        pickupNotification.SetActive(false);

        //pickupIcon.gameObject.SetActive(false);
        //pickupText.gameObject.SetActive(false);
        //pickupBG.gameObject.SetActive(false);
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

        if (collision.gameObject.name.Equals("Astolfo"))
        {
            pickUpTextObject.gameObject.SetActive(true);
            ableToBePickedUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pickUpTextObject.gameObject.SetActive(false);
        ableToBePickedUp = false;
    }


    private void PickUp()
    {
        if (ableToBePickedUp)
        {
            switch(pickup)
            {
                case PickupType.PrincipalKey: // Might need to add more, probably the UI appearing
                    gameManager.hasPrincipalKey = true;
                    pickupText.text = "Principal key obtained";
                    break;

                case PickupType.StorageKey:
                    gameManager.hasStorageKey = true;
                    pickupText.text = "Storage key obtained";
                    break;

                case PickupType.SecurityKey:
                    gameManager.hasSecurityKey = true;
                    pickupText.text = "Security key obtained";
                    break;

                case PickupType.Blueprint:
                    gameManager.hasBlueprint = true;
                    pickupText.text = "Blueprint obtained";
                    break;

                case PickupType.JournalPage: 
                    gameManager.amountofPagesOwned++;
                    pickupText.text = "Journal page obtained";
                    break;
            }

            pickupIcon.sprite = pickupImage;

            StartCoroutine(ShowNotification());
        }
    }

    private IEnumerator ShowNotification()
    {
        pickupNotification.SetActive(true);
        pickupNotificationAnimator.Play("PickUp Fade In");

        pickUpTextObject.enabled = false;
        itemSprite.enabled = false;

        Debug.Log("notification boutta close");

        yield return new WaitForSeconds(3f);

        Debug.Log("notification closing now");

        pickupNotificationAnimator.Play("PickUp Fade Out");

        yield return new WaitForSeconds(0.2f);
        pickupNotification.SetActive(false);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("destroyed");
    }
}
