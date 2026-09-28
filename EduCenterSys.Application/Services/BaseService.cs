
using EduCenterSys.Domain.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Data.Common;
namespace EduCenterSys.Application.Services;

public abstract class BaseService<T, TDtoRead, TDtoCreate, TDtoUpdate>
(IGenericRepository<T> repository, IUnitOfWork unitOfWork, IMapper mapper)
    where TDtoRead : class
    where TDtoCreate : class
    where TDtoUpdate : class
    where T : class
{
    public virtual async Task<TDtoRead> AddAsync(TDtoCreate createDto, CancellationToken cancellationToken = default)
    {
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

        mapper.Map(updateDto, entity);

        repository.Update(entity);

        int rowEffected = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (rowEffected <= 0)
            throw new InvalidOperationException($"Failed to update Student with ID {id}.");

    }


}