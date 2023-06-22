using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Sprite openDoorSprite;
    public Sprite closedDoorSprite;
    public KeyCode interactKey = KeyCode.E;

    private SpriteRenderer doorSpriteRenderer;
    private bool doorOpen = false;

    private void Start()
    {
        doorSpriteRenderer = GetComponent<SpriteRenderer>();
        doorSpriteRenderer.sprite = closedDoorSprite; // Set the initial sprite to closed door
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (doorOpen)
                CloseDoor();
            else
                OpenDoor();
        }
    }

    public void OpenDoor()
    {
        doorSpriteRenderer.sprite = openDoorSprite;
        doorOpen = true;
    }

    public void CloseDoor()
    {
        doorSpriteRenderer.sprite = closedDoorSprite;
        doorOpen = false;
    }
}