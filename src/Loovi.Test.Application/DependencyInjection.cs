using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FluentValidation;
using Loovi.Test.Application.Common.Behaviors;
using Loovi.Test.Domain;
using Loovi.Test.Application.Tasks.CreateTask;

namespace Loovi.Test.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            return services;
        }
    }
}
