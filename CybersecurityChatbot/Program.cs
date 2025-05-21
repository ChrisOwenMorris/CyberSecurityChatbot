using System;
using System.Media;
using System.Threading;
using System.Runtime.Versioning;

namespace CybersecurityChatbot
{
    class Program
    {
        [SupportedOSPlatform("windows")]
        static void PlayVoiceGreeting(string audioFilePath)
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(audioFilePath))
                {
                    player.PlaySync(); // Play the audio synchronously
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing voice greeting: {ex.Message}");
                Console.WriteLine("Please ensure 'welcome.wav' is in the application directory.");
            }
        }

        static void DisplayAsciiArt()
        {
            Console.WriteLine(@"
  _    _           _       ____   ____
 | |  | |         | |     |  _ \ / ___|
 | |__| | __ _ ___| | __  | |_) | |
 |  __  |/ _` / __| |/ /  |  _ <| |
 | |  | | (_| \__ \   <   | |_) | |___
 |_|  |_|\__,_|___/_|\_\  |____/ \____|
             Awareness Bot
");
        }

        static string GetResponse(string? query)
        {
            string lowerQuery = (query ?? "").ToLower().Trim();

            if (lowerQuery == "how are you?")
            {
                return "I'm doing well, thank you for asking! Ready to help you with your cybersecurity questions.";
            }
            else if (lowerQuery == "what's your purpose?")
            {
                return "My purpose is to educate South African citizens about cybersecurity threats and how to protect themselves online.";
            }
            else if (lowerQuery == "what can i ask you about?")
            {
                return "You can ask me about topics like password safety, phishing scams, safe browsing habits, and how to recognize suspicious links.";
            }
            else if (lowerQuery.Contains("password"))
            {
                return "For strong passwords, try using a combination of uppercase and lowercase letters, numbers, and symbols. Aim for a password that is at least 12 characters long and avoid using personal information.";
            }
            else if (lowerQuery.Contains("phishing"))
            {
                return "Phishing is a type of online fraud where criminals try to trick you into revealing personal information, such as passwords or credit card details, often through fake emails or websites. Be cautious of unsolicited messages asking for sensitive information.";
            }
            else if (lowerQuery.Contains("safe browsing") || lowerQuery.Contains("suspicious links"))
            {
                return "When browsing, ensure websites have 'https://' in the address bar, indicating a secure connection. Be wary of suspicious links in emails or messages, and avoid clicking on them if you're unsure of their legitimacy.";
            }
            else if (string.IsNullOrWhiteSpace(query))
            {
                return "Please enter a question or type 'exit' to end the conversation.";
            }
            else
            {
                return "I didn't quite understand that. Could you rephrase?";
            }
        }

        static void AnimateText(string text, int delay = 30)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        [SupportedOSPlatform("windows")]
        static void Main(string[] args)
        {
            PlayVoiceGreeting("welcome.wav");
            DisplayAsciiArt();
            Console.WriteLine();
            Console.WriteLine("Welcome to the Cybersecurity Awareness Bot!");
            Console.WriteLine();

            Console.Write("Please enter your name: ");
            string? userName = Console.ReadLine();
            string greetingName = string.IsNullOrWhiteSpace(userName) ? "there" : userName;
            Console.WriteLine($"\nHello, {greetingName}! I'm here to help you learn about staying safe online.");

            while (true)
            {
                Console.WriteLine("\nWhat would you like to ask me? (Type 'exit' to quit)");
                string? userInput = Console.ReadLine();

                if (userInput?.ToLower() == "exit")
                {
                    Console.WriteLine("Thank you for chatting! Stay safe online.");
                    break;
                }

                string response = GetResponse(userInput);
                AnimateText(response);
            }
        }
    }
}