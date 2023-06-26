using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChange : MonoBehaviour
{
    public Sprite openDoorSprite;
    public Sprite closedDoorSprite;

    public GameObject player;
    private bool playerSprite;

    public SpriteRenderer doorSpriteRenderer;

    private void Start()
    {
        //doorSpriteRenderer = GameObject.Find("Door Sprite").GetComponent<SpriteRenderer>();
        //if(doorSpriteRenderer == null)
        //{
        //    Debug.Log("sprite redner not found");
        //}
        doorSpriteRenderer.sprite = closedDoorSprite; // Set the initial sprite to closed door
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            playerSprite = true;
            //UpdateDoorSprite();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            playerSprite = false;
            //UpdateDoorSprite();
        }
    }

    private void UpdateDoorSprite()
    {
        if (playerSprite)
        {
            doorSpriteRenderer.sprite = openDoorSprite;
            doorSpriteRenderer.sprite = closedDoorSprite;
        }
    }

    public IEnumerator OpenDoor()
    {
        if(playerSprite)
        {
            doorSpriteRenderer.sprite = openDoorSprite;
            yield return new WaitForSeconds(0.5f);
            doorSpriteRenderer.sprite = closedDoorSprite;
        }
    }
}