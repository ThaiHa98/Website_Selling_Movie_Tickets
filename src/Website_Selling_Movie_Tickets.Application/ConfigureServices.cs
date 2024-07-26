using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shared.DTOs.User;
using System.Reflection;
using TinTuc.Application.Helper;
using Website_Selling_Movie_Tickets.Application.Common.Behaviours;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Application.Common.Repositories;

namespace Website_Selling_Movie_Tickets.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
            services.AddAutoMapper(Assembly.GetExecutingAssembly())
                    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                    .AddMediatR(cfg =>
                    {
                        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                    })
                    .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>))
                    .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>))
                    .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
                    .AddTransient<IUserRepository, UserRepository>()
                    .AddTransient<Token>()
                    .AddTransient<IHttpContextAccessor, HttpContextAccessor>()
                    .AddTransient<IGenreRepository, GenreRepository>();
                    
    }
}
