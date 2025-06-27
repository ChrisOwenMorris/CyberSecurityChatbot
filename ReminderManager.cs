using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ReminderManager
{
    public delegate void ReminderTriggeredHandler(TaskItem task);
    public event ReminderTriggeredHandler OnReminderTriggered;

    public void SetReminder(TaskItem task, string userInput)
    {
        var reminderTime = ParseReminderTime(userInput);
        if (reminderTime.HasValue)
        {
            task.ReminderDate = reminderTime;
            task.ReminderTriggered = false; // reset trigger if re-setting reminder
        }
        else
        {
            throw new ArgumentException("Unable to parse reminder date/time.");
        }
    }

    // Parses reminders like:
    // "Remind me in 3 minutes"
    // "Remind me on 2025-07-01 at 10:00"
    // "Remind me at 14:30"
    public DateTime? ParseReminderTime(string input)
    {
        string lower = input.ToLower();

        // Check full datetime first: "on 2025-07-01 at 10:00"
        var fullDateTimeMatch = Regex.Match(lower, @"on (\d{4}-\d{2}-\d{2}) at (\d{1,2}:\d{2})");
        if (fullDateTimeMatch.Success)
        {
            if (DateTime.TryParse(fullDateTimeMatch.Groups[1].Value, out DateTime date) &&
                TimeSpan.TryParse(fullDateTimeMatch.Groups[2].Value, out TimeSpan time))
            {
                return date.Date + time;
            }
        }

        // "in 3 minutes" or "in 2 hours"
        var inTimeMatch = Regex.Match(lower, @"in (\d+) (minute|minutes|hour|hours)");
        if (inTimeMatch.Success)
        {
            int amount = int.Parse(inTimeMatch.Groups[1].Value);
            string unit = inTimeMatch.Groups[2].Value;

            if (unit.StartsWith("minute"))
                return DateTime.Now.AddMinutes(amount);
            else if (unit.StartsWith("hour"))
                return DateTime.Now.AddHours(amount);
        }

        // "on 2025-07-01"
        var onDateMatch = Regex.Match(lower, @"on (\d{4}-\d{2}-\d{2})");
        if (onDateMatch.Success)
        {
            if (DateTime.TryParse(onDateMatch.Groups[1].Value, out DateTime date))
            {
                // Default reminder time: 9 AM
                return date.Date.AddHours(9);
            }
        }

        // "at 14:30"
        var atTimeMatch = Regex.Match(lower, @"at (\d{1,2}:\d{2})");
        if (atTimeMatch.Success)
        {
            if (TimeSpan.TryParse(atTimeMatch.Groups[1].Value, out TimeSpan time))
            {
                DateTime reminderTime = DateTime.Today.Add(time);
                if (reminderTime < DateTime.Now)
                    reminderTime = reminderTime.AddDays(1); // next day if time already passed
                return reminderTime;
            }
        }

        // Failed to parse
        return null;
    }

    // Checks tasks and triggers reminder event if any reminders are due and not yet triggered
    public void CheckReminders(List<TaskItem> tasks)
    {
        if (tasks == null) return;

        DateTime now = DateTime.Now;
        foreach (var task in tasks)
        {
            if (task.ReminderDate.HasValue && !task.ReminderTriggered && task.ReminderDate <= now)
            {
                task.ReminderTriggered = true;
                OnReminderTriggered?.Invoke(task);
            }
        }
    }
}
