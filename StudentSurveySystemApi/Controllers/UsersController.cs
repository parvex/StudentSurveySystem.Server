﻿using System;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Entities;
using Server.Helpers;
using Server.Models.Auth;
using Server.Models.Survey;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly SurveyContext _context;
        private readonly IConfiguration _appSettings;
        private readonly IUserService _userService;
        private readonly IUsosApi _usosApi;
        private readonly bool _relational;

        public UsersController(
            SurveyContext context,
            IConfiguration appSettings,
            IUserService userService,
            IUsosApi usosApi,
            bool relational = true)
        {
            _context = context;
            _appSettings = appSettings;
            _userService = userService;
            _usosApi = usosApi;
            _relational = relational;
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
        public async Task<UsosAuthDto> GetUsosAuthData(bool web = false)
        {
            return await _usosApi.GetUsosAuthData(web);
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

                if (_relational)
                {
                    await using (var transaction = await _context.Database.BeginTransactionAsync())
                    {
                        await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Users] ON");
                        await _context.Users.AddAsync(newUser);
                        await _context.SaveChangesAsync();
                        await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Users] OFF");
                        await transaction.CommitAsync();
                    }
                }
                else
                {
                    await _context.Users.AddAsync(newUser);
                    await _context.SaveChangesAsync();
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
            var key = Encoding.ASCII.GetBytes(_appSettings["Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
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
            currentUser.TokenExpirationDate = token.ValidTo;
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


        [HttpGet("GetSemestersAndMyCourses")]
        public async Task<List<SemesterDto>> GetSemestersAndMyCourses()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.Semesters.OrderBy(x => x.Name).Select(x => new SemesterDto()
            {
                Id = x.Id.Value,
                Name = x.Name,
                Courses = x.Courses.Where(c => c.CourseLecturers.Any(cl => cl.LecturerId == userId)).Select(c => c.Adapt<CourseDto>()).ToList()
            }).ToListAsync();
        }

        [HttpGet("GetSemsAndCoursesAsStudent")]
        public async Task<List<SemesterDto>> GetSemsAndCoursesAsStudent()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            return await _context.Semesters.OrderBy(x => x.Name).Select(x => new SemesterDto()
            {
                Id = x.Id.Value,
                Name = x.Name,
                Courses = x.Courses.Where(c => c.SemesterId == x.Id && c.CourseParticipants.Any(cl => cl.ParticipantId == userId)).Select(c => c.Adapt<CourseDto>()).ToList()
            }).ToListAsync();
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
            await AddNewCourses(updatedSemesters);
            await _context.SaveChangesAsync();
            foreach (var usosSemester in usosSemesters)
            {
                usosSemester.Id = _context.Semesters.First(x => x.Name == usosSemester.Name).Id;
                usosSemester.Courses.ForEach(x => x.Id = _context.Courses.First(y => y.Name == x.Name && y.SemesterId == usosSemester.Id).Id);
            }
            var userEntity = await _context.Users.Include(x => x.CourseParticipants)
                .Include(x => x.CourseLecturers).FirstAsync(x => x.Id == user.Id);
            var userCourses = usosSemesters.SelectMany(x => x.Courses).ToList();
            var userCourseParticipation = userCourses.Select(x => new CourseParticipant() {CourseId = x.Id.Value, Participant = userEntity}).ToList();
            var userCourseParticipationAsLecturer = userCoursesAsLecturer.Select(x => new CourseLecturer() {CourseId = x.Id.Value, Lecturer = userEntity}).ToList();
            userEntity.CourseParticipants = userCourseParticipation;
            userEntity.CourseLecturers = userCourseParticipationAsLecturer;
            await _context.SaveChangesAsync();
        }

        private async Task AddNewCourses(List<Semester> updatedSemesters)
        {
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
                    await _context.Courses.AddAsync(newCourse);
                }
            }
        }
    }
}
