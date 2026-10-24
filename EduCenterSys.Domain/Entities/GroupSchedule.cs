
namespace EduCenterSys.Domain.Entities;

public class GroupSchedule
{
    private GroupSchedule() { }
    public GroupSchedule(DayOfWeek day, TimeOnly startHour, TimeOnly endHour, int groupId)
    {
        Day = day;
        StartHour = startHour;
        EndHour = endHour;
        GroupId = groupId;
    }
    public int GroupScheduleId { get; private set; }
    public DayOfWeek Day { get; private set; }
    public TimeOnly StartHour { get; private set; }
    public TimeOnly EndHour { get; private set; }
    public int GroupId { get; private set; }

    public Group Group { get; private set; } = default!;
}
