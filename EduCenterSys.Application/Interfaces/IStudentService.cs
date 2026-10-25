
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Validations;
using EduCenterSys.Domain.Entities;

namespace EduCenterSys.Application.Interfaces;

public interface IStudentService
{
    Task<IReadOnlyList<StudentDtos.Read>> GetAllAsync(PagingQuery page, CancellationToken cancellationToken = default);
    Task<StudentDtos.Read> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<StudentDtos.Read> AddAsync(StudentDtos.Create createDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, StudentDtos.Update updateDto, CancellationToken cancellationToken = default);
    Task SoftDeleteAsync(int id, CancellationToken cancellationToken = default);
}