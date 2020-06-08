using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.Survey
{
    public class SurveyDto
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Survey title is required.")]
        public string Name { get; set; }

        public int? CreatorId { get; set; }

        public int? CourseId { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "You must add at least 1 question.")]
        public List<QuestionDto> Questions { get; set; }

        public bool Active { get; set; } = false;

        public bool IsTemplate { get; set; } = false;

        public bool Anonymous { get; set; } = false;

        public string CourseName { get; set; }

        public string CreatorName { get; set; }

        public DateTime EndDate { get; set; }

    }
}