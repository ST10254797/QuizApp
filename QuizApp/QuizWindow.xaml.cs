using System;
using System.Collections.Generic;
using System.Windows;

namespace QuizApp
{
    public partial class QuizWindow : Window
    {
        private List<Question> questions;
        private int currentQuestionIndex = 0;
        private int score = 0;

        public QuizWindow(List<Question> questions)
        {
            InitializeComponent();
            this.questions = questions;
            LoadQuestion();
        }

        private void LoadQuestion()
        {
            if (currentQuestionIndex >= questions.Count)
            {
                MessageBox.Show($"Quiz finished! Your score: {score}/{questions.Count}");
                this.Close();
                return;
            }

            var question = questions[currentQuestionIndex];
            QuestionTextBlock.Text = question.QuestionText;

            // Handling MCQ Questions
            if (question.QuestionType == "MCQ")
            {
                Option1.Content = question.Options[0];
                Option2.Content = question.Options[1];
                Option3.Content = question.Options[2];
                Option4.Content = question.Options[3];

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
        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
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

    }
}
