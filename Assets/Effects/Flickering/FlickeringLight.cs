using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] private Light lightToFlicker; // Drag & drop the light in Inspector

    [SerializeField, Range(0f, 30f)] private float minIntensity = 0.5f;
    [SerializeField, Range(0f, 30f)] private float maxIntensity = 1.2f;
    [SerializeField, Min(0f)] private float timeBetweenIntensity = 0.1f;

    private float currentTimer;

    private void Awake()
    {
        ValidateIntensityBounds();
    }

    private void Update()
    {
        currentTimer += Time.deltaTime;
        if (currentTimer < timeBetweenIntensity) return;

        // Set random flicker intensity
        lightToFlicker.intensity = Random.Range(minIntensity, maxIntensity);
        currentTimer = 0f;
    }

    private void ValidateIntensityBounds()
    {
        if (minIntensity > maxIntensity)
        {
            Debug.LogWarning("Min Intensity is greater than Max Intensity, swapping values!");
            float temp = minIntensity;
            minIntensity = maxIntensity;
            maxIntensity = temp;
        }
    }
}
