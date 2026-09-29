
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
    public GradeService(IGenericRepository<Grade> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
    {
    }

    protected override Expression<Func<Grade, bool>>? GetDuplicateCheckExpression(GradeDtos.Create dto)
    {
        return l => l.Level == dto.Level;
    }

    protected override Expression<Func<Grade, bool>>? GetDuplicateCheckExpression(GradeDtos.Update dto, int id)
    {
        return l => l.Level == dto.Level && id != l.GradeId;
    }

}