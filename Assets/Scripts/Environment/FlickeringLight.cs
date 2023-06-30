using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D flickeringLight;
    public float flickerInterval = 0.1f;

    private bool isFlickering = false;

    private void Start()
    {
        StartCoroutine(FlickerLight());
    }

    private IEnumerator FlickerLight()
    {
        isFlickering = true;

        while (isFlickering)
        {
            float randomValue = Random.Range(0f, 1f);
            float randomIntensity = Mathf.Round(randomValue);

            if(randomIntensity == 1)
            {
                randomIntensity = 0.5f;
            }

            flickeringLight.intensity = randomIntensity;

            yield return new WaitForSeconds(flickerInterval);
        }
    }

    public void StopFlickering()
    {
        isFlickering = false;
    }
}