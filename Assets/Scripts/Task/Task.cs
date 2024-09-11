using UnityEngine;

public enum TaskType
{
    Interaction,
    Placement,
    Cleaning
}

public abstract class Task : MonoBehaviour
{
    public string taskName;
    public TaskType taskType;

    public delegate void TaskCompleted(Task task);
    public static event TaskCompleted OnTaskCompleted;

    public abstract bool IsCompleted();

    public abstract void CheckCompletion();

    protected void CompleteTask()
    {
        Debug.Log($"Task completed: {taskName}");
        OnTaskCompleted?.Invoke(this);
    }
}
