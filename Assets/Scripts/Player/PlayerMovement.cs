using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public enum PlayerGameState
{
    STANDING,
    HIDING,
    DEAD
}

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed; //player movement speed
    private float originalSpeed;
    private Rigidbody2D rbPlayer;
    private bool facingRight = true;
    private float moveDirection;
    private bool playerCurrentlyHiding = false;

    public Animator animator;


    //awake is called after all objects are initialized, called in a random order
    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>(); // bakak nyari component yang punya rigidbody2d

    }

    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        processingInpputsMovement();

        flippingPlayersDirection();
        playersMovement();
        AnimatorMovement();

    }

    private void AnimatorMovement()
    {
        animator.SetFloat("Speed", Mathf.Abs(moveDirection));
    }

    private void playersMovement()
    {
        //Movement
        rbPlayer.velocity = new Vector2(moveDirection * moveSpeed, rbPlayer.velocity.y); //buat movement character. 
    }

    private void flippingPlayersDirection()
    {
        //fliip Characters
        if (moveDirection > 0 && !facingRight)
        {
            playerDirection();
        }
        else if (moveDirection < 0 && facingRight)
        {
            playerDirection();
        }
    }

    private void processingInpputsMovement()
    {
        //get Inputs
        moveDirection = Input.GetAxis("Horizontal");  //scale = 1 = kanan, -1 = kiri
    }

    private void playerDirection()
    {
        facingRight = !facingRight; //di inverse 
        transform.Rotate(0f, 180f, 0f); // characternya biar dirotate 180 derajat biar balik ke kiri 
    }

    public void Freeze()
    {
        rbPlayer.velocity = Vector2.zero;
    }

    public void Unfreeze()
    {
        moveSpeed = originalSpeed;
    }

    public void SetHide()
    {
        playerCurrentlyHiding = true;
    }

    public void SetUnhide()
    {
        playerCurrentlyHiding = false;
    }

    public bool GetPlayerHideStatus()
    {
        return playerCurrentlyHiding;
    }
}