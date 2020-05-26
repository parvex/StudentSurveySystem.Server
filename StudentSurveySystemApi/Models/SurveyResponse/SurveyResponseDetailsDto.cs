using System;
using System.Collections.Generic;

namespace Server.Models.SurveyResponse
{
    public class SurveyResponseDetailsDto
    {
        public int? Id { get; set; }

        public int RespondentId { get; set; }

        public List<AnswerDto> Answers { get; set; }

        public string SurveyName { get; set; }

        public string Respondent { get; set; }

        public DateTime Date { get; set; }

        public string Creator { get; set; }

        public string CourseName { get; set; }

        public int SurveyId { get; set; }
    }
}