using UnityEngine;

public class PlacementTask : Task
{
    public Item itemToPlace;
    public Transform targetPosition;

    private void Start()
    {
        taskType = TaskType.Placement;
    }

    public override void CheckCompletion()
    {
        if (Vector3.Distance(itemToPlace.transform.position, targetPosition.position) < 0.5f)
        {
            CompleteTask();
        }
    }

    public override bool IsCompleted()
    {
        return Vector3.Distance(itemToPlace.transform.position, targetPosition.position) < 0.5f;
    }
}
