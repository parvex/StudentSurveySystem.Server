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
        public string coursesJson = "{\"course_editions\":{\"2019L\":[{\"course_id\":\"103A-INxxx-ISP-LTM\",\"user_groups\":[{\"lecturers\":[{\"id\":\"801942\",\"first_name\":\"Prowadzacy\",\"last_name\":\"Prowadzi\"}]}]}],\"2020Z\":[{\"course_id\":\"103A-INxxx-ISP-SOI\",\"user_groups\":[{\"lecturers\":[{\"id\":\"1121840\",\"first_name\":\"Gabriel\",\"last_name\":\"Rębacz\"}]}]},{\"course_id\":\"103A-INxxx-ISP-SDI\",\"user_groups\":[{\"lecturers\":[{\"id\":\"1121840\",\"first_name\":\"Prowadzacy\",\"last_name\":\"Prowadzi\"}]}]}]}}";


        public string lecturerGroupsJson =
            "{\"groups\":{\"2018L\":[{\"term_id\":\"2018L\",\"course_id\":\"103C-INxxx-ISP-PROZ\",\"course_name\":{\"pl\":\"Programowanie zdarzeniowe\",\"en\":\"Event-Driven Programming\"}},{\"term_id\":\"2018L\",\"course_id\":\"103A-INIIT-ISP-TIN\",\"course_name\":{\"pl\":\"Techniki internetowe\",\"en\":\"Internet Techniques\"}},{\"term_id\":\"2018L\",\"course_id\":\"103A-INIIT-ISP-TIN\",\"course_name\":{\"pl\":\"Techniki internetowe\",\"en\":\"Internet Techniques\"}},{\"term_id\":\"2018L\",\"course_id\":\"103C-INIIT-ISP-TKOM\",\"course_name\":{\"pl\":\"Techniki kompilacji\",\"en\":\"Compilation Techniques\"}},{\"term_id\":\"2018L\",\"course_id\":\"103A-INxxx-ISP-ZPR\",\"course_name\":{\"pl\":\"Zaawansowane programowanie w C++\",\"en\":\"Advanced C++ Programming\"}}],\"2018Z\":[{\"term_id\":\"2018Z\",\"course_id\":\"103C-INxxx-ISP-AISDI\",\"course_name\":{\"pl\":\"Algorytmy i struktury danych\",\"en\":\"Algorithms and Data Structures\"}},{\"term_id\":\"2018Z\",\"course_id\":\"103C-INxxx-ISP-AISDI\",\"course_name\":{\"pl\":\"Algorytmy i struktury danych\",\"en\":\"Algorithms and Data Structures\"}},{\"term_id\":\"2018Z\",\"course_id\":\"103C-INxxx-ISP-AISDI\",\"course_name\":{\"pl\":\"Algorytmy i struktury danych\",\"en\":\"Algorithms and Data Structures\"}},{\"term_id\":\"2018Z\",\"course_id\":\"103C-INxxx-ISP-AISDI\",\"course_name\":{\"pl\":\"Algorytmy i struktury danych\",\"en\":\"Algorithms and Data Structures\"}},{\"term_id\":\"2018Z\",\"course_id\":\"103C-INxxx-ISP-AISDI\",\"course_name\":{\"pl\":\"Algorytmy i struktury danych\",\"en\":\"Algorithms and Data Structures\"}},{\"term_id\":\"2018Z\",\"course_id\":\"103A-INxxx-ISP-ZPR\",\"course_name\":{\"pl\":\"Zaawansowane programowanie w C++\",\"en\":\"Advanced C++ Programming\"}},{\"term_id\":\"2018Z\",\"course_id\":\"103A-INxxx-ISP-ZPR\",\"course_name\":{\"pl\":\"Zaawansowane programowanie w C++\",\"en\":\"Advanced C++ Programming\"}},{\"term_id\":\"2018Z\",\"course_id\":\"103A-INxxx-ISP-ZPR\",\"course_name\":{\"pl\":\"Zaawansowane programowanie w C++\",\"en\":\"Advanced C++ Programming\"}},{\"term_id\":\"2018Z\",\"course_id\":\"103A-INxxx-ISP-ZPR\",\"course_name\":{\"pl\":\"Zaawansowane programowanie w C++\",\"en\":\"Advanced C++ Programming\"}}],\"2019L\":[{\"term_id\":\"2019L\",\"course_id\":\"103C-INxxx-ISP-PROZ\",\"course_name\":{\"pl\":\"Programowanie zdarzeniowe\",\"en\":\"Event-Driven Programming\"}},{\"term_id\":\"2019L\",\"course_id\":\"103A-INIIT-ISP-TIN\",\"course_name\":{\"pl\":\"Techniki internetowe\",\"en\":\"Internet Techniques\"}},{\"term_id\":\"2019L\",\"course_id\":\"103A-INIIT-ISP-TIN\",\"course_name\":{\"pl\":\"Techniki internetowe\",\"en\":\"Internet Techniques\"}},{\"term_id\":\"2019L\",\"course_id\":\"103C-INIIT-ISP-TKOM\",\"course_name\":{\"pl\":\"Techniki kompilacji\",\"en\":\"Compilation Techniques\"}},{\"term_id\":\"2019L\",\"course_id\":\"103A-INxxx-ISP-ZPR\",\"course_name\":{\"pl\":\"Zaawansowane programowanie w C++\",\"en\":\"Advanced C++ Programming\"}}],\"2019Z\":[{\"term_id\":\"2019Z\",\"course_id\":\"103A-INxxx-ISP-PIPR\",\"course_name\":{\"pl\":\"Podstawy informatyki i programowania\",\"en\":\"Essentials of Informatics and Programming\"}},{\"term_id\":\"2019Z\",\"course_id\":\"103A-INxxx-ISP-PIPR\",\"course_name\":{\"pl\":\"Podstawy informatyki i programowania\",\"en\":\"Essentials of Informatics and Programming\"}},{\"term_id\":\"2019Z\",\"course_id\":\"103A-INxxx-ISP-PIPR\",\"course_name\":{\"pl\":\"Podstawy informatyki i programowania\",\"en\":\"Essentials of Informatics and Programming\"}},{\"term_id\":\"2019Z\",\"course_id\":\"103A-INxxx-ISP-PIPR\",\"course_name\":{\"pl\":\"Podstawy informatyki i programowania\",\"en\":\"Essentials of Informatics and Programming\"}},{\"term_id\":\"2019Z\",\"course_id\":\"103C-INIIT-ISP-TKOM\",\"course_name\":{\"pl\":\"Techniki kompilacji\",\"en\":\"Compilation Techniques\"}},{\"term_id\":\"2019Z\",\"course_id\":\"103C-INIIT-ISP-TKOM\",\"course_name\":{\"pl\":\"Techniki kompilacji\",\"en\":\"Compilation Techniques\"}},{\"term_id\":\"2019Z\",\"course_id\":\"103A-INxxx-ISP-ZPR\",\"course_name\":{\"pl\":\"Zaawansowane programowanie w C++\",\"en\":\"Advanced C++ Programming\"}},{\"term_id\":\"2019Z\",\"course_id\":\"103A-INxxx-ISP-ZPR\",\"course_name\":{\"pl\":\"Zaawansowane programowanie w C++\",\"en\":\"Advanced C++ Programming\"}},{\"term_id\":\"2019Z\",\"course_id\":\"103A-INxxx-ISP-ZPR\",\"course_name\":{\"pl\":\"Zaawansowane programowanie w C++\",\"en\":\"Advanced C++ Programming\"}},{\"term_id\":\"2019Z\",\"course_id\":\"103A-INxxx-ISP-ZPR\",\"course_name\":{\"pl\":\"Zaawansowane programowanie w C++\",\"en\":\"Advanced C++ Programming\"}}],\"2020Z\":[{\"term_id\":\"2020Z\",\"course_id\":\"103D-INxxx-ISP-AISDI\",\"course_name\":{\"pl\":\"Algorytmy i struktury danych\",\"en\":\"Algorithms and Data Structures\"}},{\"term_id\":\"2020Z\",\"course_id\":\"103A-INxxx-ISP-PAP\",\"course_name\":{\"pl\":\"Programowanie aplikacyjne\",\"en\":\"Application Programming\"}},{\"term_id\":\"2020Z\",\"course_id\":\"103A-INxxx-ISP-PAP\",\"course_name\":{\"pl\":\"Programowanie aplikacyjne\",\"en\":\"Application Programming\"}},{\"term_id\":\"2020Z\",\"course_id\":\"103A-INIIT-ISP-TIN\",\"course_name\":{\"pl\":\"Techniki internetowe\",\"en\":\"Internet Techniques\"}},{\"term_id\":\"2020Z\",\"course_id\":\"103A-INIIT-ISP-TIN\",\"course_name\":{\"pl\":\"Techniki internetowe\",\"en\":\"Internet Techniques\"}},{\"term_id\":\"2020Z\",\"course_id\":\"103C-INIIT-ISP-TKOM\",\"course_name\":{\"pl\":\"Techniki kompilacji\",\"en\":\"Compilation Techniques\"}},{\"term_id\":\"2020Z\",\"course_id\":\"103A-INxxx-ISP-ZPR\",\"course_name\":{\"pl\":\"Zaawansowane programowanie w C++\",\"en\":\"Advanced C++ Programming\"}}],\"2020L\":[{\"term_id\":\"2020L\",\"course_id\":\"103C-INxxx-ISP-PROZ\",\"course_name\":{\"pl\":\"Programowanie zdarzeniowe\",\"en\":\"Event-Driven Programming\"}},{\"term_id\":\"2020L\",\"course_id\":\"103A-INIIT-ISP-TIN\",\"course_name\":{\"pl\":\"Techniki internetowe\",\"en\":\"Internet Techniques\"}},{\"term_id\":\"2020L\",\"course_id\":\"103A-INIIT-ISP-TIN\",\"course_name\":{\"pl\":\"Techniki internetowe\",\"en\":\"Internet Techniques\"}},{\"term_id\":\"2020L\",\"course_id\":\"103C-INIIT-ISP-TKOM\",\"course_name\":{\"pl\":\"Techniki kompilacji\",\"en\":\"Compilation Techniques\"}},{\"term_id\":\"2020L\",\"course_id\":\"103A-INxxx-ISP-ZPR\",\"course_name\":{\"pl\":\"Zaawansowane programowanie w C++\",\"en\":\"Advanced C++ Programming\"}}]},\"terms\":[{\"id\":\"2020Z\",\"name\":{\"pl\":\"rok akademicki 2020/2021 - sem. zimowy\",\"en\":\"Winter Semester 2020/2021\"},\"start_date\":\"2020-10-01\",\"end_date\":\"2021-02-19\",\"order_key\":1210,\"finish_date\":\"2021-02-19\"},{\"id\":\"2020L\",\"name\":{\"pl\":\"rok akademicki 2019/2020 - sem. letni\",\"en\":\"Summer Semester 2019/2020\"},\"start_date\":\"2020-02-22\",\"end_date\":\"2020-09-30\",\"order_key\":1190,\"finish_date\":\"2020-09-30\"},{\"id\":\"2019Z\",\"name\":{\"pl\":\"rok akademicki 2019/2020 - sem. zimowy\",\"en\":\"Winter Semester 2019/2020\"},\"start_date\":\"2019-10-01\",\"end_date\":\"2020-02-21\",\"order_key\":1180,\"finish_date\":\"2020-02-21\"},{\"id\":\"2019L\",\"name\":{\"pl\":\"rok akademicki 2018/2019 - sem. letni\",\"en\":\"Summer Semester 2018/2019\"},\"start_date\":\"2019-02-18\",\"end_date\":\"2019-09-30\",\"order_key\":1160,\"finish_date\":\"2019-09-30\"},{\"id\":\"2018Z\",\"name\":{\"pl\":\"rok akademicki 2018/2019 - sem. zimowy\",\"en\":\"Winter Semester 2018/2019\"},\"start_date\":\"2018-10-01\",\"end_date\":\"2019-02-17\",\"order_key\":1150,\"finish_date\":\"2019-02-17\"},{\"id\":\"2018L\",\"name\":{\"pl\":\"rok akademicki 2017/2018 - sem. letni\",\"en\":\"Summer Semester 2017/2018\"},\"start_date\":\"2018-02-19\",\"end_date\":\"2018-09-30\",\"order_key\":1130,\"finish_date\":\"2018-09-30\"}]}";
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

                var resultGroups = new Mock<IRestResponse>();
                resultGroups.Setup(x => x.Content).Returns(lecturerGroupsJson);
                resultGroups.Setup(x => x.IsSuccessful).Returns(true);
                restClientMock.Setup(x => x.Execute(It.Is<RestRequest>(x => x.Resource=="groups/lecturer")))
                    .Returns(resultGroups.Object);

                var resultAccessToken = new Mock<IRestResponse>();
                resultAccessToken.Setup(x => x.Content).Returns("oauth_token=CZY4hw2aqKVcw7uWMN3G&oauth_token_secret=5SU2wm6XX8XuQxeGdNQTmTHtwB9tPCHgb6eemkmQ");
                resultAccessToken.Setup(x => x.IsSuccessful).Returns(true);
                restClientMock.Setup(x => x.Execute(It.Is<RestRequest>(x => x.Resource== "oauth/access_token")))
                    .Returns(resultAccessToken.Object);

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