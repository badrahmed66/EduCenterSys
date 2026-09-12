
using System.Linq.Expressions;
using EduCenterSys.Domain.Interfaces;
using EduCenterSys.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EduCenterSys.Infrastructure.Repositories;

public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
{
    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await context.Set<T>().AddAsync(entity, cancellationToken);

        return entity;
    }

    public void Delete(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        context.Set<T>()
                .Remove(entity);
    }

    public IQueryable<T> GetQueryable() => context.Set<T>().AsNoTracking();


    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);

        return await context.Set<T>()
                        .FindAsync([id], cancellationToken);

    }

    public void Update(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        context.Set<T>()
                .Update(entity);
    }

    public void SoftDelete(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (entity is ISoftDelete softDeleteEntity)
        {
            softDeleteEntity.IsDeleted = true;
            softDeleteEntity.DeletedAt = DateTimeOffset.UtcNow;
            context.Set<T>().Update(entity);
        }
        else
            throw new InvalidOperationException($"The entity {typeof(T).Name} doesn't support soft delete");
    }

    public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return await context.Set<T>()
                        .AsNoTracking()
                        .AnyAsync(predicate, cancellationToken);

    }
}