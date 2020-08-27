using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestSharp;
using Server.Controllers;
using Server.Entities;
using Server.Models.Auth;
using Server.Services;
using Xunit;

namespace Tests.Tests
{
    [Collection("MainFixtureCollection")]
    public class UsosLogicTests
    {
        public string coursesJson =
            "{\"course_editions\":{\"2019L\":[{\"course_id\":\"103A-INxxx-ISP-LTM\",\"user_groups\":[{\"lecturers\":[{\"id\":\"801942\",\"first_name\":\"Prowadzacy\",\"last_name\":\"Prowadzi\"}]}]}],\"2020Z\":[{\"course_id\":\"103A-INxxx-ISP-SOI\",\"user_groups\":[{\"lecturers\":[{\"id\":\"1121840\",\"first_name\":\"Gabriel\",\"last_name\":\"Rębacz\"}]}]},{\"course_id\":\"103A-INxxx-ISP-SDI\",\"user_groups\":[{\"lecturers\":[{\"id\":\"1121840\",\"first_name\":\"Prowadzacy\",\"last_name\":\"Prowadzi\"}]}]}]}}";

        private readonly TestFixture _fixture;
        private UsersController Controller
        {
            get
            {
                var usersControllerContext = new SurveyContext(_fixture.Options);
                var usersServiceContext = new SurveyContext(_fixture.Options);
                var usosApi = new UsosApi(_fixture.Configuration);
                var restClientMock = new Mock<IRestClient>();

                var result1 = new Mock<IRestResponse<UsosUser>>();
                result1.Setup(x => x.Data).Returns(new UsosUser()
                {
                    Id = 348782,
                    FirstName = "Test134",
                    LastName = "Test1323",
                    StaffStatus = StaffStatus.Lecturer,
                    StudentStatus = StudentStatus.ActiveStudent
                });
                result1.Setup(x => x.IsSuccessful).Returns(true);

                restClientMock.Setup(x => x.Execute<UsosUser>(It.IsAny<RestRequest>())).Returns(result1.Object);


                var result = new Mock<IRestResponse>();
                result.Setup(x => x.Content).Returns(coursesJson);
                result.Setup(x => x.IsSuccessful).Returns(true);
                restClientMock.Setup(x => x.Execute(It.Is<RestRequest>(x => x.Resource=="courses/user")))
                    .Returns(result.Object);


                result = new Mock<IRestResponse>();
                result.Setup(x => x.Content).Returns("oauth_token=CZY4hw2aqKVcw7uWMN3G&oauth_token_secret=5SU2wm6XX8XuQxeGdNQTmTHtwB9tPCHgb6eemkmQ");
                result.Setup(x => x.IsSuccessful).Returns(true);
                restClientMock.Setup(x => x.Execute(It.Is<RestRequest>(x => x.Resource== "oauth/access_token")))
                    .Returns(result.Object);

                usosApi.Client = restClientMock.Object;
                var controller = new UsersController(usersControllerContext, _fixture.Configuration,
                    new UserService(usersServiceContext, _fixture.Configuration), usosApi, false);
                //setting user context
                controller.ControllerContext = _fixture.ControllerContext;
                return controller;
            }
        }

        public UsosLogicTests(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task UsosPinAuth()
        {
            var result = await Controller.UsosPinAuth(new UsosAuthDto(){OAuthVerifier = "ver", RequestToken = "aaa", TokenSecret = "tasdasd", UsosAuthUrl = "asdas"});
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task UpdateUserUsosData()
        {
            var result = await Controller.UpdateUserUsosData();
            Assert.IsType<OkResult>(result);
        }
    }
}