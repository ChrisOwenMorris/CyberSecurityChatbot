using System;
using System.Runtime.InteropServices;
using System.Media;

namespace CybersecurityChatbot
{
    public static class Chatbot
    {
        public static string UserName { get; set; } = "user";
        public static string UserFavoriteCybersecurityTopic { get; set; } = string.Empty;
        public static string CurrentTopic { get; set; } = string.Empty;

        // Instead of Console.WriteLine for the ASCII art, return string output
        public static string GetAsciiArt()
        {
            return @"
       .--.
      / _.-'--.
     / /  _ _  \
    | | ( ' ' ) |
    | |  `-'  | |
     \ \ '--' / /
      `._____.'


     _       _     _
    | |     | |   | |
    | | __ _| |__ | | __
    | |/ _` | '_ \| |/ /
    | | (_| | | | |   <
    |_|\__,_|_| |_|_|\_\


  _    _       _
 | |  | |     | |
 | |__| | __ _| |_ _ __
 |  __  |/ _` | __| '__|
 | |  | | (_| | |_| |
 |_|  |_|\__,_|\__|_|

----------------------------------------------------------------------";
        }

        public static void PlayWelcomeAudio()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                try
                {
                    var player = new SoundPlayer("welcome.wav");
                    player.PlaySync();
                }
                catch (Exception)
                {
                    // Could raise an event or log - for now do nothing or handle in GUI
                }
            }
            else
            {
                // Audio not supported, ignore or notify GUI as needed
            }
        }

        // Instead of Console.ReadLine/WriteLine, this method returns the greeting message and requests input via event
        public static string GetGreetingMessage()
        {
            return
@"╔══════════════════════════════════════════════════╗
║          Cybersecurity Awareness Bot             ║
╠══════════════════════════════════════════════════╣
║ I'm here to provide you with essential tips and  ║
║ information to stay safe in the digital world.   ║
╚══════════════════════════════════════════════════╝

Before we start, what's your name?";
        }

        // Call this from GUI after user inputs their name
        public static string ProcessUserNameInput(string input)
        {
            UserName = string.IsNullOrWhiteSpace(input) ? "Valued User" : input.Trim();
            return $"\nIt's great to meet you, {UserName}! How can I assist you with cybersecurity today?";
        }
    }
}
