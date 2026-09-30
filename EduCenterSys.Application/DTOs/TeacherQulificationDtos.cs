
using System.ComponentModel.DataAnnotations;
using EduCenterSys.Domain.Enums;

namespace EduCenterSys.Application.DTOs;

public class TeacherQualificationDtos
{
    public record Read
    {
        public int TeacherQualificationId { get; private set; }

        public string SubjectName { get; init; } = string.Empty;
        public GradeLevel GradeLevel { get; init; }
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
        public  int? SubjectId { get; init; }
        public  int? GradeId { get; init; }
    }
}