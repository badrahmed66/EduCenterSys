
using AutoMapper;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Interfaces;
using EduCenterSys.Domain.Entities;
using EduCenterSys.Domain.Interfaces;

namespace EduCenterSys.Application.Services;

public class TeacherService : BaseService<Teacher, TeacherDtos.Read, TeacherDtos.Create, TeacherDtos.Update>, ITeacherService
{
    private readonly ITeacherQualificationService _qualification;
    private readonly IGenericRepository<Teacher> _genericRepository;
    //private readonly IMapper _mapper;
    public TeacherService(
        IGenericRepository<Teacher> genericRepository,
        IUnitOfWork unitOfWork, IMapper mapper,
        ITeacherQualificationService qualification) : base(genericRepository, unitOfWork, mapper)
    {
        _genericRepository = genericRepository;
        //_mapper = mapper;
        _qualification = qualification;
    }

    public async override Task<TeacherDtos.Read> AddAsync(TeacherDtos.Create dto, CancellationToken ct = default)
    {
        if (await IsRegistered(dto.NationalId, ct))
            throw new InvalidOperationException("Teacher has already registered");

        return await base.AddAsync(dto, ct);
    }

    private async Task<bool> IsRegistered(string nationalId, CancellationToken ct) => await _genericRepository
                    .IsExistsAsync(t => t.NationalId == nationalId, ct);

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

    public async Task<IReadOnlyList<TeacherQualificationDtos.Read>> GetAllQualificationAsync(int teacherId, int pageIndex = 1, int pageSize = 10, CancellationToken ct = default)
    {
        return await _qualification.GetAllAsync(teacherId, pageIndex, pageSize, ct);
    }
    public async Task DeleteQualificationAsync(int teacherId, int qualificationId, CancellationToken ct = default) => await _qualification.DeleteAsync(teacherId, qualificationId, ct);

    public async Task UpdateQualificationAsync(int teacherId, int qualificationId, TeacherQualificationDtos.Update dto,CancellationToken ct = default) => await _qualification.UpdateAsync(teacherId,qualificationId, dto, ct);

}