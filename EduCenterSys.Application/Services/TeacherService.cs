
using System.Data.Common;
using System.Linq.Expressions;
using AutoMapper;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Helpers;
using EduCenterSys.Application.Interfaces;
using EduCenterSys.Application.Validations;
using EduCenterSys.Domain.Entities;
using EduCenterSys.Domain.Enums;
using EduCenterSys.Domain.Interfaces;

namespace EduCenterSys.Application.Services;

public class TeacherService : BaseService<Teacher, TeacherDtos.Read, TeacherDtos.Create, TeacherDtos.Update>, ITeacherService
{
    private readonly ITeacherQualificationService _qualification;
    private readonly IGenericRepository<Teacher> _genericRepository;

    public TeacherService(
        IGenericRepository<Teacher> genericRepository,
        IUnitOfWork unitOfWork, IMapper mapper,
        ITeacherQualificationService qualification,
        IGenericRepository<TeacherQualification> qualificationRepo
        ) : base(genericRepository, unitOfWork, mapper)
    {
        _genericRepository = genericRepository;
        _qualification = qualification;
    }

    public Task<bool> ExistsAsync(int id, CancellationToken ct)
        => _genericRepository.IsExistsAsync(t => t.TeacherId == id, ct);

    protected override Expression<Func<Teacher, bool>>? GetDuplicateCheckExpression(TeacherDtos.Create dto)
    {
        return t => t.NationalId == dto.NationalId;
    }

    protected override Expression<Func<Teacher, bool>>? GetDuplicateCheckExpression(TeacherDtos.Update dto, int id)
    {
        return t => t.NationalId == dto.NationalId && id != t.TeacherId;
    }
    public async Task<TeacherQualificationDtos.Read> AddQualificationAsync(int teacherId, TeacherQualificationDtos.Create dto, CancellationToken ct = default)
    {
        var qualificationToAdd = new TeacherQualificationDtos.Create
        {
            TeacherId = teacherId,
            SubjectId = dto.SubjectId,
            GradeId = dto.GradeId
        };

        return await _qualification.AddAsync(qualificationToAdd, ct);
    }


    public async Task<bool> IsQualified(int id, int subjectId, int gradeId, CancellationToken ct)
     => await _qualification.IsQualifiedAsync(id, subjectId, gradeId, ct);

    public async Task<IReadOnlyList<TeacherQualificationDtos.Read>> GetAllQualificationAsync(int teacherId, PagingQuery page, CancellationToken ct = default)
    {
        return await _qualification.GetAllAsync(teacherId, page, ct);
    }
    public async Task DeleteQualificationAsync(int teacherId, int qualificationId, CancellationToken ct = default) => await _qualification.DeleteAsync(teacherId, qualificationId, ct);

    public async Task UpdateQualificationAsync(int teacherId, int qualificationId, TeacherQualificationDtos.Update dto, CancellationToken ct = default) => await _qualification.UpdateAsync(teacherId, qualificationId, dto, ct);

  
}