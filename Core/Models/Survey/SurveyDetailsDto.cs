using System.Collections.Generic;

namespace Core.Models.Survey
{
    public class SurveyDetailsDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Creator { get; set; }

        public int? CreatorId { get; set; }

        public string CourseName { get; set; }

        public List<QuestionDto> Questions { get; set; }
    }
}