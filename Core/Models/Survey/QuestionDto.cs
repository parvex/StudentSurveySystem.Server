using System.Collections.Generic;

namespace Core.Models.Survey
{
    public class QuestionDto
    {
        public int? Id { get; set; }

        public string QuestionText { get; set; }

        public QuestionType QuestionType { get; set; }

        //values to choose in case of single or multi select
        public List<string> Values { get; set; }
    }
}