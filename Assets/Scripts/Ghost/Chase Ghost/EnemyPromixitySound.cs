using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPromixitySound : MonoBehaviour
{
    [SerializeField] private AudioClip proximitySound;
    [SerializeField] private float minimumDistance;
    [SerializeField] private AStarChase chase;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.clip = proximitySound;
    }

    private void Update()
    {
        //CalculateDistance();
    }

    private void CalculateDistance()
    {
        float distance = Vector2.Distance(transform.position, chase.player.transform.position);

        if (minimumDistance >= distance)
        {
            Debug.Log("player close enough for proximity sonud");
            audioSource.Play();
            
        }
        else
        {
            Debug.Log("player not close");
            audioSource.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"OnTriggerEnter2D called. other's tag was {collision.gameObject.tag}.");

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Playing proximity sound");
            audioSource.clip = proximitySound;
            audioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.Stop();
        }
    }
}
