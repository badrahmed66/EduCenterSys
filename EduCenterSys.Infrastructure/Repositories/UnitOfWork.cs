

using EduCenterSys.Domain.Interfaces;
using EduCenterSys.Infrastructure.Data;

namespace EduCenterSys.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public void Dispose()
    {
        context.Dispose();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}