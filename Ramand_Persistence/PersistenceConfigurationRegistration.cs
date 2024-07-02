using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ramand_Application.Model;
using Ramand_Application.ServiceContract.Persistence;

using Ramand_Persistence.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramand_Persistence
{
    public static class PersistenceConfigurationRegistration
    {
        public static IServiceCollection PersistenceConfigurationRegister(this IServiceCollection services
            , IConfiguration configuration)
        {

            services.Configure<JWTSettings>(configuration.GetSection("JWT"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(j => {
                j.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWT:IsSure"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                };
            });

            services.AddScoped(typeof(IAuthenticationService), typeof(AuthenticationRespository));
            services.AddScoped(typeof(IRabbitMqService), typeof(RabbitMqRepository));
            services.AddAuthorization();

            return services;
        }
    }
}
