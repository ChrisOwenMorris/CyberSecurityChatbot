// Chatbot.cs
using System;
using System.Threading.Tasks; // For Task.Delay if you use it for voice
using System.Runtime.InteropServices; // Required for OS platform check

namespace CybersecurityChatbot
{
    public static class Chatbot
    {
        public static string UserName { get; set; } = "user"; // Default name
        public static string UserFavoriteCybersecurityTopic { get; set; } = string.Empty;
        public static string CurrentTopic { get; set; } = string.Empty;

        public static void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
              ____ ______ _______ _______ _____ _________________
             / __ \ __ \__ ____|_ ____|__ __|_ _ ____ ____       
            / / \ \/ _ | / __| / ___| / / | | | _ \| _ \      
           / ___ |/ ____| | | |____ / / | | | | | | | |     
          /_/ |_|_/ /_____|___ |_____| /_/ |___|_| |_|_|    
            ");
            Console.ResetColor();
            Console.WriteLine("\n" + new string('-', 70));
        }

        public static void PlayWelcomeAudio()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                try
                {
                    var player = new System.Media.SoundPlayer("welcome.wav");
                    player.PlaySync();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error playing welcome audio: {ex.Message}. Make sure 'welcome.wav' is in the correct directory.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Welcome audio is only supported on Windows.");
                Console.ResetColor();
            }
        }

        public static void GreetUser()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + new string('=', 70));
            Console.WriteLine("Welcome to the Cybersecurity Awareness Bot!");
            Console.WriteLine("I'm here to provide you with essential tips and information to stay safe online.");
            Console.WriteLine(new string('=', 70) + "\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Before we start, what's your name? ");
            var input = Console.ReadLine();
            UserName = string.IsNullOrWhiteSpace(input) ? "Valued User" : input.Trim();
            Console.WriteLine($"\nIt's great to meet you, {UserName}! How can I assist you with cybersecurity today?");
            Console.ResetColor();
        }
    }
}
