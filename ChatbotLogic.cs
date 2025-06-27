using System;
using System.Linq;
using System.Text.RegularExpressions; // Still needed if Regex is used directly for some specific case
using System.Collections.Generic; // Added for List and Dictionary if not already there

namespace CybersecurityChatbot
{
    public static class ChatbotLogic
    {
        public static string ProcessUserInput(string userInput)
        {
            string lowerInput = userInput.ToLower().Trim();

            // --- 1. Use NlpSimulator for Sentiment Detection ---
            string sentiment = NlpSimulator.AnalyzeSentiment(lowerInput);
            if (sentiment != "neutral")
            {
                // Map the sentiment string to a keyword recognized by ChatbotResponses.SentimentResponses
                // Assuming ChatbotResponses.SentimentResponses keys are "positive", "negative" etc.
                string? sentimentKeyword = ChatbotResponses.SentimentResponses.Keys
                                                      .FirstOrDefault(k => sentiment.Contains(k)); // Simplified for this example

                if (!string.IsNullOrEmpty(sentimentKeyword))
                {
                    if (!string.IsNullOrEmpty(Chatbot.CurrentTopic))
                    {
                        string topicForSentiment = Chatbot.CurrentTopic.Replace("_", " ");
                        string sentimentResponse = ChatbotResponses.SentimentResponses[sentimentKeyword!];

                        if (sentimentResponse.Contains("{0}"))
                        {
                            return string.Format(sentimentResponse, topicForSentiment + ".");
                        }
                        return sentimentResponse + topicForSentiment + ".";
                    }
                    else
                    {
                        return ChatbotResponses.SentimentResponses[sentimentKeyword!]
                                               .Replace("{0}", "online security in general.");
                    }
                }
            }

            // --- 2. Use NlpSimulator for Intent Detection ---
            Intent detectedIntent = NlpSimulator.DetectIntent(lowerInput);

            switch (detectedIntent)
            {
                case Intent.Greeting:
                    Chatbot.CurrentTopic = string.Empty; // Reset topic for greetings
                    // You would have a greeting specific response in ChatbotResponses
                    return ChatbotResponses.FixedKeywordResponses.ContainsKey("hello") ?
                           ChatbotResponses.FixedKeywordResponses["hello"] :
                           $"Hello, {Chatbot.UserName}! How can I assist you with cybersecurity today?";

                case Intent.SetFavoriteTopic:
                    // Extract cybersecurity topic keywords to identify the favorite topic
                    var topicKeywords = NlpSimulator.ExtractKeywords(lowerInput, ChatbotResponses.CybersecurityTopicKeywords.Keys);
                    string? identifiedTopic = topicKeywords.FirstOrDefault(); // Take the first found topic

                    if (!string.IsNullOrEmpty(identifiedTopic))
                    {
                        Chatbot.UserFavoriteCybersecurityTopic = ChatbotResponses.CybersecurityTopicKeywords[identifiedTopic];
                        return $"That's great, {Chatbot.UserName}! I'll remember that you're particularly interested in {Chatbot.UserFavoriteCybersecurityTopic}.";
                    }
                    break; // If no specific topic found, fall through to default handling

                case Intent.RecallFavoriteTopic:
                    if (!string.IsNullOrEmpty(Chatbot.UserFavoriteCybersecurityTopic))
                    {
                        return $"Yes, {Chatbot.UserName}! You mentioned you are particularly interested in **{Chatbot.UserFavoriteCybersecurityTopic}**.";
                    }
                    else
                    {
                        return $"I don't recall you having specified a favorite topic yet, {Chatbot.UserName}. What cybersecurity area are you most curious about?";
                    }

                case Intent.RequestMoreTips:
                    if (!string.IsNullOrEmpty(Chatbot.UserFavoriteCybersecurityTopic))
                    {
                        string? personalizedTip = ChatbotResponses.GetRandomResponseForTopic(Chatbot.UserFavoriteCybersecurityTopic);
                        if (!string.IsNullOrEmpty(personalizedTip))
                        {
                            return $"Absolutely, {Chatbot.UserName}! Since you're interested in {Chatbot.UserFavoriteCybersecurityTopic}, here's another tip: {personalizedTip}";
                        }
                    }
                    return $"I can give you more general cybersecurity tips, {Chatbot.UserName}. Would you like some?";

                case Intent.AskForInformation:
                    // This intent is broad. If a current topic exists, prioritize follow-up.
                    // Otherwise, try to find a new cybersecurity topic.

                    // Check for follow-up questions first if there's a current topic
                    if (!string.IsNullOrEmpty(Chatbot.CurrentTopic))
                    {
                        string? followUpResponse = ChatbotResponses.GetFollowUpResponse(lowerInput, Chatbot.CurrentTopic);
                        if (!string.IsNullOrEmpty(followUpResponse))
                        {
                            return followUpResponse;
                        }
                        return $"I can try to tell you more about {Chatbot.CurrentTopic.Replace("_", " ")}. What specifically are you curious about?";
                    }
                    // If no current topic, try to detect a new cybersecurity topic
                    goto case Intent.None; // Fall through to general topic detection if no specific intent or current topic

                case Intent.AddTask:
                case Intent.DeleteTask:
                case Intent.ShowTasks:
                case Intent.StartQuiz:
                case Intent.ShowActivityLog:
                    // These intents are typically handled by MainWindow.xaml.cs's ProcessInput.
                    // For ChatbotLogic, we acknowledge them and suggest the UI handles them.
                    // Or, you could return a message instructing the user, as ChatbotLogic doesn't perform these actions directly.
                    return $"I've detected you want to {detectedIntent.ToString().ToLower().Replace("task", "a task")}. The main application will handle this.";
                case Intent.Exit:
                    return "Goodbye! Stay safe online.";
                case Intent.AskForHelp:
                    return ChatbotResponses.GetRandomDefaultResponse() + " How can I specifically help you?";

                case Intent.None: // Fallback for general information or new topic detection
                default:
                    // Use NlpSimulator to extract specific cybersecurity topics
                    var detectedCyberTopicKeys = NlpSimulator.ExtractKeywords(lowerInput, ChatbotResponses.CybersecurityTopicKeywords.Keys);
                    string? detectedCyberTopicKey = detectedCyberTopicKeys.FirstOrDefault();

                    if (!string.IsNullOrEmpty(detectedCyberTopicKey))
                    {
                        string topicName = ChatbotResponses.CybersecurityTopicKeywords[detectedCyberTopicKey!];
                        Chatbot.CurrentTopic = topicName;
                        return ChatbotResponses.GetRandomResponseForTopic(topicName);
                    }
                    break; // If no specific topic, fall through to default response
            }

            // --- 3. Error Handling / Default Response (Objective: Robustness) ---
            Chatbot.CurrentTopic = string.Empty; // Clear current topic if no specific match found.
            return ChatbotResponses.GetRandomDefaultResponse();
        }
    }
}
