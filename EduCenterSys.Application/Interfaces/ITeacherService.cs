
using EduCenterSys.Application.DTOs;
using EduCenterSys.Domain.Enums;

namespace EduCenterSys.Application.Interfaces;

public interface ITeacherService
{
    Task<bool> ExistsAsync(int id, CancellationToken ct);
    Task<bool> IsQualified(int id, int subjectId, int gradeId, CancellationToken ct);
    Task<IReadOnlyList<TeacherDtos.Read>> GetAllAsync(int pageIndex = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    Task<TeacherDtos.Read> GetByIdAsync(int id, CancellationToken ct = default);
    Task<TeacherDtos.Read> AddAsync(TeacherDtos.Create dto, CancellationToken ct = default);

    Task DeleteAsync(int id, CancellationToken ct = default);

    Task UpdateAsync(int id, TeacherDtos.Update dto, CancellationToken ct);

    Task<TeacherQualificationDtos.Read> AddQualificationAsync(int teacherId, TeacherQualificationDtos.Create dto, CancellationToken ct = default);

    Task<IReadOnlyList<TeacherQualificationDtos.Read>> GetAllQualificationAsync(int teacherId, int pageIndex = 1, int pageSize = 10, CancellationToken ct = default);

    Task DeleteQualificationAsync(int teacherId, int qualificationId, CancellationToken ct = default);

    Task UpdateQualificationAsync(int teacherId, int qualificationId, TeacherQualificationDtos.Update dto, CancellationToken ct = default);
}