using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SmartReport.BackEnd.BusinessLogicLayer.Interfaces;
using SmartReport.BackEnd.BusinessLogicLayer.Services;
using SmartReport.BackEnd.CrossCuttingConcern.Configurations;
using SmartReport.BackEnd.CrossCuttingConcern.Entities;
using SmartReport.BackEnd.DataAccessLayer.Contexts;
using SmartReport.BackEnd.DataAccessLayer.Interfaces;
using SmartReport.BackEnd.DataAccessLayer.Repositories;
using System;
using System.Reflection;

namespace SmartReport.BackEnd.BusinessLogicLayer.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static AuthenticationBuilder AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddServices(configuration)
                .AddIdentity(configuration);
            return services.AddTokenAuthentication(configuration);
        }

        public static IdentityBuilder AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = true;
                }
                )
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            string migrationsAssembly = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            return services
                .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly(migrationsAssembly)), ServiceLifetime.Scoped)
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IPlaceService, PlaceService>()
                .AddScoped<IReportService, ReportService>()
                .AddScoped<ITaskService, TaskService>();
        }

        private static AuthenticationBuilder AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            AuthConfiguration authConfiguration = new AuthConfiguration(configuration);
            return services
                .AddSingleton(authConfiguration)
                .AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions => {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = authConfiguration.KEY,

                        ValidateIssuer = true,
                        ValidIssuer = authConfiguration.ISSUER,

                        ValidateAudience = true,
                        ValidAudience = authConfiguration.AUDIENCE,

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}
