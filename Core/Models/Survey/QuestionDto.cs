using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Core.Models.Survey
{
    public class QuestionDto
    {
        public int? Id { get; set; }

        [Required]
        public int Index { get; set; }
        [Required]
        public string QuestionText { get; set; }
        [Required]
        public QuestionType QuestionType { get; set; }

        public ValidationConfig ValidationConfig { get; set; }

        /// <summary>
        /// Values to choose in case of single or multi select
        /// </summary>
        public List<string> Values { get; set; }
    }
}