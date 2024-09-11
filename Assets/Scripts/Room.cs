using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform roomCenter;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            Camera.main.GetComponent<CameraController>().MoveToRoom(roomCenter);
        }
    }
}
