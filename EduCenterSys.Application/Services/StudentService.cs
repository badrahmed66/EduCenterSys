using AutoMapper;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Interfaces;
using EduCenterSys.Domain.Entities;
using EduCenterSys.Domain.Interfaces;

namespace EduCenterSys.Application.Services;

public class StudentService : BaseService<Student, StudentDtos.Read, StudentDtos.Create, StudentDtos.Update>, IStudentService
{
    private readonly IGenericRepository<Student> _repository;
    private readonly IGenericRepository<Grade> _gradeRepo;
    public StudentService(IGenericRepository<Student> repository,
    IGenericRepository<Grade> gradeRepo,
    IUnitOfWork unitOfWork,
    IMapper mapper) : base(repository, unitOfWork, mapper)
    {
        _repository = repository;
        _gradeRepo = gradeRepo;
    }

    public override async Task<StudentDtos.Read> AddAsync(StudentDtos.Create dto, CancellationToken cancellationToken = default)
    {
        // insure student didn't register before.
        if (await IsStudentAlreadyRegistered(dto.Name, dto.GradeId, cancellationToken))
            throw new InvalidOperationException("Student already registered");

        // insure the user insert an exist grade
        if(! await IsGradeExists(dto.GradeId,cancellationToken))
            throw new KeyNotFoundException("Invalid Grade Id");

        return await base.AddAsync(dto, cancellationToken);
    }

    private async Task<bool> IsStudentAlreadyRegistered(string name, int gradeId, CancellationToken cancellationToken)
    {
        return await _repository
                    .IsExistsAsync(s => s.Name == name && s.GradeId == gradeId, cancellationToken);
    }

    private async Task<bool> IsGradeExists(int gradeId, CancellationToken ct)
    => await _gradeRepo
                .IsExistsAsync(g => g.GradeId == gradeId, ct);
}