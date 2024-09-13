using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    private List<Task> initialTasks;
    [SerializeField]
    private Task finalTaskWelcomeGuest;
    [SerializeField]
    private Task finalTaskDinner;

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
                initialTasksCompleted = true;
                Debug.Log("All initial tasks completed!");

                if (finalTaskWelcomeGuest != null)
                {
                    finalTaskWelcomeGuest.gameObject.SetActive(true);
                }
            }
        }

        if (initialTasksCompleted && finalTaskWelcomeGuest != null && finalTaskWelcomeGuest.IsCompleted())
        {
            Debug.Log("Guest welcomed, let's activate DINNER!");

            if (finalTaskDinner != null)
            {
                finalTaskDinner.gameObject.SetActive(true);
            }
        }

        if (finalTaskDinner != null && finalTaskDinner.IsCompleted())
        {
            Debug.Log("All tasks completed! Game complete.");
        }
    }

    public List<Task> GetAllTasks()
    {
        List<Task> allTasks = new List<Task>();
        allTasks.AddRange(initialTasks);

        if (finalTaskWelcomeGuest != null)
            allTasks.Add(finalTaskWelcomeGuest);

        if (finalTaskDinner != null)
            allTasks.Add(finalTaskDinner);

        return allTasks;
    }

}
