using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SmartReport.BackEnd.BusinessLogicLayer.DBInitializer;
using SmartReport.BackEnd.BusinessLogicLayer.Extensions;
using SmartReport.BackEnd.BusinessLogicLayer.Interfaces;
using SmartReport.BackEnd.BusinessLogicLayer.Services;
using SmartReport.BackEnd.WebAPI.Hubs;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SmartReport.BackEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDependencies(Configuration);
            services.AddControllers();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUser, CurrentWebUser>();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "v1",
                    new OpenApiInfo()
                    {
                        Title = "SmartReport API",
                        Version = "1",
                        Description = "SmartReport API Specification.",
                    });
                setupAction.AddSecurityDefinition("bearer", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token to access this API",
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearer",
                            },
                        }, new List<string>()
                    },
                });
            });
         //   services.AddDirectoryBrowser();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", builder => builder
                    .SetIsOriginAllowed(_ => true)
                    .WithOrigins("https://smart-report-frontend.azurewebsites.net")
                    .WithOrigins("http://smart-report-frontend.azurewebsites.net")
                    .WithOrigins("https://localhost:44362")
                    .WithOrigins("http://localhost:44362")
                    .WithOrigins("https://localhost:3000")
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
            });
            services.AddSignalR(o =>
            {
                o.EnableDetailedErrors = true;
            });
        }

    //    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "backups");

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           // app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("AllowAnyOrigin");

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "SmartReport API");

                setupAction.RoutePrefix = string.Empty;

                setupAction.DefaultModelExpandDepth(2);
                setupAction.DefaultModelRendering(ModelRendering.Model);
                setupAction.DocExpansion(DocExpansion.None);
                setupAction.EnableDeepLinking();
                setupAction.DisplayOperationId();
            });

            app.UseAuthentication();
            app.UseAuthorization();
            /*
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = "/backups"
            });
            */
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notification");
            });
            DatabaseInitializer.InitializeAsync(app.ApplicationServices).Wait();
        }
    }
}
