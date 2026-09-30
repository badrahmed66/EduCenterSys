using System.Text.Json.Serialization;
using EduCenterSys.API.ExceptionHandlers;
using EduCenterSys.Application;
using EduCenterSys.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Registrations
builder.Services.AddControllers(options =>
{
    // يمنع ASP.NET Core من حذف كلمة Async من أسماء الـ Actions أثناء الـ Routing
    options.SuppressAsyncSuffixInActionNames = false;
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
