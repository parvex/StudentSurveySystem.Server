using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Entities;
using Server.Helpers;
using Server.Models.Auth;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
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
        [HttpPost("Authenticate")]
        public async Task<ActionResult<CurrentUserDto>> Authenticate([FromBody]AuthenticateDto userDto)
        {
            var user = await _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpGet("UsosAuthData")]
        public async Task<UsosAuthDto> GetUsosAuthData()
        {
            return await _usosApi.GetUsosAuthData();
        }


        [AllowAnonymous]
        [HttpPost("UsosPinAuth")]
        public async Task<ActionResult<CurrentUserDto>> UsosPinAuth(UsosAuthDto usosAuth)
        {
            if (usosAuth.RequestToken == null || usosAuth.TokenSecret == null || usosAuth.OAuthVerifier == null)
                return BadRequest("Missing parameters");
            var accessToken = _usosApi.GetAccessTokenData(usosAuth);
            var usosUser = _usosApi.GetUsosUserData(accessToken.Item1, accessToken.Item2);

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

        //[Authorize(Roles = "Admin")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<User>>> GetUsers(string userName = "", int page = 0, int count = 20)
        //{
        //    return await _context.Users.Where(x => x.Username.Contains(userName))
        //        .OrderBy(x => x.Username)
        //        .Skip(count * page).Take(count)
        //        .ToListAsync();
        //}

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
    }
}
