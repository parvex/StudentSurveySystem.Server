﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.Survey
{
    public class QuestionDto
    {
        public int? Id { get; set; }

        [Required]
        public int Index { get; set; }
        [Required(ErrorMessage = "You must specify question text.")]
        public string QuestionText { get; set; }
        [Required]
        public QuestionType QuestionType { get; set; }

        public bool Required { get; set; } = false;

        public ValidationConfig ValidationConfig { get; set; } = new ValidationConfig();

        /// <summary>
        /// Values to choose in case of single or multi select
        /// </summary>
        public List<(string, double?)> Values { get; set; }
    }
}