// Program.cs
using System;

namespace CybersecurityChatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            // Welcome audio and art
            Chatbot.PlayWelcomeAudio();
            Chatbot.DisplayAsciiArt();
            Chatbot.GreetUser();

            // Chat loop
            while (true)
            {
                Console.Write($"\n{Chatbot.UserName}> ");
                string? userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please type something. How can I help you today?");
                    Console.ResetColor();
                    continue;
                }

                string lowerInput = userInput.ToLower().Trim();

                if (lowerInput == "exit" || lowerInput == "bye" || lowerInput == "quit")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\nGoodbye, {Chatbot.UserName}! Stay safe online and remember to always be vigilant!");
                    Console.ResetColor();
                    break;
                }

                string response = ChatbotLogic.ProcessUserInput(userInput);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(response);
                Console.ResetColor();
            }
        }
    }
}
