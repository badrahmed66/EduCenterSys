
using EduCenterSys.Domain.Enums;

namespace EduCenterSys.Application.DTOs;

public class GradeDtos
{
    public record Read(int GradeId, GradeLevel Level);
    public record Create(GradeLevel Level);
    public record Update(GradeLevel Level);
}