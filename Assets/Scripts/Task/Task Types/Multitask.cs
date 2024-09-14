using System.Collections.Generic;
using UnityEngine;

public class MultiTask : Task
{
    [SerializeField] private string multitaskDescription;

    [SerializeField] private List<Task> subTasks;

    public string TaskDescription
    {
        get { return multitaskDescription; }
    }

    public override void CheckCompletion()
    {
        bool allCompleted = true;

        foreach (var task in subTasks)
        {
            Debug.Log($"Checking task: {task.name}, IsCompleted: {task.IsCompleted()}");

            if (!task.IsCompleted())
            {
                allCompleted = false;
                break;
            }
        }

        if (allCompleted)
        {
            Debug.Log("MultiTask " + gameObject.name + " completed");
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

    public override int GetCompletedObjectives()
    {
        int completedObjectives = 0;
        foreach (var task in subTasks)
        {
            completedObjectives += task.GetCompletedObjectives();
        }
        return completedObjectives;
    }

    public override int GetTotalObjectives()
    {
        int totalObjectives = 0;
        foreach (var task in subTasks)
        {
            totalObjectives += task.GetTotalObjectives();
        }
        return totalObjectives;
    }
}
