using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Survey
{
    public class SurveyDto
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int? CreatorId { get; set; }

        public int? CourseId { get; set; }
        [Required]
        [MinLength(1)]
        public List<QuestionDto> Questions { get; set; }

        public bool Active { get; set; } = false;

        public string CourseName { get; set; }

        public string CreatorName { get; set; }

        public DateTime? EndDate { get; set; }

    }
}