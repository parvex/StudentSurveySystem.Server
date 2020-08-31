using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Entities;
using Server.Models.Survey;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly SurveyContext _context;
        private readonly IPushNotificationService _pushNotificationService;

        public SurveysController(SurveyContext context, IPushNotificationService pushNotificationService)
        {
            _context = context;
            _pushNotificationService = pushNotificationService;
        }

        [HttpGet("MySurveys")]
        [Authorize(Roles = "Admin,Lecturer")]
        public async Task<ActionResult<List<SurveyListItemDto>>> GetMySurveys(string name = "", int page = 0, int count = 20)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.Surveys.Where(x => !x.IsTemplate && x.Name.Contains(name ?? "") && x.CreatorId == userId)
                .OrderByDescending(x => x.ModificationDate)
                .Skip(count * page).Take(count)
                .ProjectToType<SurveyListItemDto>()
                .ToListAsync();
        }

        [HttpGet("MyActiveSurveyNames")]
        [Authorize(Roles = "Admin,Lecturer")]
        public async Task<ActionResult<List<string>>> GetMyActtiveSurveyNames()
        {
            var now = DateTime.UtcNow;
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.Surveys.Where(x => !x.IsTemplate && x.CreatorId == userId && x.Active == true && x.EndDate > now)
                .Select(x => x.Name)
                .ToListAsync();
        }

        [HttpGet("MySurveyTemplates")]
        [Authorize(Roles = "Admin,Lecturer")]
        public async Task<ActionResult<List<SurveyListItemDto>>> GetMySurveyTemplates(string name = "", int page = 0, int count = 20)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.Surveys.Where(x => x.IsTemplate && x.Name.Contains(name ?? "") && x.CreatorId == userId)
                .OrderByDescending(x => x.ModificationDate)
                .Skip(count * page).Take(count)
                .ProjectToType<SurveyListItemDto>()
                .ToListAsync();
        }

        [HttpGet("MyNotFilledForms")]
        public async Task<ActionResult<List<SurveyListItemDto>>> GetMyNotFilledForms(string name = "", int page = 0, int count = 20)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));

            return await _context.Surveys.Where(x => !x.IsTemplate && x.Active && x.Course.CourseParticipants.Any(cp => cp.ParticipantId == userId)
                                                     && x.Name.Contains(name ?? "") && (x.EndDate == null || x.EndDate > DateTime.Now) 
                                                     && x.SurveyResponses.All(r => r.RespondentId != userId))
                .OrderByDescending(x => x.ModificationDate)
                .Skip(count * page).Take(count)
                .ProjectToType<SurveyListItemDto>()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyDto>> GetSurvey(int id)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));

            var surveyquery = _context.Surveys.Where(x => x.Id == id);

            if (role == "Lecturer")
                surveyquery = surveyquery.Where(x => x.CreatorId == userId);
            if (role == "Student")
                surveyquery = surveyquery.Where(x => x.Course.CourseParticipants.Any(x => x.ParticipantId == userId));

            var survey = await surveyquery.Include(x => x.Questions)
                .Include(x => x.Creator).Include(x => x.Course)
                .Include(x => x.Course).ThenInclude(x => x.Semester)
                .FirstOrDefaultAsync();

            if (survey == null)
                return NotFound();

            return survey.Adapt<SurveyDto>();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Lecturer")]
        public async Task<IActionResult> PutSurvey(int id, SurveyDto survey, bool activate = false)
        {
            var errorDictionary = ValidateSurveyForm(survey);
            if (errorDictionary.Count > 0)
                return BadRequest(new { errors = errorDictionary, code = 400 });

            var model = survey.Adapt<Survey>();
            model.Id = id;
            model.ModificationDate = DateTime.Now;
            _context.Entry(model).State = EntityState.Modified;
            model.CreatorId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            model.Active = activate;

            var oldQuestions = await _context.Questions.Where(x => x.SurveyId == id).ToListAsync();
            _context.Questions.RemoveRange(oldQuestions);
            await _context.SaveChangesAsync();

            foreach (var question in model.Questions)
            {
                question.Id = null;
            }

            await _context.Questions.AddRangeAsync(model.Questions);

            await _context.SaveChangesAsync();

            var addedSurvey = await GetSurvey(model.Id.Value);

            if (activate)
            {
                await _pushNotificationService.Send("New survey", $"New survey {addedSurvey.Value.Name} linked to your course {addedSurvey.Value.CourseName} has been added!", addedSurvey.Value.CourseSemesterName + addedSurvey.Value.CourseName);
            }
            return Ok();
        }

        [HttpPut]
        [HttpPost]
        [Authorize(Roles = "Admin,Lecturer")]
        public async Task<ActionResult<SurveyDto>> AddSurvey(SurveyDto survey, bool activate = false)
        {
            var errorDictionary = ValidateSurveyForm(survey);
            if (errorDictionary.Count > 0)
                return BadRequest(new { errors = errorDictionary, code = 400 });

            var dbModel = survey.Adapt<Survey>();
            dbModel.Active = activate;
            dbModel.CreatorId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            dbModel.ModificationDate = DateTime.Now;
            await _context.Surveys.AddAsync(dbModel);
            await _context.SaveChangesAsync();
            var addedSurvey = await GetSurvey(dbModel.Id.Value);
            if (activate)
            {
                await _pushNotificationService.Send("New survey" ,$"New survey {addedSurvey.Value.Name} linked to your course {addedSurvey.Value.CourseName} has been added!", addedSurvey.Value.CourseSemesterName + addedSurvey.Value.CourseName);
            }

            return addedSurvey;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Lecturer")]
        public async Task<ActionResult> DeleteSurvey(int id)
        {
            var survey = await _context.Surveys.Include(x => x.SurveyResponses).SingleOrDefaultAsync(x => x.Id == id);
            if (survey == null)
            {
                return NotFound();
            }

            if (survey.SurveyResponses.Any())
                return BadRequest("Can't delete survey with responses");

            _context.Surveys.Remove(survey);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("StartSurveyFromTemplate")]
        [Authorize(Roles = "Admin,Lecturer")]
        public async Task<ActionResult<SurveyDto>> StartSurveyFromTemplate(SurveyDto surveyDto)
        {
            if (surveyDto.Id == null)
            {
                var result = await AddSurvey(surveyDto);
                if (result.Result is BadRequestObjectResult)
                    return result;
            }
            surveyDto.Id = null;
            surveyDto.IsTemplate = false;
            surveyDto.Questions.ForEach(x => x.Id = null);

            var seurveyResult = await AddSurvey(surveyDto);
            return seurveyResult.Result is BadRequestObjectResult ? seurveyResult : Ok();
        }

        private Dictionary<string, List<string>> ValidateSurveyForm(SurveyDto survey)
        {
            var errors = new Dictionary<string, List<string>>();
            if (_context.Surveys.Any(x => x.Name == survey.Name && x.IsTemplate == survey.IsTemplate && survey.Id != x.Id))
                errors["Title"] = new List<string> {"Survey form with that title already exists" };

            return errors;
        }
    }
}
