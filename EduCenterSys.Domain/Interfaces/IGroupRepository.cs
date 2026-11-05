
using EduCenterSys.Domain.Entities;

namespace EduCenterSys.Domain.Interfaces;

public interface IGroupRepository
{
    Task<Group?> GetByIdWithSchedulesAsync(int groupId, CancellationToken ct = default);
}