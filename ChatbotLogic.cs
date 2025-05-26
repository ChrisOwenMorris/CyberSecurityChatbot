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

            // --- 1. Handle "Exit" command ---
            // This is typically handled directly in the main loop of Program.cs for immediate termination.
            // No changes needed here.

            // --- 2. Sentiment Detection (Objective: Sentiment Detection) ---
            // High priority: respond to user sentiment first if detected.
            string? sentimentKeyword = ChatbotResponses.SentimentResponses.Keys
                                             .FirstOrDefault(k => lowerInput.Contains(k));

            if (!string.IsNullOrEmpty(sentimentKeyword))
            {
                // If there's a current topic, tailor the sentiment response to it.
                if (!string.IsNullOrEmpty(Chatbot.CurrentTopic))
                {
                    string topicForSentiment = Chatbot.CurrentTopic.Replace("_", " "); // Make topic more readable for the user.
                    string sentimentResponse = ChatbotResponses.SentimentResponses[sentimentKeyword!];

                    // Check if the sentiment response uses a placeholder {0} for the topic.
                    if (sentimentResponse.Contains("{0}"))
                    {
                        return string.Format(sentimentResponse, topicForSentiment + ".");
                    }
                    // If no placeholder, just append the topic.
                    return sentimentResponse + topicForSentiment + ".";
                }
                // If no current topic, provide a general sentiment response about online security.
                else
                {
                    return ChatbotResponses.SentimentResponses[sentimentKeyword!]
                                   .Replace("{0}", "online security in general.");
                }
            }

            // --- 3. Fixed Keyword Recognition (Objective: Keyword Recognition) ---
            // Handle common greetings or fixed phrases that don't relate to cybersecurity topics.
            string? fixedResponseKeyword = ChatbotResponses.FixedKeywordResponses.Keys
                                                 .FirstOrDefault(k => lowerInput.Contains(k));

            if (!string.IsNullOrEmpty(fixedResponseKeyword))
            {
                Chatbot.CurrentTopic = string.Empty; // Reset current topic for general conversations.
                return ChatbotResponses.FixedKeywordResponses[fixedResponseKeyword!];
            }

            // --- 4. User Memory and Recall: Setting Favorite Topic (Objective: Memory and Recall) ---
            // Detect if the user is explicitly stating their favorite cybersecurity topic.
            if (lowerInput.Contains("interested in") || lowerInput.Contains("my favorite topic is"))
            {
                string? identifiedTopic = ChatbotResponses.CybersecurityTopicKeywords.Keys
                                                 .FirstOrDefault(k => lowerInput.Contains(k));

                if (!string.IsNullOrEmpty(identifiedTopic))
                {
                    // Store the identified topic in Chatbot's static memory.
                    Chatbot.UserFavoriteCybersecurityTopic = identifiedTopic;
                    return $"That's great, {Chatbot.UserName}! I'll remember that you're particularly interested in {identifiedTopic}.";
                }
            }

            // NEW LOGIC START: User Memory and Recall: Explicitly Recalling Favorite Topic (Objective: Memory and Recall)
            // Respond if the user asks the chatbot to tell them their remembered favorite topic.
            if (lowerInput.Contains("what's my favorite topic") ||
                lowerInput.Contains("do you remember my favorite topic") ||
                lowerInput.Contains("what topic am i interested in") ||
                lowerInput.Contains("my interest") // Be careful with overly general keywords
            )
            {
                // Check if a favorite topic has been stored.
                if (!string.IsNullOrEmpty(Chatbot.UserFavoriteCybersecurityTopic))
                {
                    return $"Yes, {Chatbot.UserName}! You mentioned you are particularly interested in **{Chatbot.UserFavoriteCybersecurityTopic}**.";
                }
                else
                {
                    // If no favorite topic is stored, prompt the user to set one.
                    return $"I don't recall you having specified a favorite topic yet, {Chatbot.UserName}. What cybersecurity area are you most curious about?";
                }
            }
            // NEW LOGIC END

            // User Memory and Recall: Offering more tips based on favorite topic (Objective: Memory and Recall)
            // Triggered if the user asks for general "any other tips" or "more advice".
            if (lowerInput.Contains("any other tips") || lowerInput.Contains("more advice"))
            {
                // If a favorite topic is stored, try to provide a tip related to it.
                if (!string.IsNullOrEmpty(Chatbot.UserFavoriteCybersecurityTopic))
                {
                    string? personalizedTip = ChatbotResponses.GetRandomResponseForTopic(Chatbot.UserFavoriteCybersecurityTopic);

                    if (!string.IsNullOrEmpty(personalizedTip))
                    {
                        return $"Absolutely, {Chatbot.UserName}! Since you're interested in {Chatbot.UserFavoriteCybersecurityTopic}, here's another tip: {personalizedTip}";
                    }
                }
                // Fallback if no favorite topic or no specific tip for it.
                return $"I can give you more general cybersecurity tips, {Chatbot.UserName}. Would you like some?";
            }


            // --- *** IMPORTANT REORDERING START *** ---
            // Move "Conversation Flow / Follow-up Questions" (Section 6) ABOVE "Cybersecurity Topic Keyword Recognition" (Section 5)
            // This ensures contextual follow-ups are prioritized when a topic is already set.

            // --- 6. Conversation Flow / Follow-up Questions (Objective: Contextual Understanding) ---
            // Respond to follow-up questions related to the current conversation topic.
            // This section is now prioritized to handle contextual queries when a topic is already established.
            if (!string.IsNullOrEmpty(Chatbot.CurrentTopic))
            {
                string? followUpTrigger = null;

                // Accurately determine followUpTrigger based on input
                if (lowerInput.Contains("tell me more"))
                {
                    followUpTrigger = "tell me more";
                }
                else if (lowerInput.Contains("what else"))
                {
                    followUpTrigger = "what else"; // Correctly assign "what else" as the trigger
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
                        return followUpResponse; // If specific follow-up found, return it.
                    }
                    else
                    {
                        // Generic follow-up if a specific one isn't found for the current topic.
                        return $"I can try to tell you more about {Chatbot.CurrentTopic.Replace("_", " ")}. What specifically are you curious about?";
                    }
                }
            }

            // --- 5. Cybersecurity Topic Keyword Recognition & Random Responses (Objective: Keyword Recognition) ---
            // Detect specific cybersecurity keywords to provide relevant information.
            // This section now acts as a fallback for when no contextual follow-up is detected,
            // or when a *new* topic is introduced by the user.
            string? detectedCyberTopicKey = ChatbotResponses.CybersecurityTopicKeywords.Keys
                                                 .FirstOrDefault(k => lowerInput.Contains(k));

            if (!string.IsNullOrEmpty(detectedCyberTopicKey))
            {
                string topicName = ChatbotResponses.CybersecurityTopicKeywords[detectedCyberTopicKey!];
                Chatbot.CurrentTopic = topicName; // Set the current conversation topic.
                return ChatbotResponses.GetRandomResponseForTopic(topicName);
            }
            // --- *** IMPORTANT REORDERING END *** ---


            // --- 7. Error Handling / Default Response (Objective: Robustness) ---
            // If no other rules are matched, provide a general default response.
            Chatbot.CurrentTopic = string.Empty; // Clear current topic if no specific match found.
            return ChatbotResponses.GetRandomDefaultResponse();

        }
    }
}