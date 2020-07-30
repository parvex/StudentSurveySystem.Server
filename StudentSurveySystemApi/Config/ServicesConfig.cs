using Microsoft.Extensions.DependencyInjection;
using Server.Services;

namespace Server.Config
{
    public class ServicesConfig
    {
        public static void Setup(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPushNotificationService, PushNotificationService>();
            services.AddScoped<IUsosApi, UsosApi>();
        }
    }
}