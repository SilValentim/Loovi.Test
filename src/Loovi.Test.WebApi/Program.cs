using Loovi.Test.ORM;
using Loovi.Test.Application;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Loovi.Test.Application.Tasks.CreateTask;
using Loovi.Test.WebApi.Middleware;

namespace Loovi.Test.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //TODO HEALTH CHECKS
            // builder.AddBasicHealthChecks();


            //TODO Authentication
            //builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(Program).Assembly);// WebApi
                cfg.AddMaps(typeof(Application.DependencyInjection).Assembly); // Application
            });

            builder.Services
            .AddApplication()
            .AddInfrastructure(builder.Configuration);


            var app = builder.Build();

            app.UseCustomExceptionHandling();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
