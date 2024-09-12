using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform roomCenter;
    public Transform spawnPoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = spawnPoint.position;
            Camera.main.GetComponent<CameraController>().MoveToRoom(roomCenter);
        }
    }
}
