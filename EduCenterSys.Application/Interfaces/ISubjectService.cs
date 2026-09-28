
using EduCenterSys.Application.DTOs;

namespace EduCenterSys.Application.Interfaces;

public interface ISubjectService
{
    Task<IReadOnlyList<SubjectDtos.Read>> GetAllAsync(int pageIndex = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    Task<SubjectDtos.Read?> GetByIdAsync(int id, CancellationToken ct = default);

    Task DeleteAsync(int id, CancellationToken ct = default);
    Task UpdateAsync(int id, SubjectDtos.Update dto, CancellationToken ct = default);

    Task<SubjectDtos.Read> AddAsync(SubjectDtos.Create dto, CancellationToken ct = default);
}