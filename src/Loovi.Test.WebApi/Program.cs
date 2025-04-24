using Loovi.Test.ORM;
using Loovi.Test.Application;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Loovi.Test.Application.Tasks.CreateTask;
using Loovi.Test.WebApi.Middleware;
using Loovi.Test.WebApi.Configuration;

namespace Loovi.Test.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //TODO HEALTH CHECKS
            // builder.AddBasicHealthChecks();


            //TODO Authentication
            //builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.Services
                .AddWebApiServices(builder.Configuration);

            // Application and Infrastructure DI
            builder.Services
                .AddApplication()
                .AddInfrastructure(builder.Configuration);

            // Auth
            builder.Services
                .AddJwtAuthentication(builder.Configuration)
                .AddAuthenticationProvider(builder.Configuration);


            var app = builder.Build();

            app.UseCustomExceptionHandling();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
