using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
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
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == currentUser.Id.Value);
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
                 await using (var transaction = await _context.Database.BeginTransactionAsync())
                 {
                     await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Users] ON");
                     await _context.Users.AddAsync(newUser);
                     await _context.SaveChangesAsync();
                     await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Users] OFF");
                     await transaction.CommitAsync();
                 }
                 currentUser.Id = newUser.Id;
                 var usosSemesters = _usosApi.GetUserCourses(accessToken.Item1, accessToken.Item2, newUser);
                 await UpdateSemAndCourseData(usosSemesters, newUser);
            }
            SetupToken(currentUser, accessToken.Item1, accessToken.Item2);
            return Ok(currentUser);
        }

        [HttpPut("UpdateUserUsosData")]
        public async Task<ActionResult> UpdateUserUsosData()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            var accessToken = User.FindFirstValue("AccessToken");
            var accessTokenSecret = User.FindFirstValue("AccessTokenSecret");
            var user = (await GetUser(userId)).Value;
            var semestersData = _usosApi.GetUserCourses(accessToken, accessTokenSecret, user);
            await UpdateSemAndCourseData(semestersData, user);
            return Ok();
        }

        private void SetupToken(CurrentUserDto currentUser, string accessToken, string accessTokenSecret)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, currentUser.Id.ToString()),
                    new Claim(ClaimTypes.Role, currentUser.UserRole.ToString()),
                    new Claim("AccessToken", accessToken),
                    new Claim("AccessTokenSecret", accessTokenSecret)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            currentUser.Token = tokenHandler.WriteToken(token);
        }

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

        private async Task UpdateSemAndCourseData(List<Semester> usosSemesters, User user)
        {
            var existingSemesters = _context.Semesters.ToList();
            var updatedSemesters = usosSemesters.Where(x => existingSemesters.Any(y => y.Name.Equals(x.Name))).ToList();
            updatedSemesters.ForEach(x => x.Id = existingSemesters.First(y => y.Name == x.Name).Id);
            var newSems = usosSemesters.Where(x => existingSemesters.All(y => y.Name != x.Name)).ToList();
            var userCoursesAsLecturer = usosSemesters.SelectMany(x => x.Courses).Where(x => x.CourseLecturers != null && x.CourseLecturers.Any()).ToList();
            //adding new smesters
            await _context.Semesters.AddRangeAsync(newSems);
            foreach (var semester in updatedSemesters)
            {
                var semesterEntity = await _context.Semesters.Include(x => x.Courses).FirstAsync(x => x.Name == semester.Name);
                semesterEntity.Courses ??= new List<Course>();
                var newCourses = semester.Courses.Where(x => semesterEntity.Courses.All(y => y.Name != x.Name)).ToList();
                foreach (var newCourse in newCourses)
                {
                    newCourse.SemesterId = semester.Id.Value;
                    //removing indication that user is a lecturer of this course to not break anything
                    newCourse.CourseLecturers = null;
                    //adding new courses
                    _context.Entry(newCourse).State = EntityState.Added;
                }
                
            }
            await _context.SaveChangesAsync();
            var userEntity = await _context.Users.Include(x => x.CourseParticipants)
                .Include(x => x.CourseLecturers).FirstAsync(x => x.Id == user.Id);
            var userCourses = usosSemesters.SelectMany(x => x.Courses).ToList();
            var userCourseParticipation = userCourses.Select(x => new CourseParticipant() {Course = x, Participant = userEntity}).ToList();
            var userCourseParticipationAsLecturer = userCoursesAsLecturer.Select(x => new CourseLecturer() {Course = x, Lecturer = userEntity}).ToList();
            userEntity.CourseParticipants = userCourseParticipation;
            userEntity.CourseLecturers = userCourseParticipationAsLecturer;

            await _context.SaveChangesAsync();
        }
    }
}
