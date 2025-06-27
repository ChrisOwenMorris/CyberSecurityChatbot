using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSecurityChatbotUI
{
    public class QuizManager
    {
        private List<QuizQuestion> questions;
        private int currentIndex;
        private int score;
        public bool IsActive { get; private set; }

        public QuizManager()
        {
            LoadQuestions();
            ResetQuiz();
        }

        public void ResetQuiz()
        {
            currentIndex = 0;
            score = 0;
            IsActive = false;
        }

        public void StartQuiz()
        {
            ResetQuiz();
            IsActive = true;
        }

        public QuizQuestion GetCurrentQuestion()
        {
            if (currentIndex < questions.Count)
                return questions[currentIndex];
            return null;
        }

        public bool SubmitAnswer(string answer)
        {
            if (!IsActive) return false;

            var current = questions[currentIndex];
            bool correct = string.Equals(answer.Trim(), current.Answer, StringComparison.OrdinalIgnoreCase);
            if (correct) score++;

            currentIndex++;
            if (currentIndex >= questions.Count)
                IsActive = false;

            return correct;
        }

        public int Score => score;
        public int TotalQuestions => questions.Count;
        public bool QuizFinished => !IsActive;

        private void LoadQuestions()
        {
            questions = new List<QuizQuestion>
            {
                new QuizQuestion(
                    "What should you do if you receive an email asking for your password?",
                    new List<string>{ "Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it" },
                    "C",
                    "Reporting phishing emails helps prevent scams."
                ),
                new QuizQuestion(
                    "True or False: You should use the same password for multiple accounts.",
                    new List<string>{ "True", "False" },
                    "B",
                    "False. Using unique passwords for different accounts increases security."
                ),
                new QuizQuestion(
                    "Which of the following is the strongest password?",
                    new List<string>{ "password123", "Qwerty", "P@ssw0rd!2025", "123456" },
                    "C",
                    "Passwords with a combination of letters, numbers, and symbols are stronger."
                ),
                new QuizQuestion(
                    "What does two-factor authentication (2FA) provide?",
                    new List<string>{ "An extra layer of security", "Faster login", "Password recovery", "No benefit" },
                    "A",
                    "2FA adds an extra verification step, making accounts harder to hack."
                ),
                new QuizQuestion(
                    "True or False: Public Wi-Fi networks are always safe to use without precautions.",
                    new List<string>{ "True", "False" },
                    "B",
                    "False. Public Wi-Fi can be insecure and expose your data."
                ),
                new QuizQuestion(
                    "What is phishing?",
                    new List<string>{ "A type of malware", "A social engineering attack to steal information", "A firewall", "A password manager" },
                    "B",
                    "Phishing tricks users into giving sensitive information."
                ),
                new QuizQuestion(
                    "How often should you update your software to stay secure?",
                    new List<string>{ "Only when it stops working", "Regularly with updates", "Once a year", "Never" },
                    "B",
                    "Regular updates patch security vulnerabilities."
                ),
                new QuizQuestion(
                    "True or False: Clicking on unknown email links is safe if it looks professional.",
                    new List<string>{ "True", "False" },
                    "B",
                    "False. Always verify links before clicking to avoid scams."
                ),
                new QuizQuestion(
                    "Which is the safest way to store passwords?",
                    new List<string>{ "Write on paper", "Use the same password everywhere", "Use a password manager", "Save in a text file" },
                    "C",
                    "Password managers securely store and manage your passwords."
                ),
                new QuizQuestion(
                    "What should you do if you suspect your account is compromised?",
                    new List<string>{ "Ignore it", "Change your password immediately", "Share your password with friends", "Delete your account" },
                    "B",
                    "Changing your password helps protect your account from further access."
                )
            };
        }
    }

    public class QuizQuestion
    {
        public string Question { get; }
        public List<string> Options { get; }
        public string Answer { get; }
        public string Explanation { get; }

        public QuizQuestion(string question, List<string> options, string answer, string explanation)
        {
            Question = question;
            Options = options;
            Answer = answer;
            Explanation = explanation;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Question);
            char optionChar = 'A';
            foreach (var opt in Options)
            {
                sb.AppendLine($"{optionChar}) {opt}");
                optionChar++;
            }
            return sb.ToString();
        }
    }
}
