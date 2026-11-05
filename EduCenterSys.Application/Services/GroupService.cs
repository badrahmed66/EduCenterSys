using System.Linq.Expressions;
using AutoMapper;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Exceptions;
using EduCenterSys.Application.Interfaces;
using EduCenterSys.Domain.Validation;
using EduCenterSys.Domain.Entities;
using EduCenterSys.Domain.Enums;
using EduCenterSys.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace EduCenterSys.Application.Services;

public class GroupService : BaseService<Group, GroupDtos.Read, GroupDtos.Create, GroupDtos.Update>, IGroupService
{
    private readonly ITeacherService _teacherService;
    private readonly ISubjectService _subjectService;
    private readonly IGradeService _gradeService;
    private readonly IGenericRepository<Group> _genGroupRepo;
    private readonly IGroupRepository _groupRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public GroupService(
        IGenericRepository<Group> genericRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ITeacherService teacherService,
        IGradeService gradeService,
        ISubjectService subjectService,
        IGroupRepository groupRepository,
        ITeacherRepository teacherRepository)
        : base(genericRepository, unitOfWork, mapper)
    {
        _teacherService = teacherService;
        _subjectService = subjectService;
        _gradeService = gradeService;
        _genGroupRepo = genericRepository;
        _groupRepository = groupRepository;
        _teacherRepository = teacherRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
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

        await EnsureNoScheduleConflictsAsync(g => g.TeacherId == teacherId && g.Status == Status.Active && g.GroupId != groupId, dto.Schedules, ct);
    }

    #region Schedules Services part 
    public async Task<IReadOnlyList<GroupScheduleDtos.Read>> AddSchedules(int groupId, IEnumerable<IScheduleSlot> schedules, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(schedules);

        List<IScheduleSlot> newScheudles = schedules.ToList();

        if (newScheudles.Count == 0)
            throw new ArgumentException("At least one schedule is required", nameof(schedules));

        // check if the period valid or not
        if (!ScheduleValidator.IsValidPeriod(newScheudles))
            throw new ArgumentException("Schedule end time must be later thatn start time",nameof(schedules));

        // check on the new schedules input in memory
        if (ScheduleValidator.HasOverlap(newScheudles))
            throw new ConflictException("There's a conflict in your scheduls");

        // validate the new schedules with the saved for the same teacher

        var groupEntity = await _genGroupRepo.GetByIdAsync(groupId, ct) ?? throw new KeyNotFoundException("No group with this Id");

        var teacherSchedules = await _teacherRepository.GetSchedulesByTeacherId(groupEntity.TeacherId, ct);

        if (teacherSchedules.Count > 0)
        {
            List<IScheduleSlot> tempCollection = newScheudles.ToList();

            tempCollection.AddRange(teacherSchedules);

            if (ScheduleValidator.HasOverlap(tempCollection))
                throw new ConflictException("There's a conflict with an exist schedule");
        }

        // mapping the IScheduleSlot to GroupShcedule
        List<GroupSchedule> allSchedules = _mapper.Map<List<GroupSchedule>>(
            newScheudles, opts => opts.Items["GroupId"] = groupId);

        // add the new entity to the group's schedules as a new schedule in memory
        foreach (var schedule in allSchedules)
        {
            groupEntity.Schedules.Add(schedule);
        }

        // save the entity in the Db with the new state
        await _unitOfWork.SaveChangesAsync(ct);

        // return the whole schedules 
        List<GroupSchedule> oldSchedules = teacherSchedules.Where(t => t.GroupId == groupId).ToList();

        allSchedules.AddRange(oldSchedules);

        // mapping from List<GroupSchedules> to IReadOnlyList<GroupSchedulesDtos.Read> 
        return _mapper.Map<IReadOnlyList<GroupScheduleDtos.Read>>(allSchedules);
    }

    #endregion

}