
using EduCenterSys.Domain.Enums;

namespace EduCenterSys.Application.DTOs;

public class GroupDtos
{
    public record Read(
     int GroupId,
     byte Capacity,
     GradeLevel GradeLevel,
     string SubjectName,
     string TeacherName,
     Status Status,
     byte EnrollmentsCount,
     List<GroupScheduleDtos.Read> Schedules
     );

    public record Create(
        byte Capacity,
        int GradeId,
        int SubjectId,
        int TeacherId,
        Status Status,
        List<GroupScheduleDtos.Create> Schedules
    );

    public record Update(
        byte? Capacity,
        int? GradeId,
        int? SubjectId,
        int? TeacherId,
        Status? Status,
        List<GroupScheduleDtos.Update>? Schedules
        );
}