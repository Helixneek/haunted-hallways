using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerCollisionCheck : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.transform.tag == "Ghost")
        {
            PlayerHealthManagement.health--;
            if (PlayerHealthManagement.health <= 0)
            {
                Debug.Log("AAAA");
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(GetHurt());
            }
        }
    }

    IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(6, 7);
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        
    }
}

