
using EduCenterSys.Domain.Entities;

namespace EduCenterSys.Domain.Interfaces;

public interface ITeacherRepository
{
    Task<IReadOnlyList<GroupSchedule>?> GetSchedulesByTeacherId(int teacherId, CancellationToken ct = default);
}