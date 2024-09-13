using System.Collections.Generic;
using UnityEngine;

public class PlacementTask : Task
{
    [SerializeField] private Item itemToPlace;
    [SerializeField] private List<Transform> acceptablePositions;
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
        foreach (var targetPosition in acceptablePositions)
        {
            if (Vector3.Distance(itemToPlace.transform.position, targetPosition.position) < placementTolerance)
            {
                CompleteTask();
                return;
            }
        }
    }

    public override bool IsCompleted()
    {
        foreach (var targetPosition in acceptablePositions)
        {
            if (Vector3.Distance(itemToPlace.transform.position, targetPosition.position) < placementTolerance)
            {
                return true;
            }
        }
        return false;
    }

    public override int GetCompletedObjectives()
    {
        foreach (var targetPosition in acceptablePositions)
        {
            if (Vector3.Distance(itemToPlace.transform.position, targetPosition.position) < placementTolerance)
            {
                return 1;
            }
        }
        return 0;
    }

    public override int GetTotalObjectives()
    {
        return 1;
    }
}
