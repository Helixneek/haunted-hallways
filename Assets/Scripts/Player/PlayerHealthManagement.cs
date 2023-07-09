using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthManagement : MonoBehaviour
{
    public static int health = 3;

    public Image[] hearts;

    public Sprite[] emptyHeartSprites;

    public Sprite[] fullHeartSprites;

    private void Start()
    {
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        //foreach(Image img in hearts)
        //{
        //    img.sprite = emptyHeart;
        //}

        for(int i = 0; i < 3; i++)
        {
            hearts[i].sprite = emptyHeartSprites[i];
        }

        for (int i = 0; i < health; i++) 
        {
            hearts[i].sprite = fullHeartSprites[i];
        }
    }
}
