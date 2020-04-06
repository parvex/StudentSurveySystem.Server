using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models.Survey;
using Newtonsoft.Json;
using Server.Controllers;
using Server.Entities;
using Xunit;

namespace Tests.Controllers
{
    public class SurveyTest : TestBase
    {
        private SurveysController _controller;

        public SurveyTest()
        {
            _controller = new SurveysController(Context);
            _controller.ControllerContext = ControllerContext;
        }

        [Fact]
        public async Task AddSurvey()
        {
            var survey = new SurveyFormDto
            {
                Name = $"Test survey {Guid.NewGuid().ToString()}",
                CreatorId = 1,
                Questions = new List<QuestionDto>
                {
                    new QuestionDto { QuestionType = QuestionType.Text, QuestionText = "Name"},
                    new QuestionDto { QuestionType = QuestionType.Date, QuestionText = "Birth date"},
                    new QuestionDto { QuestionType = QuestionType.Numeric, QuestionText = "Height"},
                    new QuestionDto { QuestionType = QuestionType.Boolean, QuestionText = "Driving license"},
                    new QuestionDto
                    {
                        QuestionType = QuestionType.SingleSelect, QuestionText = "Colour",
                        Values = new List<string>{"Red", "Blue", "Black"}
                    },
                    new QuestionDto
                    {
                        QuestionType = QuestionType.MultipleSelect, QuestionText = "Known languages",
                        Values = new List<string>{"English", "Polish", "German", "Japanese", "French"}
                    }
                }
            };

            var response = _controller.AddSurvey(survey);
            Assert.NotNull(response.Result);
            Assert.Equal(6, response.Result.Value.Questions.Count);
            Assert.Equal(survey.Name, response.Result.Value.Name);
        }
    }
}