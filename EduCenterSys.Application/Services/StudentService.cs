using System.Linq.Expressions;
using AutoMapper;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Helpers;
using EduCenterSys.Application.Interfaces;
using EduCenterSys.Domain.Entities;
using EduCenterSys.Domain.Interfaces;

namespace EduCenterSys.Application.Services;

public class StudentService : BaseService<Student, StudentDtos.Read, StudentDtos.Create, StudentDtos.Update>, IStudentService
{
    public StudentService(IGenericRepository<Student> repository,
    IGenericRepository<Grade> gradeRepo,
    IUnitOfWork unitOfWork,
    IMapper mapper) : base(repository, unitOfWork, mapper)
    {

    }

    protected override Expression<Func<Student, bool>>? GetDuplicateCheckExpression(StudentDtos.Create dto)
    {
        var cleanName = dto.Name.ToStandardFormat();
        return s => s.Name == cleanName && s.GradeId == dto.GradeId;
    }

    protected override Expression<Func<Student, bool>>? GetDuplicateCheckExpression(StudentDtos.Update dto, int id)
    {
        var cleanName = dto.Name.ToStandardFormat();
        return s => s.Name == cleanName && s.GradeId == dto.GradeId && id != s.StudentId;
    }
}