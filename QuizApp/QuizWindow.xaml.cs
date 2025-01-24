using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace QuizApp
{
    public partial class QuizWindow : Window
    {
        private List<Question> questions;
        private int currentQuestionIndex = 0;
        private int score = 0;

        private List<int> skippedQuestions = new List<int>();


        private DispatcherTimer timer; // Timer for question countdown
        private int timeLeft = 15; // Time limit per question (in seconds)

        public QuizWindow(List<Question> questions)
        {
            InitializeComponent();

            // Shuffle the questions before starting the quiz
            this.questions = questions.OrderBy(q => Guid.NewGuid()).ToList();

            // Initialize and configure the timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Tick every second
            timer.Tick += Timer_Tick;

            LoadQuestion();
        }

        private void LoadQuestion()
        {
            // Check if the current question index exceeds the number of questions
            if (currentQuestionIndex >= questions.Count)
            {
                // If there are skipped questions, go back to them
                if (skippedQuestions.Count > 0)
                {
                    currentQuestionIndex = skippedQuestions[0]; // Get the first skipped question
                    skippedQuestions.RemoveAt(0); // Remove it from the skippedQuestions list
                }
                else
                {
                    MessageBox.Show($"Quiz finished! Your score: {score}/{questions.Count}");
                    this.Close();
                    return;
                }
            }

            var question = questions[currentQuestionIndex];
            QuestionTextBlock.Text = question.QuestionText;

            // Reset timer for the new question
            timeLeft = 15; // Reset to 15 seconds
            TimerTextBlock.Text = $"Time Left: {timeLeft}s"; // Update timer display
            timer.Start(); // Start the timer

            // Shuffle options if the question is MCQ
            if (question.QuestionType == "MCQ")
            {
                var shuffledOptions = question.Options.OrderBy(o => Guid.NewGuid()).ToList();

                Option1.Content = shuffledOptions[0];
                Option2.Content = shuffledOptions[1];
                Option3.Content = shuffledOptions[2];
                Option4.Content = shuffledOptions[3];

                // Ensure all options are visible for MCQ
                Option1.Visibility = Visibility.Visible;
                Option2.Visibility = Visibility.Visible;
                Option3.Visibility = Visibility.Visible;
                Option4.Visibility = Visibility.Visible;
            }
            // Handling True/False Questions
            else if (question.QuestionType == "TF")
            {
                Option1.Content = "True";
                Option2.Content = "False";

                // Hide options 3 and 4 for True/False questions
                Option3.Visibility = Visibility.Collapsed;
                Option4.Visibility = Visibility.Collapsed;
            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft--; // Decrement the time left
            TimerTextBlock.Text = $"Time Left: {timeLeft}s"; // Update timer display

            if (timeLeft <= 0)
            {
                timer.Stop(); // Stop the timer
                MessageBox.Show("Time's up for this question!");
                currentQuestionIndex++; // Move to the next question
                LoadQuestion(); // Load the next question
            }
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop(); // Stop the timer when manually moving to the next question

            string selectedAnswer = null;

            // Check which option is selected
            if (Option1.IsChecked == true) selectedAnswer = Option1.Content.ToString();
            if (Option2.IsChecked == true) selectedAnswer = Option2.Content.ToString();
            if (Option3.IsChecked == true && Option3.Visibility == Visibility.Visible) selectedAnswer = Option3.Content.ToString();
            if (Option4.IsChecked == true && Option4.Visibility == Visibility.Visible) selectedAnswer = Option4.Content.ToString();

            // Ensure an answer is selected
            if (selectedAnswer == null)
            {
                MessageBox.Show("Please select an answer!");
                return;
            }

            // Check if the selected answer matches the correct answer
            if (selectedAnswer == questions[currentQuestionIndex].CorrectAnswer)
                score++;

            // Move to the next question
            currentQuestionIndex++;
            LoadQuestion();
        }
        private void SkipQuestion_Click(object sender, RoutedEventArgs e)
        {
            // Add the current question to the skippedQuestions list
            skippedQuestions.Add(currentQuestionIndex);

            // Move to the next question
            currentQuestionIndex++;
            LoadQuestion();
        }

    }
}
