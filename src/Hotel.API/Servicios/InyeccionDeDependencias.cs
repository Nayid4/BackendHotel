
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Hotel.API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Hotel.API.Servicios
{
    public static class InyeccionDeDependencias
    {
        public static IServiceCollection AddPresentation(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AddControllers();
            servicios.AddOpenApi();
            servicios.AddEndpointsApiExplorer();
            servicios.AddTransient<GlobalExceptionHandlingMiddleware>();

            servicios.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuracion["Jwt:Audience"],
                    ValidIssuer = configuracion["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuracion["Jwt:Key"]!))
                };
            });

            servicios.AddCors(options =>
            {
                
                options.AddPolicy("web", policyBuilder =>
                {
                    policyBuilder.WithOrigins(
                        "http://localhost:4000",
                        "https://localhost:4000",
                        "https://localhost:8500",
                        "https://localhost:80",
                        "https://frontend-gestion-de-series-animadas.vercel.app",
                        "https://frontend-gestion-de-series-animadas-angular.vercel.app",
                        "https://frontend-gestion-de-series-animadas-angular-2ijx8svoq.vercel.app",
                        "https://frontend-gestion-de-series-animadas-git-876feb-nayids-projects.vercel.app"
                        );
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowAnyMethod();
                    policyBuilder.AllowCredentials();
                });
                

                /*options.AddPolicy("web", policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin();
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowAnyMethod();
                });*/

            });

            return servicios;
        }
    }
}
