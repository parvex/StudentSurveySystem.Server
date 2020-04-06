using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSurveySystem.Core.Models;
using StudentSurveySystemApi.Entities;

namespace StudentSurveySystemApi.Controllers
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

        // GET: api/SurveyResponses
        [Authorize(Roles = "Admin,Lecturer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyResponseDetailsDto>>> GetSurveyResponses()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            switch (role)
            {
                case "Lecturer":
                    var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
                    return await _context.SurveyResponses.Where(x => x.Survey.CreatorId == userId).ProjectToType<SurveyResponseDetailsDto>().ToListAsync();
                case "Admin":
                    return await _context.SurveyResponses.ProjectToType<SurveyResponseDetailsDto>().ToListAsync();
                default:
                    return BadRequest("Wrong role");
            }
        }

        // GET: api/SurveyResponses/5
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

        [HttpGet("userfilled")]
        public async Task<ActionResult<IEnumerable<SurveyResponseDetailsDto>>> GetUserSurveyResponses()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.SurveyResponses.Where(x => x.RespondentId == userId)
                .ProjectToType<SurveyResponseDetailsDto>()
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<SurveyResponseDto>> PostSurveyResponse(SurveyResponseDto surveyResponse)
        {
            var surveyResponseEntity = surveyResponse.Adapt<SurveyResponse>();
            surveyResponseEntity.Date = DateTime.Now;
            _context.SurveyResponses.Add(surveyResponseEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
