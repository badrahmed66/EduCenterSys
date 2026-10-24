
using EduCenterSys.Application.DTOs;

namespace EduCenterSys.Application.Interfaces;

public interface IGroupService
{
    Task<IReadOnlyList<GroupDtos.Read>> GetAllAsync(int pageIndex = 1, int pageSize = 50, CancellationToken ct = default);

    Task<GroupDtos.Read> GetByIdAsync(int id, CancellationToken ct = default);

    Task<GroupDtos.Read> AddAsync(GroupDtos.Create dto, CancellationToken ct = default);

    Task UpdateAsync(int id, GroupDtos.Update dto, CancellationToken ct = default);

    Task DeleteAsync(int id, CancellationToken ct = default);

    Task SoftDeleteAsync(int id, CancellationToken ct = default);
}