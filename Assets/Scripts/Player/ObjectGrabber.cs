using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    [SerializeField]
    private Transform grabPoint;

    [SerializeField]
    private Transform rayPoint;

    [SerializeField]
    private float grabDistance;

    private GameObject grabbedObject;
    
    [SerializeField]
    private LayerMask layerMask;

    public void Grab(Vector2 vectorDirection)
    {
        Debug.DrawRay(rayPoint.position, vectorDirection * grabDistance, Color.red, 3f);

        // Release item
        if (grabbedObject != null)
        {
            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
            return;
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, vectorDirection, grabDistance, layerMask);

        if (hitInfo.collider != null)
        {
            // Pick up item
            if (grabbedObject == null)
            {
                grabbedObject = hitInfo.collider.gameObject;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
            }
        }

    }
}
