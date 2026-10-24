
namespace EduCenterSys.Application.DTOs;

public interface IScheduleSlot
{
    DayOfWeek Day { get; }
    TimeOnly StartHour { get; }
    TimeOnly EndHour { get; }
}