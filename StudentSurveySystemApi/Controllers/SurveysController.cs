using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Models.Survey;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly SurveyContext _context;

        public SurveysController(SurveyContext context)
        {
            _context = context;
        }

        // GET: api/Surveys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Survey>>> GetSurveys()
        {
            return await _context.Surveys.ToListAsync();
        }


        [HttpGet("usertobefilled")]
        public async Task<ActionResult<IEnumerable<SurveyDetailsDto>>> GetUserToBeFilledSurveys()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.Surveys.Where(x => x.SurveyResponses.All(r => r.RespondentId != userId))
                .ProjectToType<SurveyDetailsDto>()
                .ToListAsync();
        }


        // GET: api/Surveys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyDetailsDto>> GetSurvey(int id)
        {
            var survey = await _context.Surveys.Where(x => x.Id == id)
                .ProjectToType<SurveyDetailsDto>().FirstOrDefaultAsync();

            if (survey == null)
            {
                return NotFound();
            }

            return survey;
        }

        // PUT: api/Surveys/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurvey(int id, Survey survey)
        {
            if (id != survey.Id)
            {
                return BadRequest();
            }

            _context.Entry(survey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<SurveyDetailsDto>> AddSurvey(SurveyFormDto survey)
        {
            var dbModel = survey.Adapt<Survey>();
            _context.Surveys.Add(dbModel);
            await _context.SaveChangesAsync();

            return await GetSurvey(dbModel.Id.Value);
        }

        // DELETE: api/Surveys/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Survey>> DeleteSurvey(int id)
        {
            var survey = await _context.Surveys.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }

            _context.Surveys.Remove(survey);
            await _context.SaveChangesAsync();

            return survey;
        }

        private bool SurveyExists(int id)
        {
            return _context.Surveys.Any(e => e.Id == id);
        }
    }
}
