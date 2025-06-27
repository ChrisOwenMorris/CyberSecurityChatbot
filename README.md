🤖 Cybersecurity Chatbot
Welcome to the Cybersecurity Chatbot! This is a desktop application developed with C# and WPF, designed to provide users with information, tips, and interactive tools related to cybersecurity.

✨ Features
Interactive Chat Interface: Engage in conversations about various cybersecurity topics.

Sentiment Detection: The chatbot can detect positive or negative sentiment in your messages and respond accordingly.

Contextual Understanding: Remembers the current topic of conversation for more relevant follow-up responses.

User Memory & Recall:

Remembers your name.

Allows you to set and recall a "favorite" cybersecurity topic.

Offers personalized tips based on your stated interests.

Task Management: Add, view, and delete cybersecurity-related tasks.

Reminders: Set date/time-based reminders for your tasks.

Interactive Quiz: Test your cybersecurity knowledge with a short quiz.

Activity Log: View a summary of your recent interactions and actions with the chatbot.

Basic NLP Simulation: Utilizes a custom NlpSimulator class for sentiment analysis, keyword extraction, and intent detection based on predefined rules.

🚀 Getting Started
To get a local copy up and running, follow these simple steps.

Prerequisites
Visual Studio: Visual Studio 2019 or newer is recommended (Community Edition is free).

.NET Framework 4.8 Developer Pack: Ensure you have the .NET Framework 4.8 targeting pack installed. You can typically add this component via the Visual Studio Installer.

Git: For cloning the repository.

Installation
Clone the repository:

git clone https://github.com/ChrisOwenMorris/CyberSecurityChatbot.git

Navigate to the project directory:

cd CyberSecurityChatbot/CyberSecurityChatbotUI/CyberSecurityChatbotUI

(Note: The project structure has a nested folder with the same name.)

Open in Visual Studio:
Open the CyberSecurityChatbotUI.csproj file in Visual Studio.

Restore NuGet Packages:
Visual Studio should automatically restore the necessary NuGet packages. If not, right-click on the solution in Solution Explorer and select "Restore NuGet Packages."

Build the project:
From the Visual Studio menu, go to Build > Build Solution. This will compile the application.

Run the application:
Press F5 or click the "Start" button in Visual Studio to run the chatbot.

💡 Usage
The chatbot will greet you with ASCII art and ask for your name first.

Core Commands:

General Conversation: Just type your questions or statements about cybersecurity topics (e.g., "What is phishing?", "Tell me about malware", "I'm worried about online privacy").

Set Favorite Topic: "I'm interested in [topic]" or "My favorite topic is [topic]" (e.g., "My favorite topic is encryption").

Recall Favorite Topic: "What's my favorite topic?" or "Do you remember my favorite topic?"

More Tips: "Any other tips?" or "More advice."

Add Task: Add task - [Task Description] (e.g., Add task - Review privacy settings)

Delete Task: Delete task [number] (e.g., Delete task 1 after viewing tasks)

Show Tasks: show tasks

Start Quiz: start quiz

Show Activity Log: show activity log or what have you done

Exit: Type exit (though typically closing the window works for WPF apps).

📁 Project Structure
App.xaml / App.xaml.cs: Application entry point and global resources.

MainWindow.xaml / MainWindow.xaml.cs: The main user interface and its code-behind logic.

ActivityLog.cs: Manages a log of chatbot activities.

Chatbot.cs: Static class for global chatbot state (like user name, current topic) and welcome audio.

ChatbotLogic.cs: Contains the core conversational logic, processing user input, and deciding responses based on detected intent and context.

ChatbotResponses.cs: Stores predefined responses, sentiment keywords, and cybersecurity topic mappings.

NlpSimulator.cs: Simulates basic NLP functions (sentiment analysis, keyword extraction, intent detection).

QuizManager.cs: Manages the cybersecurity quiz questions and scoring.

ReminderManager.cs: Handles setting and checking reminders for tasks.

TaskItem.cs: Defines the structure for a single task.

TaskManager.cs: Manages the collection of tasks.

welcome.wav: Audio file played on application startup.

.github/workflows/csharp-ci.yml: GitHub Actions workflow for continuous integration (CI) on .NET Framework.

.gitignore: Specifies files and directories to be ignored by Git.

🤝 Contributing
Contributions are welcome! If you have suggestions for improvements, new features, or bug fixes, feel free to:

Fork the repository.

Create a new branch (git checkout -b feature/YourFeatureName).

Make your changes.

Commit your changes (git commit -m 'feat: Add new feature').

Push to the branch (git push origin feature/YourFeatureName).

Open a Pull Request.

🙏 Acknowledgements
This Cybersecurity Chatbot was developed with significant assistance from:

Google Gemini AI: For code generation, logic structure, problem-solving, and architectural guidance.

OpenAI ChatGPT: For brainstorming ideas, refining responses, and enhancing conceptual understanding.
