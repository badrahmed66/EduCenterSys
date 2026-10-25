
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Validations;

namespace EduCenterSys.Application.Interfaces;

public interface ISubjectService
{
    Task<bool> ExistsAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<SubjectDtos.Read>> GetAllAsync(PagingQuery page, CancellationToken cancellationToken = default);

    Task<SubjectDtos.Read> GetByIdAsync(int id, CancellationToken ct = default);

    Task DeleteAsync(int id, CancellationToken ct = default);
    Task UpdateAsync(int id, SubjectDtos.Update dto, CancellationToken ct = default);

    Task<SubjectDtos.Read> AddAsync(SubjectDtos.Create dto, CancellationToken ct = default);
}