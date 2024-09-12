using UnityEngine;

public class PlacementTask : Task
{
    [SerializeField] private Item itemToPlace;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float placementTolerance = 0.5f;

    private void Start()
    {
        taskType = TaskType.Placement;

        if (itemToPlace != null)
        {
            itemToPlace.associatedTask = this;
        }
    }

    public override void CheckCompletion()
    {
        if (Vector3.Distance(itemToPlace.transform.position, targetPosition.position) < placementTolerance)
        {
            CompleteTask();
        }
    }

    public override bool IsCompleted()
    {
        return Vector3.Distance(itemToPlace.transform.position, targetPosition.position) < placementTolerance;
    }
}
