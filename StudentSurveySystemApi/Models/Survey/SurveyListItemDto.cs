using System;

namespace Server.Models.Survey
{
    public class SurveyListItemDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; } = false;

        public bool IsTemplate { get; set; } = false;

        public bool Anonymous { get; set; } = false;

        public string CourseName { get; set; }

        public string CreatorName { get; set; }

        public DateTime EndDate { get; set; }
    }
}