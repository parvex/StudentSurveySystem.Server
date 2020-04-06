using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using StudentSurveySystemApi.Entities;
using StudentSurveySystemApi.Helpers;
using StudentSurveySystemApi.Services;

namespace Tests
{
    public abstract class TestBase
    {
        protected readonly DbContextOptions<SurveyContext> options;
        protected readonly SurveyContext context;

        protected TestBase()
        {
            options = new DbContextOptionsBuilder<SurveyContext>()
                .UseInMemoryDatabase(databaseName: "In_memory_test_db")
                .Options;

            AppSettings app = new AppSettings() { }; // Sample property
            // Make sure you include using Moq;
            var mock = new Mock<IOptions<AppSettings>>();
            // We need to set the Value of IOptions to be the SampleOptions Class
            mock.Setup(ap => ap.Value).Returns(app);


            context = new SurveyContext(options);

            using (var seedContext = new SurveyContext(options))
            {
                 DbInitializer.Seed(context, new UserService(context, mock.Object));
            }

        }
    }
}