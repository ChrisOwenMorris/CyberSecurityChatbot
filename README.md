# Cybersecurity Awareness Chatbot

## Project Overview

This project implements a Cybersecurity Awareness Chatbot designed to provide users with essential tips and information to stay safe online. The chatbot is developed in C# and aims to be an interactive and educational tool for understanding and mitigating online risks.

## Features (Part 1 & Part 2)

### Part 1: Basic Chatbot
* **Greeting and Name Personalization:** The chatbot greets the user and asks for their name to provide a personalized experience.
* **Basic Interaction:** Responds to simple queries and provides a mechanism to exit the conversation.
* **Audio Welcome:** Plays a welcome audio file upon startup.
* **ASCII Art:** Displays an ASCII art logo for visual appeal.

### Part 2: Expanded Chatbot Functionality
* **Keyword Recognition:** Identifies specific cybersecurity-related topics (e.g., "password," "scam," "phishing", "privacy", "malware", "firewall", "encryption", "VPN", "2FA", "data breach") and provides relevant responses.
* **Random Responses:** For common cybersecurity queries, the chatbot randomly selects from multiple predefined informative responses to keep interactions varied and engaging.
* **Conversation Flow:** Maintains conversational continuity, handling follow-up questions effectively by remembering the current topic.
* **Memory and Recall:** Remembers key user inputs, such as the user's name and their favorite cybersecurity topic, to offer personalized advice and enhance engagement.
* **Sentiment Detection:** Detects and responds to user emotions or tone (e.g., "worried," "anxious," "confused", "frustrated", "curious", "concerned") with empathetic lead-in phrases.
* **Error Handling and Edge Cases:** Provides clear and helpful responses when the chatbot doesn't understand the user's input, guiding them back to relevant topics.
* **Code Structure and Optimisation:** The codebase is designed to be modular, well-organized, and maintainable, using dedicated classes for chatbot state (`Chatbot.cs`), responses (`ChatbotResponses.cs`), and processing logic (`ChatbotLogic.cs`).

## Setup Instructions

To set up and run this project on your local machine, follow these steps:

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download) (Version `9.0.x` or compatible, as specified in `main.yml`)
* [Visual Studio Code](https://code.visualstudio.com/) (Recommended IDE)
* [Git](https://git-scm.com/downloads) (For version control)

### 1. Clone the Repository
Open your terminal or command prompt and clone the repository:
```bash
git clone [https://github.com/ChrisOwenMorris/CyberSecurityChatbot.git](https://github.com/ChrisOwenMorris/CyberSecurityChatbot.git)
cd CybersecurityChatbot
git checkout part2-chatbot # Switch to the branch you are working on
