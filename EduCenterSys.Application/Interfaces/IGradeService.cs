
using EduCenterSys.Application.DTOs;

namespace EduCenterSys.Application.Interfaces;

public interface IGradeService
{
    Task<bool> ExistsAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<GradeDtos.Read>> GetAllAsync(int pageIndex = 1, int pageSize = 50, CancellationToken ct = default);

    Task<GradeDtos.Read> GetByIdAsync(int id, CancellationToken ct = default);

    Task DeleteAsync(int id, CancellationToken ct = default);

    Task UpdateAsync(int id, GradeDtos.Update dto, CancellationToken ct = default);

    Task<GradeDtos.Read> AddAsync(GradeDtos.Create dto, CancellationToken ct = default);
}