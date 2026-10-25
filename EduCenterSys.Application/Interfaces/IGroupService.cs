
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Validations;

namespace EduCenterSys.Application.Interfaces;

public interface IGroupService
{
    Task<IReadOnlyList<GroupDtos.Read>> GetAllAsync(PagingQuery page, CancellationToken ct = default);

    Task<GroupDtos.Read> GetByIdAsync(int id, CancellationToken ct = default);

    Task<GroupDtos.Read> AddAsync(GroupDtos.Create dto, CancellationToken ct = default);

    Task UpdateAsync(int id, GroupDtos.Update dto, CancellationToken ct = default);

    Task DeleteAsync(int id, CancellationToken ct = default);

    Task SoftDeleteAsync(int id, CancellationToken ct = default);
}