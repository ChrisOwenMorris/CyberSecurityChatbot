// Chatbot.cs
using System; // Provides fundamental classes and base types for defining commonly used value and reference data types, events and event handlers, interfaces, attributes, and processing exceptions.
using System.Threading.Tasks; // For Task.Delay if you use it for voice – enables asynchronous operations.
using System.Runtime.InteropServices; // Required for OS platform check – allows interaction with unmanaged code and platform-specific features.
using System.Media; // Specifically for SoundPlayer to play .wav files on Windows.
// Project: Cybersecurity Chatbot
// Developed with assistance from:
// - Google Gemini AI (for code generation, logic structure, and problem-solving)
// - OpenAI ChatGPT (for brainstorming, refining responses, and understanding concepts)
//

namespace CybersecurityChatbot
{
    /// <summary>
    /// Static class to hold global chatbot state and core utility functions
    /// that are not directly part of the conversation logic or responses.
    /// This includes user specific data, display functions, and audio playback.
    /// </summary>
    public static class Chatbot
    {
        /// <summary>
        /// Stores the user's name for personalization. Defaults to "user".
        /// This property is updated during the greeting phase.
        /// </summary>
        public static string UserName { get; set; } = "user"; // Default name

        /// <summary>
        /// Stores the user's stated favorite cybersecurity topic.
        /// This is used for memory and personalized responses.
        /// Initialized as an empty string until a topic is set by the user.
        /// </summary>
        public static string UserFavoriteCybersecurityTopic { get; set; } = string.Empty;

        /// <summary>
        /// Stores the current cybersecurity topic being discussed.
        /// This helps maintain conversational context and flow for follow-up questions.
        /// Initialized as an empty string.
        /// </summary>
        public static string CurrentTopic { get; set; } = string.Empty;

        /// <summary>
        /// Displays an ASCII art logo to the console with multiple colors, representing "CYBERSECURITY BOT".
        /// Enhances the visual appeal of the chatbot's startup.
        /// </summary>
        public static void DisplayAsciiArt()
        {
            Console.WriteLine(); // Adds a blank line for spacing before the art.

            // Top Part: Shield icon (in DarkYellow)
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"
       .--.
      / _.-'--.
     / /  _ _  \
    | | ( ' ' ) |
    | |  `-'  | |
     \ \ '--' / /
      `._____.'
    ");

            // Middle Part: "CYBERSECURITY" (in Green)
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
     _       _     _
    | |     | |   | |
    | | __ _| |__ | | __
    | |/ _` | '_ \| |/ /
    | | (_| | | | |   <
    |_|\__,_|_| |_|_|\_\
 ");

            // Bottom Part: "BOT" (in Magenta)
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
  _    _       _
 | |  | |     | |
 | |__| | __ _| |_ _ __
 |  __  |/ _` | __| '__|
 | |  | | (_| | |_| |
 |_|  |_|\__,_|\__|_|
 ");

            Console.ResetColor(); // Resets the console text color to default after the ASCII art.
            Console.WriteLine("\n" + new string('-', 70)); // Adds a decorative separator line below the art.
        }

        /// <summary>
        /// Attempts to play a welcome audio file (`welcome.wav`).
        /// This function is platform-dependent and only works on Windows.
        /// Includes error handling for cases where the file is not found or other issues occur.
        /// </summary>
        public static void PlayWelcomeAudio()
        {
            // Checks if the operating system is Windows, as System.Media.SoundPlayer is Windows-specific.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                try
                {
                    // Creates a new SoundPlayer instance to play the 'welcome.wav' file.
                    var player = new System.Media.SoundPlayer("welcome.wav");
                    // Plays the sound synchronously, meaning the program waits for the sound to finish.
                    player.PlaySync();
                }
                catch (Exception ex)
                {
                    // Catches any exceptions during audio playback (e.g., file not found).
                    Console.ForegroundColor = ConsoleColor.Red; // Sets error message color to red.
                    Console.WriteLine($"Error playing welcome audio: {ex.Message}. Make sure 'welcome.wav' is in the correct directory.");
                    Console.ResetColor(); // Resets console color.
                }
            }
            else
            {
                // Informs the user if the OS is not Windows that audio playback is not supported.
                Console.ForegroundColor = ConsoleColor.Yellow; // Sets warning message color to yellow.
                Console.WriteLine("Welcome audio is only supported on Windows.");
                Console.ResetColor(); // Resets console color.
            }
        }

        /// <summary>
        /// Displays an introductory greeting message to the user with an enhanced border and colors,
        /// and prompts for their name. Stores the entered name for personalization throughout the conversation.
        /// </summary>
        public static void GreetUser()
        {
            // Top border for the greeting box using extended ASCII characters for a framed look.
            Console.WriteLine("╔══════════════════════════════════════════════════╗");
            Console.ForegroundColor = ConsoleColor.Cyan; // Color for the main title of the bot.
            Console.WriteLine("║          Cybersecurity Awareness Bot          ║");
            Console.ResetColor(); // Reset color after title for consistent formatting.
            // Middle separator for the greeting box.
            Console.WriteLine("╠══════════════════════════════════════════════════╣");
            Console.ForegroundColor = ConsoleColor.Green; // Color for the descriptive text inside the box.
            Console.WriteLine("║ I'm here to provide you with essential tips and   ║");
            Console.WriteLine("║ information to stay safe in the digital world.    ║");
            Console.ResetColor(); // Reset color after descriptive text.
            // Bottom border for the greeting box.
            Console.WriteLine("╚══════════════════════════════════════════════════╝");
            Console.WriteLine(); // Add a blank line for visual spacing after the greeting box.

            Console.ForegroundColor = ConsoleColor.Yellow; // Sets the color for the name prompt to make it stand out.
            Console.Write("Before we start, what's your name? "); // Prompts the user for their name.
            var input = Console.ReadLine(); // Reads the entire line of input from the console.

            // Assigns the user's name. If the input is empty or consists only of white-space characters,
            // it defaults to "Valued User" for a friendly, non-empty name. Otherwise, it uses the trimmed input.
            UserName = string.IsNullOrWhiteSpace(input) ? "Valued User" : input.Trim();

            // Provides a personalized welcome message using the stored name, enhancing user engagement.
            Console.WriteLine($"\nIt's great to meet you, {UserName}! How can I assist you with cybersecurity today?");
            Console.ResetColor(); // Resets console color to default after the personalized greeting.
        }
    }
}