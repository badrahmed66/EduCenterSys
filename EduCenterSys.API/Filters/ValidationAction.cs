using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EduCenterSys.API.Filters;

public class ValidationAction : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null) continue;

            var validationType = typeof(IValidator<>).MakeGenericType(argument.GetType());

            var validator = context.HttpContext.RequestServices.GetService(validationType) as IValidator;

            if (validator is not null)
            {
                var validationContext = new ValidationContext<object>(argument);

                var validationResult = await validator.ValidateAsync(validationContext);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors
                                    .GroupBy(e => e.PropertyName)
                                    .ToDictionary(
                                        failureGroup => failureGroup.Key,
                                        failureGroup => failureGroup.Select(e => e.ErrorMessage).ToArray()
                                        );

                    var problemDetails = new ValidationProblemDetails(errors)
                    {
                        Title = "One or more validation erros occured",
                        Status = StatusCodes.Status400BadRequest,
                        Instance = context.HttpContext.Request.Path
                    };

                    context.Result = new BadRequestObjectResult(problemDetails);
                    return;
                }
            }
        }

        await next();
    }
}