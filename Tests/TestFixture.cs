using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Server.Entities;
using Server.Helpers;
using Server.Models.Auth;
using Server.Services;

namespace Tests
{
    public class TestFixture
    {
        public readonly DbContextOptions<SurveyContext> Options;
        public readonly ControllerContext ControllerContext;

        public TestFixture()
        {
            //setting in memory db
            Options = new DbContextOptionsBuilder<SurveyContext>()
                .UseInMemoryDatabase(databaseName: "In_memory_test_db")
                .Options;

            //mocking settings
            AppSettings app = new AppSettings() { };
            var mock = new Mock<IOptions<AppSettings>>();
            mock.Setup(ap => ap.Value).Returns(app);


            //db seeding
            using (var seedContext = new SurveyContext(Options))
            {
                DbInitializer.Seed(seedContext, new UserService(seedContext, mock.Object), false);
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

            //use custom mappings - this is used in startup
            MapsterHelper.SetCustomMappings();
        }
    }
}