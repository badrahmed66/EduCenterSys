
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduCenterSys.API.Controllers;

[ApiController]
[Route("[Controller]")]
public class TeacherController(ITeacherService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TeacherDtos.Read>>> GetAllAsync()
    {
        var result = await service.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeacherDtos.Read>> GetByIdAsync(int id, CancellationToken ct)
    {
        var result = await service.GetByIdAsync(id, ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TeacherDtos.Read>> AddAsync(TeacherDtos.Create dto, CancellationToken ct)
    {
        var result = await service.AddAsync(dto, ct);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct = default)
    {
        await service.DeleteAsync(id, ct);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TeacherDtos.Update dto, CancellationToken ct)
    {
        await service.UpdateAsync(id, dto, ct);

        return NoContent();
    }

    [HttpPost("{teacherId}/qualification")]
    public async Task<ActionResult<TeacherQualificationDtos.Read>> AddQualificationAsync(int teacherId, TeacherQualificationDtos.Create dto, CancellationToken ct = default)
    {
        var result = await service.AddQualificationAsync(teacherId, dto, ct);
        return Ok(result);
    }

    [HttpGet("{teacherId}/qualification")]
    public async Task<ActionResult<TeacherQualificationDtos.Read>> GetAllQualifications(int teacherId, CancellationToken ct = default)
    {
        var result = await service.GetAllQualificationAsync(teacherId: teacherId, ct: ct);

        return Ok(result);
    }

    [HttpDelete("{teacherId}/qualification/{qualificationId}")]
    public async Task<IActionResult> DeleteQualification(int teacherId, int qualificationId, CancellationToken ct = default)
    {
        await service.DeleteQualificationAsync(teacherId, qualificationId, ct);
        return NoContent();
    }

    [HttpPut("{teacherId}/qualification/{qualificationId}")]
    public async Task<IActionResult> UpdateQualificationAsync(int teacherId, int qualificationId, TeacherQualificationDtos.Update dto, CancellationToken ct = default)
    {
        await service.UpdateQualificationAsync(teacherId, qualificationId, dto, ct);
        return NoContent();
    }

   
}