using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    public class Question
    {
        public string QuestionText { get; set; } // The text of the question
        public string QuestionType { get; set; } // "MCQ" for Multiple Choice, "TF" for True/False
        public string[] Options { get; set; }    // The options for MCQs
        public string CorrectAnswer { get; set; } // The correct answer for the question
    }
}
