
using EduCenterSys.Domain.Entities;
using EduCenterSys.Domain.Interfaces;
using EduCenterSys.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EduCenterSys.Infrastructure.Repositories;

public class TeacherRepository(AppDbContext context) : ITeacherRepository
{
    public async Task<IReadOnlyList<GroupSchedule>?> GetSchedulesByTeacherId(int teacherId, CancellationToken ct)
    {
        return await context.Set<GroupSchedule>()
                        .AsNoTracking()
                        .Where(s => s.Group.TeacherId == teacherId)
                        .ToListAsync(ct);
    }
}