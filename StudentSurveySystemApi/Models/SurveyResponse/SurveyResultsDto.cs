using System;
using System.Collections.Generic;
using Server.Models.Survey;

namespace Server.Models.SurveyResponse
{
    public class SurveyResultsDto
    {
        public int SurveyId { get; set; }

        public bool Anonymous { get; set; }

        public List<QuestionResultsDto> QuestionResults { get; set; }
    }

    public class QuestionResultsDto
    {
        public int QuestionId { get; set; }

        public string QuestionText { get; set; }

        public QuestionType QuestionType { get; set; }

        public List<QuestionAnswerDto> QuestionAnswers { get; set; }

        public List<AnswerPercentage> AnswerPercentages { get; set; }

        public double Mean { get; set; }

    }

    public class QuestionAnswerDto
    {
        public int RespondentId { get; set; }

        public string Respondent { get; set; }

        public object Value { get; set; }
    }

    public class AnswerPercentage
    {
        public string Answer { get; set; }

        public double PercentOfAnswers { get; set; }

        public int NumberOfAnswers { get; set; }
    }
}