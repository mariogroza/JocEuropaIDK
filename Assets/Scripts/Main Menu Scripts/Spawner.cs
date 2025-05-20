using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpriteSpawner : MonoBehaviour
{
    public float spawnRate = 1f; // Time between spawns
    public Vector2 fallSpeedRange = new Vector2(1f, 2f); // Random fall speed per object
    public Vector2 xRange = new Vector2(-8f, 8f); // X position range
    public float ySpawnOffset = 6f; // Y offset above camera
    public Vector2 randomScaleRange = new Vector2(1f, 2f); // Scale range

    private string[] spriteNames = { "Blue", "Green", "Yellow", "Red" };

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnSprite();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnSprite()
    {
        string chosenName = spriteNames[Random.Range(0, spriteNames.Length)];
        Sprite sprite = Resources.Load<Sprite>(chosenName);
        if (sprite == null)
        {
            Debug.LogError("Sprite not found in Resources: " + chosenName);
            return;
        }

        GameObject obj = new GameObject("Falling_" + chosenName);
        SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;

        // Ensure sprite is rendered behind UI
        sr.sortingLayerName = "Default";
        sr.sortingOrder = -10;

        float randomX = Random.Range(xRange.x, xRange.y);
        float y = Camera.main.transform.position.y + ySpawnOffset;
        obj.transform.position = new Vector3(randomX, y, 0f);

        obj.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        float randomScale = Random.Range(randomScaleRange.x, randomScaleRange.y);
        obj.transform.localScale = Vector3.one * randomScale;

        // Random fall speed
        float randomSpeed = Random.Range(fallSpeedRange.x, fallSpeedRange.y);
        obj.AddComponent<FallDown>().speed = randomSpeed;
    }
}
