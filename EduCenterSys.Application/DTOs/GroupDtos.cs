
using Microsoft.EntityFrameworkCore;

namespace EduCenterSys.Application.DTOs;

public class GroupDtos
{
    public record Read(int GroupId, byte Capacity, string GradeName, string SubjectName, string TeacherName, string Status);

    public record Create(byte Capacity, int GradeId, int SubjectId, int TeacherId, EntityState Status);

    public record Update(byte Capacity, int GradeId, int SubjectId, int TeacherId, EntityState Status);
}