using System.Linq.Expressions;
using AutoMapper;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Exceptions;
using EduCenterSys.Application.Interfaces;
using EduCenterSys.Domain.Entities;
using EduCenterSys.Domain.Enums;
using EduCenterSys.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduCenterSys.Application.Services;

public class GroupService : BaseService<Group, GroupDtos.Read, GroupDtos.Create, GroupDtos.Update>, IGroupService
{
    private readonly ITeacherService _teacherService;
    private readonly ISubjectService _subjectService;
    private readonly IGradeService _gradeService;
    private readonly IGenericRepository<Group> _genGroupRepo;

    public GroupService(
        IGenericRepository<Group> genericRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ITeacherService teacherService,
        IGradeService gradeService,
        ISubjectService subjectService)
        : base(genericRepository, unitOfWork, mapper)
    {
        _teacherService = teacherService;
        _subjectService = subjectService;
        _gradeService = gradeService;
        _genGroupRepo = genericRepository;
    }

    protected override async Task ValidateBeforeCreateAsync(GroupDtos.Create dto, CancellationToken ct = default)
    {
        await EnsureReferencedEntitiesExistAsync(dto.SubjectId, dto.GradeId, dto.TeacherId, ct);

        await EnsureTeacherIsQualifiedAsync(dto.TeacherId, dto.SubjectId, dto.GradeId, ct);

        await EnsureNoScheduleConflictsAsync(g => g.TeacherId == dto.TeacherId && g.Status == Status.Active, dto.Schedules, ct);
    }


    private async Task EnsureReferencedEntitiesExistAsync(int? subjectId, int? gradeId, int? teacherId, CancellationToken ct)
    {
        if (subjectId is not null && !await _subjectService.ExistsAsync(subjectId.Value, ct))
            throw new KeyNotFoundException("Subject not found");

        if (gradeId is not null && !await _gradeService.ExistsAsync(gradeId.Value, ct))
            throw new KeyNotFoundException("Grade not found");

        if (teacherId is not null && !await _teacherService.ExistsAsync(teacherId.Value, ct))
            throw new KeyNotFoundException("Teacher not found");
    }

    private async Task EnsureTeacherIsQualifiedAsync(int teacherId, int subjectId, int gradeId, CancellationToken ct)
    {
        if (!await _teacherService.IsQualified(teacherId, subjectId, gradeId, ct))
            throw new ConflictException("The teacher is not qualified for the subject or the grade level");
    }


    private async Task EnsureNoScheduleConflictsAsync(Expression<Func<Group, bool>> predicate, IEnumerable<IScheduleSlot> schedules, CancellationToken ct)
    {
        if (schedules is null || !schedules.Any())
            return;

        var existingSchedules = await _genGroupRepo.GetQueryable()
                .Where(predicate)
                .SelectMany(g => g.Schedules)
                .ToListAsync(ct);

        if (schedules.Any(newSchedule => existingSchedules.Any(
            ex => ex.Day == newSchedule.Day &&
            newSchedule.StartHour < ex.EndHour &&
            newSchedule.EndHour > ex.StartHour)))
            throw new ConflictException("There's a conflict Schedule");
    }

    protected async override Task ValidateBeforeUpdateAsync(int groupId, GroupDtos.Update dto, Group entity, CancellationToken ct = default)
    {
        await EnsureReferencedEntitiesExistAsync(dto.SubjectId, dto.GradeId, dto.TeacherId, ct);

        var teacherId = dto.TeacherId ?? entity.TeacherId;
        var subjectId = dto.SubjectId ?? entity.SubjectId;
        var gradeId = dto.GradeId ?? entity.GradeId;

        await EnsureTeacherIsQualifiedAsync(teacherId, subjectId, gradeId, ct);

        await EnsureNoScheduleConflictsAsync(g => g.TeacherId == dto.TeacherId && g.Status == Status.Active && g.GroupId != groupId, dto.Schedules, ct);
    }
}