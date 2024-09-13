using UnityEngine;

public class DoorDebugger : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider2D;
    [SerializeField] Transform spawnPoint;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Debug.DrawLine(gameObject.transform.position, spawnPoint.position, Color.cyan);

        Gizmos.color = new Color(0, 0, 1, 0.25f);
        Gizmos.DrawCube(transform.position, new Vector3(boxCollider2D.size.x, boxCollider2D.size.y, 1));
    }
#endif
}
