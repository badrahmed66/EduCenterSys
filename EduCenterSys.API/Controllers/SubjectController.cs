
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Interfaces;
using EduCenterSys.Application.Validations;
using Microsoft.AspNetCore.Mvc;

namespace EduCenterSys.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectController(ISubjectService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SubjectDtos.Read>>> GetAllAsync([FromQuery] PagingQuery page, CancellationToken ct = default)
    {
        var result = await service.GetAllAsync(page, ct);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubjectDtos.Read?>> GetByIdAsync(int id, CancellationToken ct = default) => Ok(await service.GetByIdAsync(id, ct));

    [HttpPost]
    public async Task<ActionResult<SubjectDtos.Read>> AddAsync(SubjectDtos.Create dto, CancellationToken ct = default)
    {
        var result = await service.AddAsync(dto, ct);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = result.SubjectId }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, SubjectDtos.Update dto, CancellationToken ct = default)
    {
        await service.UpdateAsync(id, dto, ct);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        await service.DeleteAsync(id, ct);
        return NoContent();
    }
}