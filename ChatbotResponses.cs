// ChatbotResponses.cs
using System;
using System.Collections.Generic;
// Project: Cybersecurity Chatbot
// Developed with assistance from:
// - Google Gemini AI (for code generation, logic structure, and problem-solving)
// - OpenAI ChatGPT (for brainstorming, refining responses, and understanding concepts)
//

namespace CybersecurityChatbot
{
    public static class ChatbotResponses
    {
        // --- Keyword Recognition Responses ---
        // Maps a keyword (lowercase) to a fixed response string
        public static Dictionary<string, string> FixedKeywordResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"hello", "Hi there! How can I help you with cybersecurity today?"},
            {"hi", "Hello! Ready to learn about online safety?"},
            {"greetings", "Greetings! I'm your Cybersecurity Awareness Bot. What's on your mind?"},
            {"how are you", "I'm a bot, so I don't have feelings, but I'm ready to assist you! How can I help?"},
            {"who are you", "I am the Cybersecurity Awareness Bot, designed to help you understand and mitigate online risks."},
            {"what is cybersecurity", "Cybersecurity is the practice of protecting systems, networks, and programs from digital attacks. It's about keeping your information safe online."},
            {"what do you do", "I provide information, tips, and guidance on various cybersecurity topics to help you stay safe online."},
            {"thank you", "You're welcome! I'm always here to help."},
            {"thanks", "No problem at all! Stay safe out there."},
            {"ok", "Alright. Is there anything else you'd like to discuss?"},
            {"okay", "Understood. Feel free to ask more questions."}
            // Add more general conversational keywords as needed
        };


        // --- Cybersecurity Specific Keyword Responses (can be fixed or map to random topics) ---
        // We'll use this to guide where the conversation goes.
        public static Dictionary<string, string> CybersecurityTopicKeywords = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"password", "password_safety_tips"}, // Maps to a topic for random responses
            {"scam", "scam_prevention_tips"},
            {"phishing", "phishing_prevention_tips"},
            {"privacy", "online_privacy_tips"},
            {"malware", "malware_info"},
            {"virus", "malware_info"}, // Multiple keywords can map to the same topic
            {"firewall", "firewall_info"},
            {"encryption", "encryption_info"},
            {"vpn", "vpn_info"},
            {"2fa", "2fa_info"},
            {"two-factor authentication", "2fa_info"},
            {"data breach", "data_breach_info"}
            // Add more cybersecurity topics
        };

        // --- Random Responses for specific topics ---
        // Maps a topic name (lowercase) to a list of possible responses
        public static Dictionary<string, List<string>> RandomTopicResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            {"password_safety_tips", new List<string>
                {
                    "A strong password is typically 12+ characters, using a mix of upper/lower case, numbers, and symbols. Avoid common words!",
                    "Never reuse passwords across different online accounts. Use a password manager to keep track of unique, complex passwords.",
                    "Regularly change your important passwords, especially for financial and email accounts.",
                    "Enable two-factor authentication (2FA) wherever possible – it adds a crucial layer of security even if your password is stolen."
                }
            },
            {"scam_prevention_tips", new List<string>
                {
                    "Be wary of unsolicited emails, calls, or messages asking for personal information or urgent action. Scammers often create a sense of urgency.",
                    "Always verify the sender's identity and the legitimacy of links before clicking. Look for spelling errors or suspicious domains.",
                    "If an offer seems too good to be true, it probably is. Don't fall for tempting but unrealistic promises.",
                    "Never provide your passwords, banking details, or other sensitive information in response to unexpected requests."
                }
            },
            {"phishing_prevention_tips", new List<string>
                {
                    "Phishing emails often mimic legitimate companies. Double-check the sender's email address – it's usually slightly off.",
                    "Hover over links (don't click!) to see the actual URL before visiting. If it looks suspicious, don't click.",
                    "Be suspicious of attachments from unknown senders. They can contain malware. Always scan them first if unsure.",
                    "Report suspicious emails to your IT department or email provider. Don't engage with the phisher."
                }
            },
            {"online_privacy_tips", new List<string>
                {
                    "Regularly review privacy settings on social media and other online accounts. Limit what information is public.",
                    "Be mindful of what you share online, especially personal details. Once it's out there, it's hard to remove.",
                    "Use secure connections (HTTPS) when Browse, especially for sensitive transactions. Look for the padlock icon.",
                    "Understand app permissions before installing. Don't grant access to your photos or contacts if it's not relevant to the app's function."
                }
            },
            {"malware_info", new List<string>
                {
                    "Malware is malicious software designed to harm or gain unauthorized access to your computer. This includes viruses, worms, and ransomware.",
                    "Keep your operating system and all software updated. Updates often include critical security patches.",
                    "Use reputable antivirus and anti-malware software and keep it updated. Run regular scans.",
                    "Be careful about downloading files from untrusted sources or clicking suspicious pop-ups. They often contain malware."
                }
            },
            {"firewall_info", new List<string>
                {
                    "A firewall acts as a barrier between your computer/network and the internet, blocking unauthorized access.",
                    "Ensure your operating system's built-in firewall is enabled. For businesses, a dedicated network firewall is essential."
                }
            },
            {"encryption_info", new List<string>
                {
                    "Encryption is the process of scrambling data so it can only be read by authorized parties with a decryption key.",
                    "It's used to protect data in transit (e.g., HTTPS websites) and data at rest (e.g., encrypted hard drives, cloud storage)."
                }
            },
            {"vpn_info", new List<string>
                {
                    "A VPN (Virtual Private Network) creates a secure, encrypted connection over a public network, like the internet.",
                    "It helps protect your online privacy and security, especially when using public Wi-Fi, by masking your IP address and encrypting your traffic."
                }
            },
            {"2fa_info", new List<string>
                {
                    "Two-Factor Authentication (2FA) adds an extra layer of security beyond just a password.",
                    "It typically involves something you know (password) and something you have (e.g., a code from your phone via SMS or an authenticator app)."
                }
            },
            {"data_breach_info", new List<string>
                {
                    "A data breach occurs when unauthorized individuals gain access to sensitive, protected, or confidential data.",
                    "If you're notified of a data breach, change your password immediately, especially if you reused it. Enable 2FA on affected accounts."
                }
            }
            // Add more topics and responses to ensure variety
        };

        private static Random _random = new Random(); // Initialize Random once for efficiency

        public static string GetRandomResponseForTopic(string topicKey)
        {
            if (RandomTopicResponses.ContainsKey(topicKey))
            {
                List<string> responses = RandomTopicResponses[topicKey];
                int randomIndex = _random.Next(responses.Count);
                return responses[randomIndex];
            }
            return "I'm not sure how to respond to that. Could you try asking in a different way?";
        }

        // --- Sentiment Detection Responses ---
        public static Dictionary<string, string> SentimentResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"worried", "I understand that can be concerning. Let me share some tips to help you feel more secure about "}, // Appends topic
            {"anxious", "It's natural to feel that way about online security. Let's break down "},
            {"frustrated", "I'm sorry to hear you're feeling frustrated about that. Perhaps I can clarify "},
            {"confused", "It's okay to be confused, cybersecurity can be complex. What specific aspect of "},
            {"curious", "That's great! Curiosity is key to learning about cybersecurity. What are you curious about regarding "},
            {"concerned", "It's good to be concerned about {0}. Let's find some information to ease your mind."} // Uses string.Format
            // Add more sentiments and their lead-in responses
        };

        // --- Conversation Flow/Follow-up Responses ---
        // This could be more dynamic, but for now, simple fixed follow-ups
        public static Dictionary<string, string> FollowUpResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"tell me more_password_safety_tips", "Delving deeper into password safety: Consider using a password manager. They securely store complex passwords and help you generate new ones regularly. Never share your passwords with anyone!"},
            {"tell me more_scam_prevention_tips", "To add to scam prevention: Always verify the source of communication by contacting the organization directly using a known, official phone number or website, not one provided in the suspicious message."},
            {"tell me more_phishing_prevention_tips", "More on phishing: Be cautious of generic greetings like 'Dear Customer'. Legitimate organizations typically use your name. Also, be suspicious of any urgent call to action or threats."},
            {"tell me more_online_privacy_tips", "Further on online privacy: Regularly review the permissions you grant to apps on your phone and computer. Many apps ask for more access than they truly need, which can compromise your privacy."},
            {"what else_password_safety_tips", "Another point on password safety: Avoid using easily guessable information like your birth date, pet's name, or sequential numbers. Be creative and combine unrelated words."},
            {"what else_scam_prevention_tips", "What else for scams: Be especially wary of requests for payment in unusual forms, like gift cards or cryptocurrency, which are common tactics for scammers as they are difficult to trace."},
            {"what else_phishing_prevention_tips", "What else about phishing: If you receive a suspicious link, try opening it in a virtual machine or using a dedicated link-scanning tool if you have access to one, rather than directly on your main device."}
            // Add more follow-up responses, combining "trigger_topic"
        };

        public static string GetFollowUpResponse(string trigger, string topic)
        {
            string key = $"{trigger}_{topic}";
            if (FollowUpResponses.ContainsKey(key))
            {
                return FollowUpResponses[key];
            }
            return "I'm not sure how to respond to that. Could you try asking in a different way?";
        }

        // --- Default/Error Handling Responses ---
        public static List<string> DefaultResponses = new List<string>
        {
            "I'm not sure I understand. Could you please rephrase your question or ask about a specific cybersecurity topic?",
            "Hmm, I didn't quite get that. Perhaps you could ask about a topic like 'passwords', 'scams', or 'privacy'?",
            "My apologies, I'm still learning! Can you simplify your query or ask about another cybersecurity concept?",
            "I'm here to help with cybersecurity. What specific area are you interested in today?"
        };

        public static string GetRandomDefaultResponse()
        {
            int randomIndex = _random.Next(DefaultResponses.Count);
            return DefaultResponses[randomIndex];
        }
    }
}