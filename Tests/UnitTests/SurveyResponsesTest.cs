using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Core.Models.SurveyResponse;
using Microsoft.AspNetCore.Mvc;
using Server.Controllers;
using Server.Entities;
using Xunit;

namespace Tests.UnitTests
{
    [Collection("MainFixtureCollection")]
    public class SurveyResponsesTest
    {
        private SurveyResponsesController _controller;

        public SurveyResponsesTest(TestFixture fixture)
        {
            var context = new SurveyContext(fixture.Options);
            _controller = new SurveyResponsesController(context);
            //setting user context
            _controller.ControllerContext = fixture.ControllerContext;
        }

        [Fact]
        public async Task AddSurveyResponse()
        {
            var surveyResponse = new SurveyResponseDto
            {
                SurveyId = 0,
                RespondentId = 0,
                Answers = new List<AnswerDto> { 
                    new AnswerDto
                    {
                        QuestionId = 0,
                        Value = 4.ToString(),
                    },
                    new AnswerDto
                    {
                        QuestionId = 1,
                        Value = DateTime.Now.ToString(CultureInfo.InvariantCulture)
                    }
                }
            };

            var result = await _controller.AddSurveyResponse(surveyResponse);
            Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public async Task GetSurveyResponse()
        {
            var result = await _controller.GetSurveyResponse(0);
            Assert.Equal(2, result.Value.Answers.Count);
        }

        [Fact]
        public async Task GetSurveyResponses()
        {
            var result = await _controller.GetSurveyResponses();
            Assert.NotEmpty(result.Value);
        }

        [Fact]
        public async Task GetUserSurveyResponses()
        {
            var result = await _controller.GetUserSurveyResponses();
            Assert.NotEmpty(result.Value);
        }
    }
}
