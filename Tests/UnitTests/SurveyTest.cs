using System;
using System.Collections.Generic;
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
                var controller = new SurveysController(context);
                //setting user context
                controller.ControllerContext = _fixture.ControllerContext;
                return controller;
            }
        }

        public SurveyTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task<int> AddSurvey()
        {
            var survey = new SurveyDto()
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
            var id = await AddSurvey();
            var survey = (await Controller.GetSurvey(id)).Value;
            survey.Name = "New test name" + Guid.NewGuid();
            survey.Questions[0].QuestionType = QuestionType.Date;
            var response = await Controller.PutSurvey(id, survey);
            var modifiedResponse = await Controller.GetSurvey(id);

            Assert.IsType<OkResult>(response);
            Assert.Equal(survey.Name, modifiedResponse.Value.Name);
            Assert.Equal(QuestionType.Date, modifiedResponse.Value.Questions[0].QuestionType);

        }

        [Fact]
        public async Task<int> ActivateSurvey()
        {
            var id = await AddSurvey();
            var response = await Controller.ActivateSurvey(id);

            Assert.IsType<OkResult>(response);
            var surveyResponse = await Controller.GetSurvey(id);
            Assert.True(surveyResponse.Value.Active);
            return surveyResponse.Value.Id.Value;
        }

        [Fact]
        public async Task DeactivateSurvey()
        {
            var id = await ActivateSurvey();
            var response = await Controller.DeactivateSurvey(id);

            Assert.IsType<OkResult>(response);
            var surveyResponse = await Controller.GetSurvey(id);
            Assert.False(surveyResponse.Value.Active);
        }

        [Fact]
        public async Task DeleteSurvey()
        {
            var id = await AddSurvey();
            var response = await Controller.DeleteSurvey(id);
            var detailsResponse = await Controller.GetSurvey(id);

            Assert.IsType<OkResult>(response);
            Assert.IsType<NotFoundResult>(detailsResponse.Result);
        }
    }
}