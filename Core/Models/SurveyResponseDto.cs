using System.Collections.Generic;
using Core.Models;

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