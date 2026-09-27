
using EduCenterSys.Application.DTOs;

namespace EduCenterSys.Application.Interfaces;

public interface ITeacherQualificationService
{
    Task DeleteAsync(int teacherId, int qulalificationId, CancellationToken ct = default);
    Task UpdateAsync(int teacherId, int qulalificationId, TeacherQualificationDtos.Update dto, CancellationToken ct);

    Task<TeacherQualificationDtos.Read> AddAsync(TeacherQualificationDtos.Create cto, CancellationToken ct = default);
    
    Task<IReadOnlyList<TeacherQualificationDtos.Read>> GetAllAsync(int teacherId, int pageIndex = 1, int pageSize = 10, CancellationToken ct = default);
}