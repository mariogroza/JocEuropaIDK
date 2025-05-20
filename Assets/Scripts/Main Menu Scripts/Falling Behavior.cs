using UnityEngine;

public class FallDown : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Destroy when out of view
        if (Camera.main != null && transform.position.y < Camera.main.transform.position.y - 6f)
        {
            Destroy(gameObject);
        }
    }
}
