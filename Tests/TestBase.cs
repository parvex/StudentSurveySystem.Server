using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using StudentSurveySystem.Core.Models;
using StudentSurveySystemApi.Entities;
using StudentSurveySystemApi.Helpers;
using StudentSurveySystemApi.Services;

namespace Tests
{
    public abstract class TestBase
    {
        protected readonly DbContextOptions<SurveyContext> options;
        protected readonly SurveyContext context;
        protected readonly ControllerContext controllerContext;

        protected TestBase()
        {
            //setting in memory db
            options = new DbContextOptionsBuilder<SurveyContext>()
                .UseInMemoryDatabase(databaseName: "In_memory_test_db")
                .Options;

            //mocking settings
            AppSettings app = new AppSettings() { };
            var mock = new Mock<IOptions<AppSettings>>();
            mock.Setup(ap => ap.Value).Returns(app);

            context = new SurveyContext(options);

            //db seeding
            using (var seedContext = new SurveyContext(options))
            {
                 DbInitializer.Seed(context, new UserService(context, mock.Object));
            }

            //creating controller context - logged as seeded admin to add when required in controller
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "1"),
                new Claim(ClaimTypes.Role, UserRole.Admin.ToString()),
            }, "mock"));
            controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }
    }
}