using UnityEngine;

public class RainbowCycle : MonoBehaviour
{
    public float cycleSpeed = 1f; // Speed of color cycling
    private SpriteRenderer spriteRenderer;
    private float hue = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        hue += Time.deltaTime * cycleSpeed;
        if (hue > 1f) hue -= 1f;

        // Create a soft, light color
        float saturation = 0.8f; // Lower = more pastel
        float value = 1.0f;      // Max brightness

        Color softColor = Color.HSVToRGB(hue, saturation, value);
        spriteRenderer.color = softColor;
    }
}
