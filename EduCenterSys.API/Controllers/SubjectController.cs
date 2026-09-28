
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduCenterSys.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectController(ISubjectService service) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult<IReadOnlyList<SubjectDtos.Read>>> GetAllAsync(CancellationToken ct = default)
    => Ok(await service.GetAllAsync(cancellationToken: ct));

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