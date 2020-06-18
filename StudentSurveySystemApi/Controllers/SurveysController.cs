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
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Server.Entities;
using Server.Helpers;
using Server.Models.Survey;

namespace Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly SurveyContext _context;

        public SurveysController(SurveyContext context)
        {
            _context = context;
        }

        [HttpGet("MySurveys")]
        public async Task<ActionResult<List<SurveyDto>>> GetMySurveys(string name = "", int page = 0, int count = 20)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.Surveys.Where(x => !x.IsTemplate && x.Name.Contains(name ?? "") && x.CreatorId == userId)
                .OrderByDescending(x => x.ModificationDate)
                .Skip(count * page).Take(count)
                .ProjectToType<SurveyDto>()
                .ToListAsync();
        }

        [HttpGet("MySurveyTemplates")]
        public async Task<ActionResult<List<SurveyDto>>> GetMySurveyTemplates(string name = "", int page = 0, int count = 20)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.Surveys.Where(x => x.IsTemplate && x.Name.Contains(name ?? "") && x.CreatorId == userId)
                .OrderByDescending(x => x.ModificationDate)
                .Skip(count * page).Take(count)
                .ProjectToType<SurveyDto>()
                .ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<SurveyDto>>> GetSurveys(string name = "", int page = 0, int count = 20)
        {
            return await _context.Surveys.Where(x => !x.IsTemplate && x.Name.Contains(name ?? ""))
                .OrderByDescending(x => x.ModificationDate)
                .Skip(count * page).Take(count)
                .ProjectToType<SurveyDto>()
                .ToListAsync();
        }

        [HttpGet("MyNotFilledForm")]
        public async Task<ActionResult<List<SurveyDto>>> GetMyNotFilledForm(string name = "", int page = 0, int count = 20)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.Surveys.Where(x => !x.IsTemplate && x.Active && x.Course.CourseParticipants.Any(cp => cp.ParticipantId == userId)
                                                     && x.Name.Contains(name ?? "") && (x.EndDate == null || x.EndDate > DateTime.Now) 
                                                     && x.SurveyResponses.All(r => r.RespondentId != userId))
                .OrderByDescending(x => x.ModificationDate)
                .Skip(count * page).Take(count)
                .ProjectToType<SurveyDto>()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyDto>> GetSurvey(int id)
        {
            var survey = await _context.Surveys.Where(x => x.Id == id)
                .ProjectToType<SurveyDto>().FirstOrDefaultAsync();

            if (survey == null)
            {
                return NotFound();
            }

            return survey;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurvey(int id, SurveyDto survey)
        {
            var model = survey.Adapt<Survey>();
            model.Id = id;
            model.ModificationDate = DateTime.Now;
            _context.Entry(model).State = EntityState.Modified;
            foreach (var question in model.Questions)
            {
                if (question.Id != null)
                    _context.Entry(question).State = EntityState.Modified;
                else
                    _context.Entry(question).State = EntityState.Added;
            }
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [HttpPost]
        public async Task<ActionResult<SurveyDto>> AddSurvey(SurveyDto survey)
        {
            var dbModel = survey.Adapt<Survey>();
            dbModel.CreatorId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            dbModel.ModificationDate = DateTime.Now;
            await _context.Surveys.AddAsync(dbModel);
            await _context.SaveChangesAsync();

            return await GetSurvey(dbModel.Id.Value);
        }

        [HttpDelete("{id}")]
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
        public async Task<ActionResult> StartSurveyFromTemplate(SurveyDto surveyDto)
        {
            surveyDto.Id = null;
            surveyDto.Active = true;
            surveyDto.IsTemplate = false;
            surveyDto.Questions.ForEach(x => x.Id = null);

            await AddSurvey(surveyDto);

            return Ok();
        }

        [HttpGet("GetSemestersAndMyCourses")]
        public async Task<List<SemesterDto>> GetSemestersAndMyCourses()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.Semesters.Select(x => new SemesterDto()
            {
                Id = x.Id.Value, Name = x.Name,
                Courses = x.Courses.Where(c => c.CourseLecturers.Any(cl => cl.LecturerId == userId)).Select(c => c.Adapt<CourseDto>()).ToList()
            }).ToListAsync();
        }
    }
}
