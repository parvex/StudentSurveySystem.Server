using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FoolProof.Core;
using Server.Migrations;

namespace Server.Models.Survey
{
    public class SurveyDto
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Survey title is required.")]
        public string Name { get; set; }

        public int? CreatorId { get; set; }

        [Required(ErrorMessage = "Course must be specified")]
        public int? CourseId { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "You must add at least 1 question.")]
        public List<QuestionDto> Questions { get; set; }

        public bool Active { get; set; } = false;

        public bool IsTemplate { get; set; } = false;

        public bool Anonymous { get; set; } = false;

        public string CourseName { get; set; }

        public string CreatorName { get; set; }

        [GreaterThan(nameof(Today), ErrorMessage = "End date must be in future")]
        public DateTime EndDate { get; set; }

        public DateTime Today => DateTime.Today;

        public bool? Ended { get; set; }
    }
}