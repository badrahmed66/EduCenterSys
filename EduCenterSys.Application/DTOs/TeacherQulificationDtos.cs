
using System.ComponentModel.DataAnnotations;

namespace EduCenterSys.Application.DTOs;

public class TeacherQualificationDtos
{
    public record Read
    {
        public int TeacherQualificationId { get; private set; }

        public string SubjectName { get; init; } = string.Empty;
        public string GradeName { get; init; } = string.Empty;
    }

    public record Create
    {
        public int TeacherId { get; init; }

        [Range(1, int.MaxValue)]
        public required int SubjectId { get; init; }

        [Range(1, int.MaxValue)]
        public required int GradeId { get; init; }
    }
    public record Update
    {
        public required int SubjectId { get; init; }
        public required int GradeId { get; init; }
    }
}