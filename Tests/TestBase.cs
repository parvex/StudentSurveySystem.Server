using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Core.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Server.Entities;
using Server.Helpers;
using Server.Services;

namespace Tests
{
    public abstract class TestBase
    {
        protected readonly DbContextOptions<SurveyContext> Options;
        protected readonly SurveyContext Context;
        protected readonly ControllerContext ControllerContext;

        protected TestBase()
        {
            //setting in memory db
            Options = new DbContextOptionsBuilder<SurveyContext>()
                .UseInMemoryDatabase(databaseName: "In_memory_test_db")
                .Options;

            //mocking settings
            AppSettings app = new AppSettings() { };
            var mock = new Mock<IOptions<AppSettings>>();
            mock.Setup(ap => ap.Value).Returns(app);

            Context = new SurveyContext(Options);

            //db seeding
            using (var seedContext = new SurveyContext(Options))
            {
                 DbInitializer.Seed(Context, new UserService(Context, mock.Object));
            }

            //creating controller context - logged as seeded admin to add when required in controller
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "1"),
                new Claim(ClaimTypes.Role, UserRole.Admin.ToString()),
            }, "mock"));
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }
    }
}