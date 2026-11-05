using EduCenterSys.Application.DTOs;
using EduCenterSys.Domain.Entities;
using EduCenterSys.Domain.Interfaces;
using EduCenterSys.Domain.Enums;
using FluentValidation;
using EduCenterSys.Domain.Validation;

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


        RuleFor(g => g.Schedules)
            .Must(s => ScheduleValidator.IsValidPeriod(s))
            .WithMessage("Schedule start time must be before its end")
            .When(g => g.Schedules is not null && g.Schedules.Count > 0);

        RuleFor(g => g.Schedules)
            .Must(schedules => !ScheduleValidator.HasOverlap(schedules))
            .WithMessage("Group schedules must not overlap on the same day.")
            .When(g => g.Schedules is not null && g.Schedules.Count > 0);
    }


}