using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ItemPlaceDebugger : MonoBehaviour
{
    [SerializeField] Transform anchorPoint;
    [SerializeField] BoxCollider2D boxCollider2D;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Debug.DrawLine(gameObject.transform.position, anchorPoint.position, Color.white);

        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(boxCollider2D.size.x, boxCollider2D.size.y, 1));
    }
#endif

}
