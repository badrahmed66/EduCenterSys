
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduCenterSys.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradeController(IGradeService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<GradeDtos.Read>>> GetAllAsync(int pageIndex = 1, int pageSize = 50, CancellationToken ct = default)
    {
        var result = await service.GetAllAsync(pageIndex, pageSize, ct);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GradeDtos.Read>> GetByIdAsync(int id, CancellationToken ct = default) => Ok(await service.GetByIdAsync(id, ct));

    [HttpPost]
    public async Task<ActionResult<GradeDtos.Read>> AddAsync(GradeDtos.Create dto, CancellationToken ct = default)
    {
        var grade = await service.AddAsync(dto, ct);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = grade.GradeId }, grade);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        await service.DeleteAsync(id, ct);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, GradeDtos.Update dto, CancellationToken ct = default)
    {
        await service.UpdateAsync(id, dto, ct);
        return NoContent();
    }
}