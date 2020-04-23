using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Models.SurveyResponse;
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
            var query = _context.SurveyResponses.Where(x => x.Survey.Name.Contains(name));
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
            return await _context.SurveyResponses.Where(x => x.RespondentId == userId && x.Survey.Name.Contains(name))
                .OrderByDescending(x => x.Survey.ModificationDate)
                .Skip(count * page).Take(count)
                .ProjectToType<SurveyResponseDetailsDto>()
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<SurveyResponseDto>> AddSurveyResponse(SurveyResponseDto surveyResponse)
        {
            var surveyResponseEntity = surveyResponse.Adapt<SurveyResponse>();
            surveyResponseEntity.Date = DateTime.Now;
            _context.SurveyResponses.Add(surveyResponseEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
