
using FluentValidation;

namespace EduCenterSys.Application.Validations;

public record PagingQuery(int PageIndex = 1, int PageSize = 50);

public class PagingQueryValidator : AbstractValidator<PagingQuery>
{
    public PagingQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 50);
    }
}