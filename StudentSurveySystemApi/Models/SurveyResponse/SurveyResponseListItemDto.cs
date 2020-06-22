using System;

namespace Server.Models.SurveyResponse
{
    public class SurveyResponseListItemDto
    {
        public int? Id { get; set; }

        public int RespondentId { get; set; }

        public string SurveyName { get; set; }

        public string Respondent { get; set; }

        public DateTime Date { get; set; }

        public string Creator { get; set; }

        public string CourseName { get; set; }

    }
}