using System;

public class TaskItem
{
    public string Title { get; set; }
    public string Description { get; set; } = "";
    public DateTime? ReminderDate { get; set; }  // exact date/time reminder
    public bool ReminderTriggered { get; set; } = false;
    public bool IsCompleted { get; set; } = false;

    public override string ToString()
    {
        string status = IsCompleted ? "✅ Completed" : "⏳ Pending";
        string reminder = ReminderDate.HasValue
            ? $" – Reminder: {ReminderDate.Value.ToString("g")}"  // general date/time format
            : "";

        return $"{Title} – {status}{reminder}\n{Description}";
    }
}
