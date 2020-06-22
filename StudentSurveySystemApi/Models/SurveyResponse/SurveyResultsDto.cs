using System;
using System.Collections.Generic;
using Server.Models.Survey;

namespace Server.Models.SurveyResponse
{
    public class SurveyResultsDto
    {
        public int SurveyId { get; set; }

        public string SurveyName { get; set; }

        public bool Anonymous { get; set; }

        public List<QuestionResultsDto> QuestionResults { get; set; }
    }

    public class QuestionResultsDto
    {
        public int QuestionIndex { get; set; }

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
        //answer
        public string Name { get; set; }

        //percent (this answer to all answers)
        public double Value { get; set; }

        //number of same answers
        public int NumberOfAnswers { get; set; }
    }
}