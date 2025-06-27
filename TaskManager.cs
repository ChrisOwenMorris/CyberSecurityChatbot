using System;
using System.Collections.Generic;

public class TaskManager
{
    private List<TaskItem> tasks = new List<TaskItem>();

    public IReadOnlyList<TaskItem> Tasks => tasks.AsReadOnly();

    public TaskItem AddTask(string title, string description = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Task title cannot be empty.");

        var task = new TaskItem
        {
            Title = title,
            Description = description ?? $"Task: {title}"
        };

        tasks.Add(task);
        return task;
    }

    public bool DeleteTask(int index)
    {
        if (index < 0 || index >= tasks.Count) return false;

        tasks.RemoveAt(index);
        return true;
    }

    public bool CompleteTask(int index)
    {
        if (index < 0 || index >= tasks.Count) return false;

        tasks[index].IsCompleted = true;
        return true;
    }

    public string ListTasks()
    {
        if (tasks.Count == 0) return "No tasks available.";

        var result = "";
        for (int i = 0; i < tasks.Count; i++)
        {
            result += $"{i + 1}. {tasks[i].ToString()}\n";
        }
        return result.TrimEnd();
    }
}
