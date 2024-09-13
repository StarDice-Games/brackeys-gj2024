using UnityEngine;

public class InteractionTask : Task
{
    [SerializeField] private GameObject interactableGameObject;
    private IInteractable interactable;

    private void Start()
    {
        taskType = TaskType.Interaction;

        if (interactableGameObject.TryGetComponent(out IInteractable interactableComponent))
        {
            interactable = interactableComponent;

            if (interactableGameObject.TryGetComponent(out Item item))
            {
                item.associatedTask = this;
            }
        }
    }

    public override void CheckCompletion()
    {
        if (interactable != null && interactable.IsCompleted())
        {
            CompleteTask();
        }
    }

    public override bool IsCompleted()
    {
        return interactable != null && interactable.IsCompleted();
    }

    public override int GetCompletedObjectives()
    {
        return interactable != null && interactable.IsCompleted() ? 1 : 0;
    }

    public override int GetTotalObjectives()
    {
        return 1;
    }
}
