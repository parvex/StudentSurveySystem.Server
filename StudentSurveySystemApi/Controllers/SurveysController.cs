using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Survey;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Server.Entities;

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
            return await _context.Surveys.Where(x => x.Name.Contains(name) && x.CreatorId == userId)
                .OrderByDescending(x => x.ModificationDate)
                .Skip(count * page).Take(count)
                .ProjectToType<SurveyDto>()
                .ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<SurveyDto>>> GetSurveys(string name = "", int page = 0, int count = 20)
        {
            return await _context.Surveys.Where(x => x.Name.Contains(name))
                .OrderByDescending(x => x.ModificationDate)
                .Skip(count * page).Take(count)
                .ProjectToType<SurveyDto>()
                .ToListAsync();
        }

        [HttpGet("MyNotFilledForm")]
        public async Task<ActionResult<List<SurveyDto>>> GetMyNotFilledForm(string name = "", int page = 0, int count = 20)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.Surveys.Where(x => x.SurveyResponses.All(r => r.Survey.Name.Contains(name) && r.RespondentId != userId))
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
            _context.Entry(model).State = EntityState.Modified;
            foreach (var question in model.Questions)
            {
                _context.Entry(question).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [HttpPost]
        public async Task<ActionResult<SurveyDto>> AddSurvey(SurveyDto survey)
        {
            var dbModel = survey.Adapt<Survey>();
            _context.Surveys.Add(dbModel);
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

        [HttpPost("/activate/{id}")]
        public async Task<ActionResult> ActivateSurvey(int id)
        {
            var survey = _context.Surveys.FirstOrDefault(x => x.Id == id);
            if (survey == null)
                return NotFound();

            survey.Active = true;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("/deactivate/{id}")]
        public async Task<ActionResult> DeactivateSurvey(int id)
        {
            var survey = _context.Surveys.FirstOrDefault(x => x.Id == id);
            if (survey == null)
                return NotFound();

            survey.Active = false;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
