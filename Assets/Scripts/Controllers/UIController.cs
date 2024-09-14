using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<Image> taskProgressBars;
    [SerializeField] private List<TextMeshProUGUI> taskDescriptions;
    [SerializeField] private List<TextMeshProUGUI> taskProgressTexts;
    [SerializeField] private TaskManager taskManager;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        List<Task> allTasks = taskManager.GetAllTasks();

        for (int i = 0; i < allTasks.Count && i < taskProgressBars.Count; i++)
        {
            Task task = allTasks[i];
            float progress = task.GetCompletedObjectives() / (float)task.GetTotalObjectives();
            taskProgressBars[i].fillAmount = progress;
            taskProgressTexts[i].text = $"{task.GetCompletedObjectives()} / {task.GetTotalObjectives()}";

            if (task is MultiTask multiTask)
            {
                taskDescriptions[i].text = multiTask.TaskDescription;
            }
            else
            {
                taskDescriptions[i].text = task.GetType().Name;
            }
        }
    }
}
