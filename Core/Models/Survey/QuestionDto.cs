using System.Collections.Generic;

namespace Core.Models.Survey
{
    public class QuestionDto
    {
        public int? Id { get; set; }

        public int Index { get; set; }

        public string QuestionText { get; set; }

        public QuestionType QuestionType { get; set; }

        public ValidationConfig ValidationConfig { get; set; }

        /// <summary>
        /// Values to choose in case of single or multi select
        /// </summary>
        public List<string> Values { get; set; }
    }
}