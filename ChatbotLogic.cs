using System;
using System.Linq; // For .Any() and other LINQ methods
using System.Text.RegularExpressions; // If you want more advanced keyword matching

namespace CybersecurityChatbot
{
    public static class ChatbotLogic
    {
        public static string ProcessUserInput(string userInput)
        {
            string lowerInput = userInput.ToLower().Trim();

            // --- 1. Handle "Exit" command (already in Program.cs, but good to note) ---
            // This is typically handled directly in the main loop of Program.cs.

            // --- 2. Sentiment Detection (Objective: Sentiment Detection) ---
            // High priority: respond to sentiment first
            string? sentimentKeyword = ChatbotResponses.SentimentResponses.Keys
                                        .FirstOrDefault(k => lowerInput.Contains(k));

            if (!string.IsNullOrEmpty(sentimentKeyword))
            {
                if (!string.IsNullOrEmpty(Chatbot.CurrentTopic))
                {
                    string topicForSentiment = Chatbot.CurrentTopic.Replace("_", " "); // Make topic more readable
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

            // --- 3. Fixed Keyword Recognition (Objective: Keyword Recognition) ---
            string? fixedResponseKeyword = ChatbotResponses.FixedKeywordResponses.Keys
                                            .FirstOrDefault(k => lowerInput.Contains(k));

            if (!string.IsNullOrEmpty(fixedResponseKeyword))
            {
                Chatbot.CurrentTopic = string.Empty; // Reset topic for general greetings
                return ChatbotResponses.FixedKeywordResponses[fixedResponseKeyword!];
            }

            // --- 4. User Memory and Recall (Objective: Memory and Recall) ---
            if (lowerInput.Contains("interested in") || lowerInput.Contains("my favorite topic is"))
            {
                string? identifiedTopic = ChatbotResponses.CybersecurityTopicKeywords.Keys
                                            .FirstOrDefault(k => lowerInput.Contains(k));

                if (!string.IsNullOrEmpty(identifiedTopic))
                {
                    Chatbot.UserFavoriteCybersecurityTopic = identifiedTopic;
                    return $"That's great, {Chatbot.UserName}! I'll remember that you're particularly interested in {identifiedTopic}.";
                }
            }

            if (lowerInput.Contains("any other tips") || lowerInput.Contains("more advice"))
            {
                if (!string.IsNullOrEmpty(Chatbot.UserFavoriteCybersecurityTopic))
                {
                    string? personalizedTip = ChatbotResponses.GetRandomResponseForTopic(Chatbot.UserFavoriteCybersecurityTopic);

                    if (!string.IsNullOrEmpty(personalizedTip))
                    {
                        return $"Absolutely, {Chatbot.UserName}! Since you're interested in {Chatbot.UserFavoriteCybersecurityTopic}, here's another tip: {personalizedTip}";
                    }
                }

                return $"I can give you more general cybersecurity tips, {Chatbot.UserName}. Would you like some?";
            }

            // --- 5. Cybersecurity Topic Keyword Recognition & Random Responses ---
            string? detectedCyberTopicKey = ChatbotResponses.CybersecurityTopicKeywords.Keys
                                            .FirstOrDefault(k => lowerInput.Contains(k));

            if (!string.IsNullOrEmpty(detectedCyberTopicKey))
            {
                string topicName = ChatbotResponses.CybersecurityTopicKeywords[detectedCyberTopicKey!];
                Chatbot.CurrentTopic = topicName;
                return ChatbotResponses.GetRandomResponseForTopic(topicName);
            }

            // --- 6. Conversation Flow / Follow-up Questions ---
            if (!string.IsNullOrEmpty(Chatbot.CurrentTopic))
            {
                string? followUpTrigger = null;

                if (lowerInput.Contains("tell me more") || lowerInput.Contains("what else"))
                {
                    followUpTrigger = "tell me more";
                }
                else if (lowerInput.Contains("more details") || lowerInput.Contains("explain more"))
                {
                    followUpTrigger = "more details";
                }

                if (followUpTrigger != null)
                {
                    string? followUpResponse = ChatbotResponses.GetFollowUpResponse(followUpTrigger, Chatbot.CurrentTopic);

                    if (!string.IsNullOrEmpty(followUpResponse))
                    {
                        return followUpResponse;
                    }
                    else
                    {
                        return $"I can try to tell you more about {Chatbot.CurrentTopic.Replace("_", " ")}. What specifically are you curious about?";
                    }
                }
            }

            // --- 7. Error Handling / Default Response ---
            Chatbot.CurrentTopic = string.Empty;
            return ChatbotResponses.GetRandomDefaultResponse();
        }
    }
}
