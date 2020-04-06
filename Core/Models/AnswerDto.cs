﻿using StudentSurveySystem.Core.Models;

namespace Core.Models
{
    public class AnswerDto
    {
        public int? Id { get; set; }

        public string Value { get; set; }

        public string QuestionText { get; set; }

        public QuestionType QuestionType { get; set; }

        public int QuestionId { get; set; }
    }
}