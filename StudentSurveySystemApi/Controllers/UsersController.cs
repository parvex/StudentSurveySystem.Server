using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentSurveySystem.Core.Models;
using StudentSurveySystem.Core.Models.Auth;
using StudentSurveySystemApi.Entities;
using StudentSurveySystemApi.Helpers;
using StudentSurveySystemApi.Services;

namespace StudentSurveySystemApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly SurveyContext _context;
        private readonly AppSettings _appSettings;
        private readonly IUserService _userService;
        private readonly IUsosApi _usosApi;

        public UsersController(
            SurveyContext context,
           IOptions<AppSettings> appSettings,
            IUserService userService,
            IUsosApi usosApi)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _userService = userService;
            _usosApi = usosApi;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateDto userDto)
        {
            var user = await _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpGet("usosauthdata")]
        public async Task<UsosAuthDto> GetUsosAuthData()
        {

            return await _usosApi.GetUsosAuthData();
        }


        [AllowAnonymous]
        [HttpPost("usospinauth")]
        public async Task<IActionResult> UsosPinAuth(UsosAuthDto usosAuth)
        {
            if (usosAuth.RequestToken == null || usosAuth.TokenSecret == null || usosAuth.OAuthVerifier == null)
                return BadRequest("Missing parameters");
            var usosUser = _usosApi.GetUsosUserData(usosAuth);

            if (usosUser == null)
                return Unauthorized("Wrong PIN");

            var currentUser = usosUser.Adapt<CurrentUserDto>();
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.UsosId == currentUser.UsosId.Value);
            if (dbUser != null)
            {
                currentUser.Id = dbUser.Id;
                currentUser.UserRole = dbUser.UserRole;
            }
            else
            {
                 var newUser = currentUser.Adapt<User>();
                 newUser.UserRole = usosUser.StaffStatus == StaffStatus.Lecturer ? UserRole.Lecturer :
                     usosUser.StudentStatus == StudentStatus.ActiveStudent ? UserRole.Student : throw new ArgumentOutOfRangeException("Incorrent user status");
                 await _context.Users.AddAsync(newUser);
                 await _context.SaveChangesAsync();
                 currentUser.Id = newUser.Id;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, currentUser.Id.ToString()),
                    new Claim(ClaimTypes.Role, currentUser.UserRole.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            currentUser.Token = tokenHandler.WriteToken(token);

            return Ok(currentUser);
        }

        // GET: api/Users
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserDto userDto)
        {
            var user = userDto.Adapt<User>();
            user.Id = id;

            try 
            {
                await _userService.Update(user, userDto.Password);
                return Ok();
            } 
            catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }
    }
}
