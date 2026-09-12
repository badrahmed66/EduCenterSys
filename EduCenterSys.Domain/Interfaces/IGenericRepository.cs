
using System.Linq.Expressions;

namespace EduCenterSys.Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);

    IQueryable<T> GetQueryable();
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    void Delete(T entity);
    void Update(T entity);
    void SoftDelete(T entity);

    Task<bool> IsExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

}