using StudentSurveySystem.Core.Models;

namespace StudentSurveySystemApi.Entities
{
    public class Question
    {
        public int? Id { get; set; }

        public string QuestionText { get; set; }

        public QuestionType QuestionType { get; set; }

        //values to choose in case of single or multi select converted to json
        public string Values { get; set; }

        public int SurveyId { get; set; }

        public Survey Survey { get; set; }

    }
}