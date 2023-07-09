using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class PlayerFlickeringFlashlight : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Light2D flashlight;
    [SerializeField] private GameObject ghostObject;

    [Header("Values")]
    [SerializeField] private float minimumDistance;
    [SerializeField] private float maximumIntensity;
    [SerializeField] private float minimumIntensity;
    [SerializeField] private float flickerInterval = 0.1f;

    [SerializeField] private bool isFlickering = false;

    private void Update()
    {
        CalculateDistance();
        StartCoroutine(FlickerFlashlight());
    }

    private void CalculateDistance()
    {
        float distance = Vector2.Distance(transform.position, ghostObject.transform.position);

        if(minimumDistance >= distance)
        {
            isFlickering = true;
        } else
        {
            flashlight.intensity = maximumIntensity;
            isFlickering = false;
        }
    }

    private IEnumerator FlickerFlashlight()
    {
        while (isFlickering)
        {
            float randomValue = Random.Range(0f, 1f);

            //Debug.Log("FLICKER: light value: " + randomValue);
            flashlight.intensity = randomValue * 2;

            yield return new WaitForSeconds(flickerInterval);
        }
    }
}
