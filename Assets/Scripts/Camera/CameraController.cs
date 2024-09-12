using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float transitionSpeed = 2f;  
    private Transform targetRoomCenter;

    public void MoveToRoom(Transform roomCenter)
    {
        StartCoroutine(TransitionToRoom(roomCenter));
    }

    private IEnumerator TransitionToRoom(Transform roomCenter)
    {
        targetRoomCenter = roomCenter;

        while (Vector3.Distance(transform.position, targetRoomCenter.position) > 0.05f)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, targetRoomCenter.position, Time.deltaTime * transitionSpeed);
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

            yield return null; 
        }

        transform.position = new Vector3(targetRoomCenter.position.x, targetRoomCenter.position.y, transform.position.z);
    }

}
