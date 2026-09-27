
namespace EduCenterSys.Domain.Entities;

public class GroupSchedule
{
    private GroupSchedule() { }
    public GroupSchedule(DayOfWeek day, byte startHour, byte endHour, int groupId)
    {
        Day = day;
        StartHour = startHour;
        EndHour = endHour;
        GroupId = groupId;
    }
    public int GroupScheduleId { get; private set; }
    public DayOfWeek Day { get; private set; }
    public byte StartHour { get; private set; }
    public byte EndHour { get; private set; }
    public int GroupId { get; private set; }

    public Group Group { get; private set; } = default!;
}
