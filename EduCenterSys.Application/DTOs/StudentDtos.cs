
using System.ComponentModel.DataAnnotations;

namespace EduCenterSys.Application.DTOs;

public class StudentDtos
{
    public record Read
    {
        public int StudentId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public string Address { get; init; } = string.Empty;
        public string GuardianPhoneNumber { get; init; } = string.Empty;
        public string GradeName { get; init; } = string.Empty;
    }

    public record Create
    {
        [Required, MaxLength(50)]
        public string Name { get; init; } = string.Empty;

        [Required]
        [RegularExpression(@"^01[0125]\d{8}$")]
        public string PhoneNumber { get; init; } = string.Empty;

        [Required, MaxLength(100)]
        public string Address { get; init; } = string.Empty;

        [Required]
        [RegularExpression(@"^01[0125]\d{8}$")]
        public string GuardianPhoneNumber { get; init; } = string.Empty;

        [Required, Range(1, int.MaxValue)]
        public int GradeId { get; init; }
    }
    public record Update
    {
        [MaxLength(50)]
        public string? Name { get; init; }
        
        [RegularExpression(@"^01[0125]\d{8}$")]
        public string? PhoneNumber { get; init; }

        [MaxLength(100)]
        public string? Address { get; init; }

        [RegularExpression(@"^01[0125]\d{8}$")]
        public string? GuardianPhoneNumber { get; init; }

        [Range(1, int.MaxValue)]
        public int? GradeId { get; init; }
    }
}