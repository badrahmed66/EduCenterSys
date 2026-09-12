
using System.Reflection;
using EduCenterSys.Application.Interfaces;
using EduCenterSys.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EduCenterSys.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IStudentService,StudentService>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}