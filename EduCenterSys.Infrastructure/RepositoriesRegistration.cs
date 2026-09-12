
using EduCenterSys.Domain.Interfaces;
using EduCenterSys.Infrastructure.Data;
using EduCenterSys.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduCenterSys.Infrastructure;

public static class RepositoriesRegistration
{
    public static IServiceCollection AddRepositoriesRegistration(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("Default")));

        service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        service.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return service;
    }
}