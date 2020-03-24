using System.Collections.Generic;

namespace StudentSurveySystem.Core.Models
{
    public class SurveyDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Creator { get; set; }

        public string CourseName { get; set; }

        public List<QuestionDto> Questions { get; set; }
    }
}