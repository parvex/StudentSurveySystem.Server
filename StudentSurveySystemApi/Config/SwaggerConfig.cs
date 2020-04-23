using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Server.Config
{
    public class SwaggerConfig
    {
        public static void Setup(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Student survey system API", Version = "v1" });

                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}