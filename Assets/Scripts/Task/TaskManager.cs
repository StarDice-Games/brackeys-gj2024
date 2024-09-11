using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    private List<Task> allTasks;

    void Start()
    {
        Task.OnTaskCompleted += CheckAllTasksCompleted;
    }

    private void CheckAllTasksCompleted(Task completedTask)
    {
        bool allTasksCompleted = true;
        foreach (var task in allTasks)
        {
            if (!task.IsCompleted())
            {
                allTasksCompleted = false;
                break;
            }
        }

        if (allTasksCompleted)
        {
            Debug.Log("All tasks completed!");
        }
    }
}
