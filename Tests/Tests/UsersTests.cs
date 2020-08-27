using System;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Server.Controllers;
using Server.Entities;
using Server.Models.Auth;
using Server.Services;
using Xunit;

namespace Tests.Tests
{
    [Collection("MainFixtureCollection")]
    public class UsersTests
    {
        private readonly TestFixture _fixture;
        private UsersController Controller
        {
            get
            {
                var usersControllerContext = new SurveyContext(_fixture.Options);
                var usersServiceContext = new SurveyContext(_fixture.Options);
                var controller = new UsersController(usersControllerContext, _fixture.Configuration,
                    new UserService(usersServiceContext, _fixture.Configuration), new UsosApi(_fixture.Configuration));
                //setting user context
                controller.ControllerContext = _fixture.ControllerContext;
                return controller;
            }
        }

        public UsersTests(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task BasicAuthentication()
        {
            var result = await Controller.Authenticate(new AuthenticateDto() {Username = "student", Password = "password"});
            Assert.NotNull(result.Result);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task GettingUsosAuthenticationData(bool web)
        {
            var result = await Controller.GetUsosAuthData(web);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUser()
        {
            var result = await Controller.GetUser(1);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task GetSenestersAndMyCourses()
        {
            var result = await Controller.GetSemestersAndMyCourses();
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task GetSemsAndCoursesAsStudent()
        {
            var result = await Controller.GetSemsAndCoursesAsStudent();
            Assert.NotEmpty(result);
        }
    }

}