using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    private List<Task> initialTasks;
    [SerializeField]
    private Task finalTask;

    private bool initialTasksCompleted = false;

    void Start()
    {
        Task.OnTaskCompleted += CheckTasksCompletion;
    }

    private void CheckTasksCompletion(Task completedTask)
    {
        if (!initialTasksCompleted)
        {
            bool allInitialTasksCompleted = true;
            foreach (var task in initialTasks)
            {
                if (!task.IsCompleted())
                {
                    allInitialTasksCompleted = false;
                    break;
                }
            }

            if (allInitialTasksCompleted)
            {
                Debug.Log("All initial task completed");
            }

            if (allInitialTasksCompleted&&finalTask)
            {
                initialTasksCompleted = true;
                finalTask.gameObject.SetActive(true);
            }
        }

        if (initialTasksCompleted && finalTask.IsCompleted())
        {
            Debug.Log("All tasks completed! Game complete.");
        }
    }
}
