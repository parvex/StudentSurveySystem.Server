using System;
using System.Collections.Generic;

namespace Core.Models.Survey
{
    public class SurveyDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int? CreatorId { get; set; }

        public int? CourseId { get; set; }

        public List<QuestionDto> Questions { get; set; }

        public bool Active { get; set; } = false;

        public string CourseName { get; set; }

        public string CreatorName { get; set; }

        public DateTime? EndDate { get; set; }

    }
}