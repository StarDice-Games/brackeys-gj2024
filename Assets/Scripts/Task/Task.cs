using UnityEngine;

public enum TaskType
{
    Interaction,
    Placement,
    Cleaning
}

public abstract class Task : MonoBehaviour
{
    [HideInInspector]
    public TaskType taskType;

    public delegate void TaskCompleted(Task task);
    public static event TaskCompleted OnTaskCompleted;

    public abstract bool IsCompleted();

    public abstract void CheckCompletion();

    protected void CompleteTask()
    {
        Debug.Log("Task completed");
        OnTaskCompleted?.Invoke(this);
    }

    public abstract int GetCompletedObjectives();

    public abstract int GetTotalObjectives();
}
