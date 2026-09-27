
//using System.Security.AccessControl;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Interfaces;
using EduCenterSys.Domain.Entities;
using EduCenterSys.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduCenterSys.Application.Services;

public class TeacherQualificationService : BaseService<TeacherQualification, TeacherQualificationDtos.Read, TeacherQualificationDtos.Create, TeacherQualificationDtos.Update>, ITeacherQualificationService
{
    private readonly IGenericRepository<TeacherQualification> _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public TeacherQualificationService(IGenericRepository<TeacherQualification> genericRepository, IUnitOfWork unitOfWork, IMapper mapper) : base(genericRepository, unitOfWork, mapper)
    {
        _repository = genericRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<TeacherQualificationDtos.Read>> GetAllAsync(int teacherId, int pageIndex = 1, int pageSize = 10, CancellationToken ct = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(teacherId);

        const int maxPageSize = 50;

        if (pageIndex < 1) pageIndex = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > maxPageSize) pageSize = maxPageSize;

        var result = await _repository.GetQueryable()
                        .Where(q => q.TeacherId == teacherId)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ProjectTo<TeacherQualificationDtos.Read>(_mapper.ConfigurationProvider)
                        .ToListAsync(ct) ?? throw new KeyNotFoundException($"Teacher with {teacherId} is not found");
        return result;
    }

    public async Task DeleteAsync(int teacherId, int qulalificationId, CancellationToken ct = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(teacherId);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(qulalificationId);

        var qualificationToDelete = await _repository.GetQueryable()
                        .FirstOrDefaultAsync(q => q.TeacherQualificationId == qulalificationId && q.TeacherId == teacherId, ct) ?? throw new KeyNotFoundException("Invalid teacher ID or Qualification ID");

        _repository.Delete(qualificationToDelete);

        var rowEffected = await _unitOfWork.SaveChangesAsync(ct);

        if (rowEffected <= 0)
            throw new InvalidOperationException($"Failed to delete qualification with ID {qulalificationId}.");
    }

    public async Task UpdateAsync(int teacherId, int qulalificationId, TeacherQualificationDtos.Update dto, CancellationToken ct)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(teacherId);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(qulalificationId);

        var qualificationToUpdate = await _repository.GetQueryable()
            .FirstOrDefaultAsync(q => q.TeacherQualificationId == qulalificationId && q.TeacherId == teacherId, ct) ?? throw new KeyNotFoundException("Invalid teacher ID or Qualification ID");

        _mapper.Map(dto, qualificationToUpdate);

        _repository.Update(qualificationToUpdate);

        var rowEffected = await _unitOfWork.SaveChangesAsync(ct);

        if (rowEffected <= 0)
            throw new InvalidOperationException($"Failed to update qualification with ID {qulalificationId}");
    }
}