using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Website_Selling_Movie_Tickets.Application.Common.Behaviours;

namespace Website_Selling_Movie_Tickets.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
            services.AddAutoMapper(Assembly.GetExecutingAssembly())
                    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                    .AddMediatR(cfg => {
                        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                    })
                    .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>))
                    .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>))
                    .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }
}
