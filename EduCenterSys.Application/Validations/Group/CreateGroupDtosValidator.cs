using EduCenterSys.Application.DTOs;
using EduCenterSys.Domain.Entities;
using EduCenterSys.Domain.Interfaces;
using EduCenterSys.Domain.Enums;
using FluentValidation;

namespace EduCenterSys.Application.Validations.Group;

public class CreateGroupDtosValidator : AbstractValidator<GroupDtos.Create>
{

    public CreateGroupDtosValidator()
    {

        RuleFor(g => g.Capacity)
            .GreaterThan((byte)0)
            .WithMessage("Group's Capacity must be greater then 0");

        RuleFor(g => g.TeacherId)
            .GreaterThan(0)
            .WithMessage("Must select a teacher for the group");

        RuleFor(g => g.SubjectId)
            .GreaterThan(0)
            .WithMessage("Must select a Subject form the group");

        RuleForEach(g => g.Schedules)
            .Must(s => s.StartHour < s.EndHour)
            .WithMessage("Schedule start time must be before its end")
            .When(g => g.Schedules is not null && g.Schedules.Count > 0);

        RuleFor(g => g.Schedules)
            .Must(schedules => !HasOverlap(schedules))
            .WithMessage("Group schedules must not overlap on the same day.")
            .When(g => g.Schedules is not null && g.Schedules.Count > 0);
    }

    private static bool HasOverlap(IEnumerable<GroupScheduleDtos.Create> schedules) =>
        schedules.GroupBy(s => s.Day)
                 .Any(day => day.Any(s1 => day.Any(s2 =>
                 !ReferenceEquals(s1, s2)
                 && s1.StartHour < s2.EndHour
                 && s1.EndHour > s2.StartHour)));


}