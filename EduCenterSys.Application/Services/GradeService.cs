
using System.Linq.Expressions;
using AutoMapper;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Helpers;
using EduCenterSys.Application.Interfaces;
using EduCenterSys.Domain.Entities;
using EduCenterSys.Domain.Interfaces;

namespace EduCenterSys.Application.Services;

public class GradeService : BaseService<Grade, GradeDtos.Read, GradeDtos.Create, GradeDtos.Update>, IGradeService
{
    private readonly IGenericRepository<Grade> _genericRepo;
    public GradeService(IGenericRepository<Grade> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
    {
        _genericRepo = repository;
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken ct)
        => await _genericRepo.IsExistsAsync(g => g.GradeId == id, ct);


    protected override Expression<Func<Grade, bool>>? GetDuplicateCheckExpression(GradeDtos.Create dto)
    {
        return l => l.Level == dto.Level;
    }

    protected override Expression<Func<Grade, bool>>? GetDuplicateCheckExpression(GradeDtos.Update dto, int id)
    {
        return l => l.Level == dto.Level && id != l.GradeId;
    }

}