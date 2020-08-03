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

        [HttpGet("MyNotFilledForm")]
        public async Task<ActionResult<List<SurveyListItemDto>>> GetMyNotFilledForm(string name = "", int page = 0, int count = 20)
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

            var survey = await _context.Surveys.Where(x => x.Id == id).Include(x => x.Questions)
                .Include(x => x.Creator).Include(x => x.Course).ThenInclude(x => x.CourseParticipants)
                .Include(x => x.Course).ThenInclude(x => x.Semester)
                .FirstOrDefaultAsync();

            if (survey == null)
                return NotFound();

            if(role == "Student" && survey.Course.CourseParticipants.All(x => x.ParticipantId != userId))
                return Unauthorized("You can get only surveys in which you participate");
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
            foreach (var question in model.Questions)
            {
                if (question.Id != null)
                    _context.Entry(question).State = EntityState.Modified;
                else
                    _context.Entry(question).State = EntityState.Added;
            }
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
            else
            {
                var result = await PutSurvey(surveyDto.Id.Value, surveyDto);
                if (result is BadRequestObjectResult)
                    return (BadRequestObjectResult) result;
            }
            surveyDto.Id = null;
            surveyDto.Active = true;
            surveyDto.IsTemplate = false;
            surveyDto.Questions.ForEach(x => x.Id = null);

            var seurveyResult = await AddSurvey(surveyDto);
            return seurveyResult.Result is BadRequestObjectResult ? seurveyResult : Ok();
        }

        [HttpGet("GetSemestersAndMyCourses")]
        public async Task<List<SemesterDto>> GetSemestersAndMyCourses()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.Semesters.OrderBy(x => x.Name).Select(x => new SemesterDto()
            {
                Id = x.Id.Value, Name = x.Name,
                Courses = x.Courses.Where(c => c.CourseLecturers.Any(cl => cl.LecturerId == userId)).Select(c => c.Adapt<CourseDto>()).ToList()
            }).ToListAsync();
        }

        [HttpGet("GetSemsAndCoursesAsStudent")]
        public async Task<List<SemesterDto>> GetSemsAndCoursesAsStudent()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.Semesters.OrderBy(x => x.Name).Select(x => new SemesterDto()
            {
                Id = x.Id.Value, Name = x.Name,
                Courses = x.Courses.Where(c => c.CourseParticipants.Any(cl => cl.ParticipantId == userId)).Select(c => c.Adapt<CourseDto>()).ToList()
            }).ToListAsync();
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
