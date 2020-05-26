using System.Collections.Generic;

namespace Server.Models.SurveyResponse
{
    public class SurveyResponseDto
    {
        public int? Id { get; set; }

        public int RespondentId { get; set; }

        public List<AnswerDto> Answers { get; set; }

        public int SurveyId { get; set; }
    }
}