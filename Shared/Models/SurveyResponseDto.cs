using System.Collections.Generic;

namespace StudentSurveySystem.Core.Models
{
    public class SurveyResponseDto
    {
        public int? Id { get; set; }

        public int RespondentId { get; set; }

        public List<AnswerDto> Answers { get; set; }

        public int SurveyId { get; set; }
    }
}