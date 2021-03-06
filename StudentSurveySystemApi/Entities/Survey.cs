﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Internal;

namespace Server.Entities
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

        public bool Active { get; set; } = false;

        public bool IsTemplate { get; set; } = true;

        public bool Anonymous { get; set; } = false;

        public DateTime ModificationDate { get; set; }

        public List<SurveyResponse> SurveyResponses { get; set; }

        public DateTime? EndDate { get; set; }

        public SurveyStatus Status
        {
            get
            {
                if (!Active)
                    return SurveyStatus.Draft;
                if (Active && EndDate < DateTime.Now)
                    return SurveyStatus.Ended;
                else
                    return SurveyStatus.Active;
            }
        }
    }
}