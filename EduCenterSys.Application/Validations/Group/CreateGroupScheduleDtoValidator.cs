
using EduCenterSys.Application.DTOs;
using FluentValidation;

namespace EduCenterSys.Application.Validations.Group;

public class CreateGroupScheduleDtoValidator : AbstractValidator<GroupScheduleDtos.Create>
{
    public CreateGroupScheduleDtoValidator()
    {
        RuleFor(s => s.GroupId)
            .GreaterThan(0)
            .WithMessage("Must Select a valid Group");
        
        RuleFor(s => s)
            .Must(s => s.StartHour < s.EndHour)
            .WithMessage("Schedule start time must be before its end");
        
        
    }
}