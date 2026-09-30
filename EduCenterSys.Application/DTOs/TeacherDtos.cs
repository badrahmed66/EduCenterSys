
using System.ComponentModel.DataAnnotations;
using EduCenterSys.Domain.Enums;

namespace EduCenterSys.Application.DTOs;

public class TeacherDtos
{
    public record Read
    {
        public int TeacherId { get; init; }
        public string NationalId { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public string Address { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public EntityStatus Status { get; init; }
        public byte GroupsNumbers { get; init; }

        public IReadOnlyCollection<TeacherQualificationDtos.Read> Qualifications { get; init; } = [];
    }

    public record Create
    {
        [Required, Length(5, 50)]
        public string Name { get; init; } = string.Empty;

        [Required, Length(14, 14)]
        public string NationalId { get; init; } = string.Empty;

        [Required, Length(3, 100)]
        public string Address { get; init; } = string.Empty;

        [Required]
        [RegularExpression(@"^01[0125]\d{8}$")]
        public string PhoneNumber { get; init; } = string.Empty;

        [Required]
        [EnumDataType(typeof(EntityStatus))]
        public EntityStatus Status { get; init; }

        public IReadOnlyCollection<TeacherQualificationDtos.Create> Qualifications { get; init; } = [];
    }

    public record Update
    {
        [Length(5, 50)]
        public string? Name { get; init; }

        [Length(5, 100)]
        public string? Address { get; init; }

        [Length(14, 14)]
        public string? NationalId { get; init; }

        [RegularExpression(@"^01[0125]\d{8}$")]
        public string? PhoneNumber { get; init; }

        [EnumDataType(typeof(EntityStatus))]
        public EntityStatus? Status { get; init; }

    }

}