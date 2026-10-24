
namespace EduCenterSys.Application.DTOs;

public class GroupScheduleDtos
{
    public record Read(
       int GroupScheduleId,
       DayOfWeek Day,
       TimeOnly StartHour,
       TimeOnly EndHour,
       int GroupId
       );

    public record Create(
       DayOfWeek Day,
       TimeOnly StartHour,
       TimeOnly EndHour,
       int GroupId
       ) : IScheduleSlot;

    public record Update(
        int GroupScheduleId,
        DayOfWeek Day,
        TimeOnly StartHour,
        TimeOnly EndHour,
        int GroupId
    ) : IScheduleSlot;
}