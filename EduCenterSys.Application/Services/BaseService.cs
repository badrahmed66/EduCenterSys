
using EduCenterSys.Domain.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Data.Common;
using System.Linq.Expressions;
namespace EduCenterSys.Application.Services;

public abstract class BaseService<T, TDtoRead, TDtoCreate, TDtoUpdate>
(IGenericRepository<T> repository, IUnitOfWork unitOfWork, IMapper mapper)
    where TDtoRead : class
    where TDtoCreate : class
    where TDtoUpdate : class
    where T : class
{
    protected virtual Expression<Func<T, bool>>? GetDuplicateCheckExpression(TDtoCreate dto) => null;

    protected virtual Expression<Func<T, bool>>? GetDuplicateCheckExpression(TDtoUpdate dto, int id) => null;

    public virtual async Task<TDtoRead> AddAsync(TDtoCreate createDto, CancellationToken cancellationToken = default)
    {
        // check if the record exists in the system or not
        var duplicatePredicate = GetDuplicateCheckExpression(createDto);

        if (duplicatePredicate != null)
        {
            bool existsResult = await repository.IsExistsAsync(duplicatePredicate, cancellationToken);

            if (existsResult)
                throw new InvalidOperationException("This Record Already exists in the system");
        }

        var entityToAdd = mapper.Map<T>(createDto);

        await repository.AddAsync(entityToAdd, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<TDtoRead>(entityToAdd);
    }

    public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);

        var entity = await repository.GetByIdAsync(id, cancellationToken)
        ?? throw new KeyNotFoundException($"Student with ID {id} was not found.");

        repository.Delete(entity);

        int rowEffected = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (rowEffected <= 0)
            throw new InvalidOperationException($"Failed to delete Student with ID {id}.");
    }

    public virtual async Task<IReadOnlyList<TDtoRead>> GetAllAsync(int pageIndex = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        const int maxPageSize = 50;

        if (pageIndex < 1) pageIndex = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > maxPageSize) pageSize = maxPageSize;

        return await repository.GetQueryable()
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ProjectTo<TDtoRead>(mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);
    }

    public virtual async Task<TDtoRead> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);

        string primaryKeyName = $"{typeof(T).Name}Id";

        var result = await repository
                        .GetQueryable()
                        .Where(e => EF.Property<int>(e, primaryKeyName) == id)
                        .ProjectTo<TDtoRead>(mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(cancellationToken) ?? throw new KeyNotFoundException($"{typeof(T).Name} entity with id {id} was not found.");
        return result;
    }

    public virtual Task SoftDeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual async Task UpdateAsync(int id, TDtoUpdate updateDto, CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);

        var entity = await repository.GetByIdAsync(id, cancellationToken)
                            ?? throw new KeyNotFoundException($"Entity with id {id} was not found.");

        // check if the record exists in the system or not
        var duplicatePredicate = GetDuplicateCheckExpression(updateDto, id);

        if (duplicatePredicate != null)
        {
            bool existsResult = await repository.IsExistsAsync(duplicatePredicate, cancellationToken);

            if (existsResult)
                throw new InvalidOperationException("This Record Already exists in the system");
        }

        mapper.Map(updateDto, entity);

        repository.Update(entity);

        int rowEffected = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (rowEffected <= 0)
            throw new InvalidOperationException($"Failed to update Student with ID {id}.");

    }


}