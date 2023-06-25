using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public bool currentlyChasing = false;

    private GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.playerSoundMade)
        {
            currentlyChasing = true;
            ChasePlayer();
        } else
        {
            currentlyChasing = false;
            Wander();
        }
    }

    public void Wander()
    {

    }

    public void ChasePlayer()
    {
        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        else
        {
            scale.x = Mathf.Abs(scale.x);
            transform.Translate(speed * Time.deltaTime * -1, 0, 0);
        }



        transform.localScale = scale;
    }
}
