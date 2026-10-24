using System.Text.Json.Serialization;
using EduCenterSys.API.ExceptionHandlers;
using EduCenterSys.API.Filters;
using EduCenterSys.Application;
using EduCenterSys.Application.Validations.Group;
using EduCenterSys.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ValidationAction>();

//Registrations
builder.Services.AddControllers(options =>
{
    // يمنع ASP.NET Core من حذف كلمة Async من أسماء الـ Actions أثناء الـ Routing
    options.SuppressAsyncSuffixInActionNames = false;

    options.Filters.Add<ValidationAction>();
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// for register all the Validation's classes one time
builder.Services.AddValidatorsFromAssemblyContaining<CreateGroupDtosValidator>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
    options.SuppressModelStateInvalidFilter = true
);
builder.Services.AddOpenApi();

builder.Services.AddApplicationServices();
builder.Services.AddRepositoriesRegistration(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
});

var app = builder.Build();

app.UseExceptionHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStatusCodePages();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
