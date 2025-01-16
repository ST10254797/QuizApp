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

            if (question.QuestionType == "MCQ")
            {
                Option1.Content = question.Options[0];
                Option2.Content = question.Options[1];
                Option3.Content = question.Options[2];
                Option4.Content = question.Options[3];

                Option1.Visibility = Visibility.Visible;
                Option2.Visibility = Visibility.Visible;
                Option3.Visibility = Visibility.Visible;
                Option4.Visibility = Visibility.Visible;
            }
            else if (question.QuestionType == "TF")
            {
                Option1.Content = "True";
                Option2.Content = "False";

                Option3.Visibility = Visibility.Collapsed;
                Option4.Visibility = Visibility.Collapsed;
            }
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            string selectedAnswer = null;

            if (Option1.IsChecked == true) selectedAnswer = Option1.Content.ToString();
            if (Option2.IsChecked == true) selectedAnswer = Option2.Content.ToString();
            if (Option3.IsChecked == true) selectedAnswer = Option3.Content.ToString();
            if (Option4.IsChecked == true) selectedAnswer = Option4.Content.ToString();

            if (selectedAnswer == null)
            {
                MessageBox.Show("Please select an answer!");
                return;
            }

            if (selectedAnswer == questions[currentQuestionIndex].CorrectAnswer)
                score++;

            currentQuestionIndex++;
            LoadQuestion();
        }
    }
}
