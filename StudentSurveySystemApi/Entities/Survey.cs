using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace StudentSurveySystemApi.Entities
{
    public class Survey
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int? CreatorId { get; set; }

        [ForeignKey(nameof(CreatorId))]
        public User Creator { get; set; }

        public List<Question> Questions { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public List<SurveyResponse> SurveyResponses { get; set; }
    }
}