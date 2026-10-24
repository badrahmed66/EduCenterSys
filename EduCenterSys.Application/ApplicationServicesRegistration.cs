
using System.Reflection;
using EduCenterSys.Application.Interfaces;
using EduCenterSys.Application.Services;
using EduCenterSys.Application.Validations.Group;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EduCenterSys.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<ITeacherQualificationService, TeacherQualificationService>();

        services.AddScoped<ISubjectService, SubjectService>();

        services.AddScoped<IGradeService, GradeService>();
        services.AddScoped<IGroupService, GroupService>();

        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssemblyContaining<CreateGroupDtosValidator>();
        return services;
    }
}