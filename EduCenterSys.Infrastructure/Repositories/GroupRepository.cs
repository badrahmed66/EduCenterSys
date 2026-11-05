
using EduCenterSys.Domain.Interfaces;
using EduCenterSys.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using EduCenterSys.Domain.Entities;
namespace EduCenterSys.Infrastructure.Repositories;

public class GroupRepository(AppDbContext context) : IGroupRepository
{
    public async Task<Group?> GetByIdWithSchedulesAsync(int groupId, CancellationToken ct = default)
    {
        return await context.Groups
             .Where(g => g.GroupId == groupId)
             .Include(g => g.Schedules)
             .FirstOrDefaultAsync(ct);
    }

}