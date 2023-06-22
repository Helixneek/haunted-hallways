using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorChange : MonoBehaviour
{
    public Sprite openDoorSprite;
    public Sprite closedDoorSprite;

    public GameObject player;
    private bool playerSprite;

    private SpriteRenderer doorSpriteRenderer;

    private void Start()
    {
        doorSpriteRenderer = GetComponent<SpriteRenderer>();
        doorSpriteRenderer.sprite = closedDoorSprite; // Set the initial sprite to closed door
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            playerSprite = true;
            UpdateDoorSprite();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            playerSprite = false;
            UpdateDoorSprite();
        }
    }

    private void UpdateDoorSprite()
    {
        if (playerSprite)
        {
            doorSpriteRenderer.sprite = openDoorSprite;
        }
        else
        {
            doorSpriteRenderer.sprite = closedDoorSprite;
        }
    }
}