using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Controllers;
using Server.Entities;
using Server.Models.Survey;
using Xunit;

namespace Tests.UnitTests
{
    [Collection("MainFixtureCollection")]
    public class SurveyTest
    {
        private readonly TestFixture _fixture;
        private SurveysController Controller
        {
            get
            {
                var context = new SurveyContext(_fixture.Options);
                var controller = new SurveysController(context, _fixture.NotificationService);
                //setting user context
                controller.ControllerContext = _fixture.ControllerContext;
                return controller;
            }
        }

        public SurveyTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task<int> AddSurvey(bool isTemplate)
        {
            var survey = new SurveyDto()
            {
                Name = $"Test survey {Guid.NewGuid()}",
                CreatorId = 1,
                Questions = new List<QuestionDto>
                {
                    new QuestionDto { QuestionType = QuestionType.Text, QuestionText = "Name", Index = 1},
                    new QuestionDto { QuestionType = QuestionType.Date, QuestionText = "Birth date", Index = 2},
                    new QuestionDto { QuestionType = QuestionType.Numeric, QuestionText = "Height", Index = 3},
                    new QuestionDto { QuestionType = QuestionType.Boolean, QuestionText = "Driving license", Index = 4},
                    new QuestionDto
                    {
                        QuestionType = QuestionType.SingleSelect, QuestionText = "Colour",
                        Values = new List<(string, double?)>{("Red", null), ("Blue", null), ("Black", null)},
                        Index = 5
                    },
                    new QuestionDto
                    {
                        QuestionType = QuestionType.MultipleSelect, QuestionText = "Known languages",
                        Values = new List<(string, double?)>{("English", null), ("Polish", null), ("German", null), ("Japanese", null), ("French", null)},
                        Index = 6
                    }
                },
                IsTemplate = isTemplate
            };

            var response = await Controller.AddSurvey(survey);
            Assert.NotNull(response.Value);
            Assert.Equal(6, response.Value.Questions.Count);
            Assert.Equal(survey.Name, response.Value.Name);
            Assert.False(response.Value.Active);
            return response.Value.Id.Value;
        }


        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task<SurveyDto> GetSurvey(int id)
        {
            var response = await Controller.GetSurvey(id);
            Assert.NotNull(response.Value);
            return response.Value;
        }

        [Fact]
        public async Task UpdateSurvey()
        {
            var id = await AddSurvey(false);
            var survey = (await Controller.GetSurvey(id)).Value;
            survey.Name = "New test name" + Guid.NewGuid();
            survey.Questions.First(x => x.Index == 1).QuestionType = QuestionType.Date;
            var response = await Controller.PutSurvey(id, survey);
            var modifiedResponse = await Controller.GetSurvey(id);

            Assert.IsType<EmptyResult>(response);
            Assert.Equal(survey.Name, modifiedResponse.Value.Name);
            Assert.Equal(QuestionType.Date, modifiedResponse.Value.Questions.First(x => x.Index == 1).QuestionType);

        }

        [Fact]
        public async Task DeleteSurvey()
        {
            var id = await AddSurvey(false);
            var response = await Controller.DeleteSurvey(id);
            var detailsResponse = await Controller.GetSurvey(id);

            Assert.IsType<OkResult>(response);
            Assert.IsType<NotFoundResult>(detailsResponse.Result);
        }

        [Fact]
        public async Task StartActiveSurveyFromTemplate()
        {
            var id = await AddSurvey(true);
            var surveyResponse = await Controller.GetSurvey(id);
            var response = await Controller.StartSurveyFromTemplate(surveyResponse.Value);

            Assert.IsType<OkResult>(response.Result);
        }
    }
}