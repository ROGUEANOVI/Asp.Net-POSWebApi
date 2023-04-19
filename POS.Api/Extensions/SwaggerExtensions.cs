using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace POS.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var openApi = new OpenApiInfo
            {
                Title = "POS WEB API",
                Version = "v1",
                Description = "Punto de Venta Web Api",
                TermsOfService = new Uri("https://opensource.org/licenses/"),
                Contact = new OpenApiContact
                {
                    Name = "ROGUEANOVI",
                    Email = "ovidioromero66@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/ovidio-antonio-romero-guerrero/")
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LiCX",
                    Url = new Uri("https://opensource.org/licenses/")
                }
            };

            services.AddSwaggerGen(x =>
            {
                openApi.Version = "v1";
                x.SwaggerDoc("v1", openApi);

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Jwt Authentication",
                    Description = "Jwt Bearer Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "Jwt",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                //Button Authorize for Bearer Token
                x.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[] {} }
                });

            });

            return services;
        }
    }
}
