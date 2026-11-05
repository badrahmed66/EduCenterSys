
using EduCenterSys.Domain.Interfaces;

namespace EduCenterSys.Domain.Validation;

public static class ScheduleValidator
{
    public static bool HasOverlap(IEnumerable<IScheduleSlot> schedules)
    {

        return schedules.GroupBy(schedule => schedule.Day)
             .Any(gs => gs.Any(s1 => gs.Any(s2 =>
             !ReferenceEquals(s1, s2) &&
             s1.StartHour < s2.EndHour &&
             s1.EndHour > s2.StartHour)));
    }

    public static bool IsValidPeriod(IEnumerable<IScheduleSlot> schedules) =>
        schedules.All(s1 => s1.StartHour < s1.EndHour);

}