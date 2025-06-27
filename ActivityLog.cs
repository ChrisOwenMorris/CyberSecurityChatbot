using System.Collections.Generic;
using System.Text;

namespace CyberSecurityChatbotUI
{
    public class ActivityLog
    {
        private List<string> logs = new List<string>();
        private const int MaxEntries = 10;

        public void Add(string entry)
        {
            logs.Add(entry);
        }

        public string GetRecent()
        {
            var count = logs.Count;
            if (count == 0) return "No activities logged.";

            var recent = count > MaxEntries ? logs.GetRange(count - MaxEntries, MaxEntries) : logs;
            var sb = new StringBuilder();
            int index = 1;
            foreach (var log in recent)
            {
                sb.AppendLine($"{index++}. {log}");
            }
            return sb.ToString();
        }
    }
}
