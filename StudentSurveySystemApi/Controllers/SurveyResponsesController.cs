using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Entities;
using Server.Helpers;
using Server.Models.Survey;
using Server.Models.SurveyResponse;

namespace Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SurveyResponsesController : ControllerBase
    {
        private readonly SurveyContext _context;

        public SurveyResponsesController(SurveyContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin,Lecturer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyResponseDetailsDto>>> GetSurveyResponses(string name = "", int page = 0, int count = 20)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var query = _context.SurveyResponses.Where(x => x.Survey.Name.Contains(name ?? ""));
            switch (role)
            {
                case "Lecturer":
                    var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
                    query = query.Where(x => x.Survey.CreatorId == userId);
                    break;
                case "Admin":
                    break;
                default:
                    return BadRequest("Wrong role");
            }

            return await query.OrderByDescending(x => x.Survey.ModificationDate)
                .Skip(count * page).Take(count)
                .ProjectToType<SurveyResponseDetailsDto>()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyResponseDto>> GetSurveyResponse(int id)
        {
            var surveyResponse = await _context.SurveyResponses.ProjectToType<SurveyResponseDto>().FirstAsync(x => x.Id == id);

            if (surveyResponse == null)
            {
                return NotFound();
            }

            return surveyResponse;
        }

        [HttpGet("MyCompleted")]
        public async Task<ActionResult<IEnumerable<SurveyResponseDetailsDto>>> GetMyCompletedSurveyResponses(string name = "", int page = 0, int count = 20)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.SurveyResponses.Where(x => x.RespondentId == userId && x.Survey.Name.Contains(name ?? ""))
                .OrderByDescending(x => x.Survey.ModificationDate)
                .Skip(count * page).Take(count)
                .ProjectToType<SurveyResponseDetailsDto>()
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<SurveyResponseDto>> AddSurveyResponse(SurveyResponseDto surveyResponse)
        {
            var errorDictionary = ValidateSurveyResponse(surveyResponse);

            if (errorDictionary.Count > 0)
                return BadRequest(new {errors = errorDictionary, code = 400});

            var surveyResponseEntity = surveyResponse.Adapt<SurveyResponse>();
            var survey = await _context.Surveys.FirstAsync(x => x.Id == surveyResponse.SurveyId);

            if(survey.Anonymnous)
                surveyResponseEntity.RespondentId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            surveyResponseEntity.Date = DateTime.Now;
            _context.SurveyResponses.Add(surveyResponseEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private Dictionary<string, List<string>> ValidateSurveyResponse(SurveyResponseDto response)
        {
            var errors = new Dictionary<string, List<string>>();
            foreach (var answer in response.Answers)
            {
                var question = _context.Questions.First(x => x.Id == answer.QuestionId).Adapt<QuestionDto>();
                switch (answer.QuestionType)
                {
                    case QuestionType.Text:
                        var regexString = question.ValidationConfig.Regex;
                        if(question.ValidationConfig.Regex != null) 
                            if (regexString == null) 
                                break;
                        regexString = "^" + regexString + "$";
                        if (answer.Value == null)
                        {
                            errors[question.Index.ToString()] = new List<string> { $"{question.Index}. Text cannot be empty" };
                            break;
                        }
                        var match = Regex.Match(answer.Value, regexString);
                        if(!match.Success)
                            errors[question.Index.ToString()] = new List<string>{ $"{question.Index}. Text doesn't match criteria - {regexString}" };
                        break;
                    case QuestionType.Numeric:
                        var minNumeric = question.ValidationConfig.MinNumericValue;
                        var maxNumeric = question.ValidationConfig.MaxNumericValue;
                        if (answer.Value != null && !double.TryParse(answer.Value, out var n))
                        {
                            errors[question.Index.ToString()] = new List<string> { $"{question.Index}. Value is not valid number" };
                            break;
                        }
                        if (!minNumeric.HasValue && !maxNumeric.HasValue) 
                            break;
                        var number = double.TryParse(answer.Value, out var tempVal) ? tempVal : (double?)null;
                        if (!number.HasValue || (minNumeric.HasValue && number < minNumeric) || (maxNumeric.HasValue && number > maxNumeric))
                        {
                            errors[question.Index.ToString()] = new List<string> { $"{question.Index}. Number should be " + (minNumeric.HasValue ? "from " + minNumeric : null) + (maxNumeric.HasValue ? " to " + maxNumeric : null) };

                        }
                        break;
                    case QuestionType.Date:
                        var minDate = question.ValidationConfig.MinDateValue;
                        var maxDate = question.ValidationConfig.MaxDateValue;
                        if (answer.Value != null && !DateTime.TryParse(answer.Value, out var n2))
                        {
                            errors[question.Index.ToString()] = new List<string> { $"{question.Index}. Value is not valid date" };
                            break;
                        }
                        if (!minDate.HasValue && !maxDate.HasValue)
                            break;
                        var date = DateTime.TryParse(answer.Value, out var tempVal2) ? tempVal2 : (DateTime?)null;
                        if (!date.HasValue || (minDate.HasValue && date < minDate) || (maxDate.HasValue && date > maxDate))
                        {
                            errors[question.Index.ToString()] = new List<string> { $"{question.Index}. Date should be " + (minDate.HasValue ? "from " + minDate : null) + (maxDate.HasValue ? " to " + maxDate : null) };

                        }
                        break;
                }

            }
            return errors;
        }
    }
}
