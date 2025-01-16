using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace QuizApp
{
    public partial class MainWindow : Window
    {
        private List<Question> questions;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadQuiz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filePath = "questions.json"; // Path to the JSON file
                string json = File.ReadAllText(filePath);
                questions = JsonConvert.DeserializeObject<List<Question>>(json);
                MessageBox.Show($"Loaded {questions.Count} questions!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading quiz: {ex.Message}");
            }
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            if (questions == null || questions.Count == 0)
            {
                MessageBox.Show("Please load a quiz first!");
                return;
            }

            QuizWindow quizWindow = new QuizWindow(questions);
            quizWindow.Show();
            this.Close();
        }
    }
}
