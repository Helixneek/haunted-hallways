using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public GameObject player;
    public float speed;

    // Update is called once per frame
    void Update()
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
