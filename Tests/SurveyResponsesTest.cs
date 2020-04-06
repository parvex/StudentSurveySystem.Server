using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSurveySystem.Core.Models;
using StudentSurveySystemApi.Controllers;
using StudentSurveySystemApi.Entities;
using Xunit;

namespace Tests
{
    public class SurveyResponsesTest : TestBase
    {
        private SurveyResponsesController _controller;

        public SurveyResponsesTest() : base()
        {
            _controller = new SurveyResponsesController(context);
            //setting user context - created in TestBase
            _controller.ControllerContext = controllerContext;
        }

        [Fact]
        public async Task PostSurveyResponse()
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

            var result = await _controller.PostSurveyResponse(surveyResponse);
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
