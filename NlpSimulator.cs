using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions; // Useful for more advanced pattern matching

namespace CybersecurityChatbot
{
    // Define an enum for detected intents
    public enum Intent
    {
        None,
        Greeting,
        AskForInformation,
        ExpressSentiment,
        SetFavoriteTopic,
        RecallFavoriteTopic,
        RequestMoreTips,
        AddTask,
        DeleteTask,
        ShowTasks,
        StartQuiz,
        ShowActivityLog,
        AskForHelp,
        Exit // Although 'Exit' is usually handled directly by the main loop
    }

    /// <summary>
    /// Simulates basic Natural Language Processing capabilities.
    /// This includes sentiment analysis, keyword extraction, and intent detection
    /// based on predefined rules and keyword matching.
    /// </summary>
    public static class NlpSimulator
    {
        // Simple sentiment keywords and their associated sentiments
        private static readonly Dictionary<string, string> SentimentKeywords = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"happy", "positive"},
            {"great", "positive"},
            {"good", "positive"},
            {"excellent", "positive"},
            {"sad", "negative"},
            {"unhappy", "negative"},
            {"bad", "negative"},
            {"worried", "negative"},
            {"concerned", "negative"},
            {"scared", "negative"},
            {"anxious", "negative"},
            {"afraid", "negative"}
        };

        // Keywords for general intent detection (can be expanded)
        private static readonly Dictionary<string, Intent> IntentTriggers = new Dictionary<string, Intent>(StringComparer.OrdinalIgnoreCase)
        {
            {"hello", Intent.Greeting},
            {"hi", Intent.Greeting},
            {"hey", Intent.Greeting},
            {"what is", Intent.AskForInformation},
            {"tell me about", Intent.AskForInformation},
            {"how to", Intent.AskForInformation},
            {"interested in", Intent.SetFavoriteTopic},
            {"my favorite topic is", Intent.SetFavoriteTopic},
            {"what's my favorite topic", Intent.RecallFavoriteTopic},
            {"do you remember my favorite topic", Intent.RecallFavoriteTopic},
            {"what topic am i interested in", Intent.RecallFavoriteTopic},
            {"any other tips", Intent.RequestMoreTips},
            {"more advice", Intent.RequestMoreTips},
            {"add task", Intent.AddTask},
            {"delete task", Intent.DeleteTask},
            {"show tasks", Intent.ShowTasks},
            {"start quiz", Intent.StartQuiz},
            {"show activity log", Intent.ShowActivityLog},
            {"what have you done", Intent.ShowActivityLog},
            {"help", Intent.AskForHelp},
            {"exit", Intent.Exit}
        };

        /// <summary>
        /// Analyzes the sentiment of the input text based on predefined keywords.
        /// </summary>
        /// <param name="text">The user's input text.</param>
        /// <returns>A string indicating the sentiment ("positive", "negative", or "neutral").</returns>
        public static string AnalyzeSentiment(string text)
        {
            string lowerText = text.ToLower().Trim();
            foreach (var entry in SentimentKeywords)
            {
                if (lowerText.Contains(entry.Key))
                {
                    return entry.Value;
                }
            }
            return "neutral"; // Default sentiment
        }

        /// <summary>
        /// Extracts known keywords from the input text.
        /// </summary>
        /// <param name="text">The user's input text.</param>
        /// <param name="knownKeywords">A list of keywords to look for.</param>
        /// <returns>A list of extracted keywords found in the text.</returns>
        public static List<string> ExtractKeywords(string text, IEnumerable<string> knownKeywords)
        {
            string lowerText = text.ToLower().Trim();
            List<string> foundKeywords = new List<string>();

            // Sort keywords by length descending to prioritize longer, more specific phrases
            // This helps avoid matching "cat" when "cat food" is present
            var sortedKeywords = knownKeywords.OrderByDescending(k => k.Length);

            foreach (string keyword in sortedKeywords)
            {
                // Use Regex.IsMatch for whole word matching to avoid partial matches
                // \b ensures whole word match, e.g., "cat" won't match "catapult"
                if (Regex.IsMatch(lowerText, $@"\b{Regex.Escape(keyword.ToLower())}\b"))
                {
                    foundKeywords.Add(keyword);
                }
            }
            return foundKeywords;
        }

        /// <summary>
        /// Detects the likely intent of the user's input based on predefined triggers.
        /// </summary>
        /// <param name="text">The user's input text.</param>
        /// <returns>An <see cref="Intent"/> enum value representing the detected intent.</returns>
        public static Intent DetectIntent(string text)
        {
            string lowerText = text.ToLower().Trim();

            // Prioritize longer, more specific intent triggers
            var sortedIntentTriggers = IntentTriggers
                .OrderByDescending(entry => entry.Key.Length);

            foreach (var entry in sortedIntentTriggers)
            {
                // Use Regex.IsMatch for more robust keyword detection
                if (Regex.IsMatch(lowerText, $@"\b{Regex.Escape(entry.Key)}\b"))
                {
                    return entry.Value;
                }
            }

            // Fallback for general questions if no specific intent trigger matches
            if (lowerText.Contains("what") || lowerText.Contains("how") || lowerText.Contains("why") || lowerText.Contains("can you"))
            {
                return Intent.AskForInformation;
            }

            return Intent.None; // Default if no intent is detected
        }
    }
}
