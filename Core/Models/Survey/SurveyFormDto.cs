using System.Collections.Generic;

namespace Core.Models.Survey
{
    public class SurveyFormDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int? CreatorId { get; set; }

        public int? CourseId { get; set; }

        public List<QuestionDto> Questions { get; set; }
    }
}