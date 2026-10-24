
using EduCenterSys.Application.DTOs;
using FluentValidation;

namespace EduCenterSys.Application.Validations.Group;

public class UpdateGroupDtosValidator : AbstractValidator<GroupDtos.Update>
{
    public UpdateGroupDtosValidator()
    {
        RuleFor(g => g.Capacity)
            .GreaterThan((byte)0)
            .WithMessage("Capacity must greater than 0")
            .When(g => g.Capacity is not null);

        RuleFor(g => g.TeacherId)
            .GreaterThan(0)
            .WithMessage("Select a valid teacher")
            .When(g => g.TeacherId is not null);

        RuleFor(g => g.GradeId)
            .GreaterThan(0)
            .WithMessage("Select a valid grade")
            .When(g => g.GradeId is not null);

        RuleFor(g => g.SubjectId)
            .GreaterThan(0)
            .WithMessage("Select a valid subject")
            .When(g => g.SubjectId is not null);

        RuleFor(g => g.Status)
            .IsInEnum()
            .WithMessage("Invalid status value")
            .When(g => g.Status is not null);

        RuleFor(g => g.Schedules)
            .NotEmpty()
            .WithMessage("Schedule shouldn't be empty")
            .When(g => g.Schedules is not null);

        RuleFor(g => g.Schedules)
            .Must(schedules => !HasOverlap(schedules))
            .WithMessage("Group schedules must not overlap on the same day.")
            .When(g => g.Schedules is not null && g.Schedules.Count > 0);

    }
    private static bool HasOverlap(IEnumerable<GroupScheduleDtos.Update> schedules) =>
        schedules.GroupBy(s => s.Day)
                 .Any(day => day.Any(s1 => day.Any(s2 =>
                 !ReferenceEquals(s1, s2)
                 && s1.StartHour < s2.EndHour
                 && s1.EndHour > s2.StartHour)));
}