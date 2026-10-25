
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Interfaces;
using EduCenterSys.Application.Validations;
using Microsoft.AspNetCore.Mvc;

namespace EduCenterSys.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupController(IGroupService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<GroupDtos.Read>> AddAsync(GroupDtos.Create dto, CancellationToken ct = default)
    {
        var group = await service.AddAsync(dto, ct);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = group.GroupId }, group);
    }

    [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult<GroupDtos.Read>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var result = await service.GetByIdAsync(id, ct);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<GroupDtos.Read>>> GetAllAsync([FromQuery] PagingQuery page, CancellationToken ct = default)
    {
        var result = await service.GetAllAsync(page, ct);

        return Ok(result);
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<IActionResult> Update(int id, GroupDtos.Update dto, CancellationToken ct = default)
    {
        await service.UpdateAsync(id, dto, ct);

        return NoContent();
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<IActionResult> SoftDeleteAsync(int id, CancellationToken ct = default)
    {
        await service.SoftDeleteAsync(id, ct);
        return NoContent();
    }

}
