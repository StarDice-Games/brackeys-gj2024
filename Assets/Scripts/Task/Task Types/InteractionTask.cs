using System.Collections.Generic;

public class InteractionTask : Task
{
    private List<IInteractable> interactables;

    private void Start()
    {
        taskType = TaskType.Interaction;
        interactables = new List<IInteractable>(GetComponentsInChildren<IInteractable>()); // Get children tasks
    }

    public override void CheckCompletion()
    {
        bool allCompleted = true;
        foreach (var interactable in interactables)
        {
            if (!interactable.IsCompleted())
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
        foreach (var interactable in interactables)
        {
            if (!interactable.IsCompleted())
            {
                return false;
            }
        }
        return true;
    }
}
