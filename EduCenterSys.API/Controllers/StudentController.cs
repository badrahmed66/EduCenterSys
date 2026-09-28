
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Interfaces;
using EduCenterSys.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EduCenterSys.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController(IStudentService service) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult<IReadOnlyList<StudentDtos.Read>>> GetAll()
    {
        var result = await service.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDtos.Read>> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await service.GetByIdAsync(id, cancellationToken);

        return result is null ? NotFound() : result;
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync(StudentDtos.Create dto, CancellationToken cancellationToken)
    {
        if (dto is null)
            return BadRequest();

        var result = await service.AddAsync(dto, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = result.StudentId }, result);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken ct)
    {
        await service.DeleteAsync(id, ct);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, StudentDtos.Update dto, CancellationToken ct)
    {
        await service.UpdateAsync(id, dto, ct);
        return NoContent();
    }
}