
namespace EduCenterSys.Domain.Entities;

public class GroupSchedule(DayOfWeek day, byte startHour, byte endHour, int groupId)
{
    public int GroupScheduleId { get; private set; }
    public DayOfWeek Day { get; private set; } = day;
    public byte StartHour { get; private set; } = startHour;
    public byte EndHour { get; private set; } = endHour;
    public int GroupId { get; private set; } = groupId;

    public Group Group{get;private set;} = default!;
}
    