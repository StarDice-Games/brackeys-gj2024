using UnityEngine;

public class CleaningTask : Task
{
    [SerializeField] private GameObject cleanableGameObject;
    private IInteractable cleanable;

    private void Start()
    {
        taskType = TaskType.Cleaning;

        if (cleanableGameObject.TryGetComponent(out IInteractable cleanableComponent))
        {
            cleanable = cleanableComponent;

            if (cleanableGameObject.TryGetComponent(out Item item))
            {
                item.associatedTask = this;
            }
        }
    }

    public override void CheckCompletion()
    {
        if (cleanable != null && cleanable.IsCompleted())
        {
            CompleteTask();
        }
    }

    public override bool IsCompleted()
    {
        return cleanable != null && cleanable.IsCompleted();
    }
}
