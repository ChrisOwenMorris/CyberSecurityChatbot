// Program.cs
using System;
using System.Media; // Potentially still needed for SoundPlayer if not fully moved


namespace CybersecurityChatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part 1 implementations, now called from Chatbot.cs
            Chatbot.PlayWelcomeAudio();
            Chatbot.DisplayAsciiArt();
            Chatbot.GreetUser();

            // Main chatbot loop
            while (true)
            {
                Console.Write($"\n{Chatbot.UserName}> "); // Personalized prompt
                string userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please type something. How can I help you today?");
                    Console.ResetColor();
                    continue;
                }

                string lowerInput = userInput.ToLower().Trim(); // Clean input

                if (lowerInput == "exit" || lowerInput == "bye" || lowerInput == "quit")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\nGoodbye, {Chatbot.UserName}! Stay safe online and remember to always be vigilant!");
                    Console.ResetColor();
                    break;
                }

                // This is where Part 2 logic will be integrated
                string response = ChatbotLogic.ProcessUserInput(userInput); // We will create ChatbotLogic next
                Console.ForegroundColor = ConsoleColor.White; // Default bot response color
                Console.WriteLine(response);
                Console.ResetColor();
            }
        }
    }
}