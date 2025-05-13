using UnityEngine;

public class RoomSwitch : MonoBehaviour
{
    public GameObject virtualCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            virtualCam.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            virtualCam.SetActive(false);
        }
    }
}
