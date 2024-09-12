using System.Collections.Generic;

public class CleaningTask : Task
{
    private List<IInteractable> dirtObjects;

    private void Start()
    {
        taskType = TaskType.Cleaning;
        dirtObjects = new List<IInteractable>(GetComponentsInChildren<IInteractable>());
    }

    public override void CheckCompletion()
    {
        bool allCleaned = true;
        foreach (var dirt in dirtObjects)
        {
            if (!dirt.IsCompleted())
            {
                allCleaned = false;
                break;
            }
        }

        if (allCleaned)
        {
            CompleteTask();
        }
    }

    public override bool IsCompleted()
    {
        foreach (var dirt in dirtObjects)
        {
            if (!dirt.IsCompleted())
            {
                return false;
            }
        }
        return true;
    }
}
