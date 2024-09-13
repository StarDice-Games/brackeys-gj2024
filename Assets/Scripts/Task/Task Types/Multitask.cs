using System.Collections.Generic;
using UnityEngine;

public class MultiTask : Task
{
    [SerializeField] private List<Task> subTasks;

    public override void CheckCompletion()
    {
        bool allCompleted = true;
        foreach (var task in subTasks)
        {
            if (!task.IsCompleted())
            {
                allCompleted = false;
                break;
            }
        }

        if (allCompleted)
        {
            CompleteTask();
        }
    }

    public override bool IsCompleted()
    {
        foreach (var task in subTasks)
        {
            if (!task.IsCompleted())
            {
                return false;
            }
        }
        return true;
    }
}
