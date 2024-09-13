using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform roomCenter;
    [SerializeField] Transform spawnPoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = spawnPoint.position;
            Camera.main.GetComponent<CameraController>().MoveToRoom(roomCenter);
        }

        if (other.CompareTag("Guest"))
        {
            other.transform.position = spawnPoint.position;
        }
    }
}
