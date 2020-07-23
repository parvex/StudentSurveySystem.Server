using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Entities
{
    public class SurveyResponse
    {
        public int? Id { get; set; }

        public int? RespondentId { get; set; }

        [ForeignKey(nameof(RespondentId))]
        public User Respondent { get; set; }

        public List<Answer> Answers { get; set; }

        public DateTime Date { get; set; }

        public int SurveyId { get; set; }

        //marks stamps that indicates that user has filled anonymous survey
        public bool IsStamp { get; set; }

        [ForeignKey(nameof(SurveyId))]
        public Survey Survey { get; set; }
    }
}