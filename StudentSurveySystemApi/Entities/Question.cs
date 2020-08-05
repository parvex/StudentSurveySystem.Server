using System.Collections.Generic;
using Server.Models.Survey;

namespace Server.Entities
{
    public class Question
    {
        public int? Id { get; set; }

        public int Index { get; set; }

        public string QuestionText { get; set; }

        public bool Required { get; set; } = false;

        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// Validation config converted to json
        /// </summary>
        public string ValidationConfig { get; set; }

        /// <summary>
        /// values to choose in case of single or multi select converted to json
        /// </summary>
        public string Values { get; set; }

        public int SurveyId { get; set; }

        public Survey Survey { get; set; }

        public List<Answer> Answers { get; set; }
    }
}