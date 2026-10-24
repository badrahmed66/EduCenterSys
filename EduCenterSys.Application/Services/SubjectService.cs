
using System.Linq.Expressions;
using AutoMapper;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Helpers;
using EduCenterSys.Application.Interfaces;
using EduCenterSys.Domain.Entities;
using EduCenterSys.Domain.Interfaces;

namespace EduCenterSys.Application.Services;

public class SubjectService : BaseService<Subject, SubjectDtos.Read, SubjectDtos.Create, SubjectDtos.Update>, ISubjectService
{
    private readonly IGenericRepository<Subject> _genericRepo;
    public SubjectService(IGenericRepository<Subject> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
    {
        _genericRepo = repository;
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken ct)
        => await _genericRepo.IsExistsAsync(g => g.SubjectId == id, ct);
        
    protected override Expression<Func<Subject, bool>>? GetDuplicateCheckExpression(SubjectDtos.Create dto)
    {
        var cleanName = dto.Name.ToStandardFormat();
        return s => s.Name == cleanName;
    }

    protected override Expression<Func<Subject, bool>>? GetDuplicateCheckExpression(SubjectDtos.Update dto, int id)
    {
        var cleanName = dto.Name.ToStandardFormat();
        return s => s.Name == cleanName && s.SubjectId != id;
    }

}