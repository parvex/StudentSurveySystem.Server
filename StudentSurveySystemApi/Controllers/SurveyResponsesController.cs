using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Newtonsoft.Json;
using Server.Entities;
using Server.Extensions;
using Server.Helpers;
using Server.Models.Survey;
using Server.Models.SurveyResponse;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SurveyResponsesController : ControllerBase
    {
        private readonly SurveyContext _context;
        private readonly IPushNotificationService _pushNotificationService;

        public SurveyResponsesController(SurveyContext context, IPushNotificationService pushNotificationService)
        {
            _context = context;
            _pushNotificationService = pushNotificationService;
        }

        [HttpGet("MySurveyResults")]
        [Authorize(Roles = "Admin,Lecturer")]
        public async Task<ActionResult<List<SurveyListItemDto>>> GetSurveyResultList(string name = "", int page = 0, int count = 20)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.Surveys.Where(x => !x.IsTemplate && x.Name.Contains(name ?? "") && x.CreatorId == userId && x.SurveyResponses.Any())
                .OrderByDescending(x => x.ModificationDate)
                .Skip(count * page).Take(count)
                .ProjectToType<SurveyListItemDto>()
                .ToListAsync();
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Lecturer")]
        public async Task<ActionResult<IEnumerable<SurveyResponseListItemDto>>> GetSurveyResponses(string name = "", int? surveyId = null, int page = 0, int count = 20)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var query = _context.SurveyResponses.Where(x => (surveyId == null || x.SurveyId == surveyId) && !x.IsStamp && x.Survey.Name.Contains(name ?? ""));
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
                .ProjectToType<SurveyResponseListItemDto>()
                .ToListAsync();
        }        
        
        [HttpGet("details/{id}")]
        public async Task<ActionResult<SurveyResponseDetailsDto>> GetSurveyResponseDetails(int id)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var query = _context.SurveyResponses.Where(x => x.Id == id);

            if (role == "Lecturer")
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
                query = query.Where(x => x.Survey.CreatorId == userId);
            }
            else if (role == "Student")
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
                query = query.Where(x => x.RespondentId == userId);
            }

            var response = await query.ProjectToType<SurveyResponseDetailsDto>().FirstOrDefaultAsync();

            foreach (var answer in response.Answers)
            {
                switch (answer.QuestionType)
                {
                    case QuestionType.SingleSelect:
                    {
                        if(answer.Value != null)
                            answer.Value = JsonConvert.DeserializeObject<(string, double?)>(answer.Value).Item1;
                        break;
                    }
                    case QuestionType.MultipleSelect:
                    {
                        if (answer.Value != null)
                            answer.Value = string.Join(", " , JsonConvert.DeserializeObject<List<(string, double?)>>(answer.Value).Select(x => x.Item1).ToList());
                        break;
                    }
                    case QuestionType.ValuedSingleSelect:
                    {
                        if (answer.Value != null)
                        {
                            var tuple = JsonConvert.DeserializeObject<(string, double?)>(answer.Value);
                            answer.Value = tuple.Item1 + " : " + tuple.Item2;
                        }
                        break;
                    }
                }
            }

            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyResponseDetailsDto>> GetSurveyResponse(int id)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            var query = _context.SurveyResponses.Where(x => x.Id == id);

            switch (role)
            {
                case "Student":
                    query.Where(x => x.RespondentId == userId);
                    break;
                case "Lecturer":
                    query.Where(x => x.Survey.CreatorId == userId);
                    break;
            }

            return await query.ProjectToType<SurveyResponseDetailsDto>().FirstOrDefaultAsync();
        }

        [HttpGet("MyCompleted")]
        public async Task<ActionResult<IEnumerable<SurveyResponseListItemDto>>> GetMyCompletedSurveyResponses(string name = "", int page = 0, int count = 20)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.SurveyResponses.Where(x => x.RespondentId == userId && x.Survey.Name.Contains(name ?? ""))
                .OrderByDescending(x => x.Survey.ModificationDate)
                .Skip(count * page).Take(count)
                .ProjectToType<SurveyResponseListItemDto>()
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

            if (!survey.Anonymous)
            {
                surveyResponseEntity.RespondentId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            }
            else
            {
                var surveyResponseStamp = surveyResponse.Adapt<SurveyResponse>();
                surveyResponseStamp.Answers = null;
                surveyResponseStamp.RespondentId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
                surveyResponseStamp.IsStamp = true;
                await _context.SurveyResponses.AddAsync(surveyResponseStamp);
            }

            surveyResponseEntity.Date = DateTime.Now;
            await _context.SurveyResponses.AddAsync(surveyResponseEntity);
            await _context.SaveChangesAsync();
            await _pushNotificationService.Send("New survey response" ,$"New response added to {survey.Name}", survey.Name.RemoveDiactrics().RemoveWhiteSpaces());
            return Ok();
        }

        [Authorize(Roles = "Admin,Lecturer")]
        [HttpGet("SurveyResults/{id}")]
        public async Task<ActionResult<SurveyResultsDto>> GetSurveyResults(int id)
        {
            var survey = await _context.Surveys.Include(x => x.Questions)
                .ThenInclude(x => x.Answers).ThenInclude(x => x.SurveyResponse).ThenInclude(x => x.Respondent)
                .FirstAsync(x => x.Id == id);

            var result = new SurveyResultsDto
            {
                SurveyName = survey.Name,
                Anonymous = survey.Anonymous,
                SurveyId = survey.Id.Value,
                QuestionResults = survey.Questions.Select(q => new QuestionResultsDto
                {
                    QuestionId = q.Id.Value,
                    QuestionIndex = q.Index, 
                    QuestionType = q.QuestionType,
                    QuestionText = q.QuestionText,
                    QuestionAnswers = q.Answers.Select(a => SelectQuestionAnswer(survey, q, a)).ToList(),
                    AnswerPercentages = CalculateAnswerPercentages(q).OrderByDescending(z => z.Value).ToList(),
                    Statistics = CalculateStatistics(q)
                }).ToList()
            };

            return result;
        }

        private Statistics CalculateStatistics(Question question)
        {
            if (question.QuestionType != QuestionType.ValuedSingleSelect &&
                question.QuestionType != QuestionType.Numeric)
                return null;

            List<double> values = new List<double>();

            switch (question.QuestionType)
            {
                case QuestionType.Numeric:
                    values = question.Answers.Select(x => x.Value.ToNullableDouble()).Where(x => x.HasValue).Select(x => x.Value).ToList();
                    break;
                case QuestionType.ValuedSingleSelect:
                    values = question.Answers.Select(x => JsonConvert.DeserializeObject<(string, double?)>(x.Value)).Select(x => x.Item2.Value).ToList();
                    break;
            }

            if (!values.Any())
                return null;

            return new Statistics()
            {
                Min = values.Min(),
                Max = values.Max(),
                Mean = values.Average(),
                Variance = MathNet.Numerics.Statistics.Statistics.Variance(values),
                Median = MathNet.Numerics.Statistics.Statistics.Median(values),
                StandardDeviation = MathNet.Numerics.Statistics.Statistics.StandardDeviation(values),
                Mode = values.GroupBy(x => x).OrderByDescending(g => g.Count()).First().Key
            };
        }

        private QuestionAnswerDto SelectQuestionAnswer(Survey survey, Question question, Answer answer)
        {
            var questionAnswer = new QuestionAnswerDto();
            if (survey.Anonymous)
            {
                questionAnswer.Respondent = answer.SurveyResponse.Id.Value.ToString();
                questionAnswer.RespondentId = answer.SurveyResponse.Id.Value;
            }
            else
            {
                questionAnswer.Respondent = answer.SurveyResponse.Respondent.FirstName + " " +
                                            answer.SurveyResponse.Respondent.LastName;
                questionAnswer.RespondentId = answer.SurveyResponse.RespondentId.Value;
            }
            if (question.QuestionType == QuestionType.MultipleSelect)
            {
                var list = JsonConvert.DeserializeObject<List<(string, double?)>>(answer.Value).Select(x => x.Item1).ToList();
                questionAnswer.Value = string.Join(',', list);
            }
            else if (question.QuestionType == QuestionType.ValuedSingleSelect)
            {
                var tuple = JsonConvert.DeserializeObject<(string, double?)>(answer.Value);
                questionAnswer.Value = tuple.Item1 + " : " + tuple.Item2;
            }
            else if (question.QuestionType == QuestionType.SingleSelect)
            {
                var tuple = JsonConvert.DeserializeObject<(string, double?)>(answer.Value);
                questionAnswer.Value = tuple.Item1;
            }
            else
            {
                questionAnswer.Value = answer.Value;
            }
            return questionAnswer;
        }

        private IEnumerable<AnswerPercentage> CalculateAnswerPercentages(Question question)
        {
            var percentages = new List<AnswerPercentage>();
            var answersCount = question.Answers.Count;

            switch (question.QuestionType)
            {
                case QuestionType.Date:
                    string timeFormat = "MM/dd/yyyy hh:mm:ss";
                    return question.Answers.GroupBy(x => x.Value).Select(g => new AnswerPercentage
                    {
                        Name = g.Key != null ? g.Key.Substring(0, 10): "-",
                        NumberOfAnswers = g.Count(),
                        Value = ((double)g.Count() / answersCount) * 100
                    });
                case QuestionType.Text:
                case QuestionType.Numeric:
                case QuestionType.Boolean:
                    return question.Answers.GroupBy(x => x.Value).Select(g => new AnswerPercentage
                    {
                        Name = g.Key ?? "-",
                        NumberOfAnswers = g.Count(),
                        Value = ((double)g.Count() / answersCount) * 100
                    });
                case QuestionType.SingleSelect:
                    return question.Answers.GroupBy(x => x.Value).Select(g => new AnswerPercentage
                    {
                        Name = g.Key != null ? JsonConvert.DeserializeObject<(string, double?)>(g.Key).Item1 : "-",
                        NumberOfAnswers = g.Count(),
                        Value = ((double)g.Count() / answersCount) * 100
                    });
                case QuestionType.ValuedSingleSelect:
                    return question.Answers.GroupBy(x => x.Value).Select(g => new AnswerPercentage
                    {
                        Name = g.Key != null ? JsonConvert.DeserializeObject<(string, double?)>(g.Key).Item1 + " : " + JsonConvert.DeserializeObject<(string, double?)>(g.Key).Item2 : "-",
                        NumberOfAnswers = g.Count(),
                        Value = ((double)g.Count() / answersCount)*100
                    });
                case QuestionType.MultipleSelect:
                    var answersLists = question.Answers.Select(x => JsonConvert.DeserializeObject<List<(string, double?)>>(x.Value)).ToList();
                    var answers = answersLists.SelectMany(x => x);
                    var distinctAnswers = answersLists.SelectMany(x => x).Distinct();
                    return answers.Select(x => new AnswerPercentage
                    {
                        Name = x.Item1 ?? "-",
                        NumberOfAnswers = answers.Count(a => a == x),
                        Value = ((double) answers.Count(a => a == x) / answersCount) * 100
                    });
                        default:
                    throw new ArgumentOutOfRangeException();
            }
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
                        string format = "MM/dd/yyyy HH:mm:ss";
                        if (answer.Value != null && !DateTime.TryParseExact(answer.Value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var n2))
                        {
                            errors[question.Index.ToString()] = new List<string> { $"{question.Index}. Value is not valid date" };
                            break;
                        }
                        if (!minDate.HasValue && !maxDate.HasValue)
                            break;
                        var date = DateTime.TryParseExact(answer.Value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out n2) ? n2 : (DateTime?) null;
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
