using EduCenterSys.Domain.Enums;
using EduCenterSys.Domain.Interfaces;

namespace EduCenterSys.Domain.Entities;

public class Group : ISoftDelete
{
    private Group() { }
    public Group(byte capacity, int gradeId, int subjectId, Status status, int teacherId)
    {
        Capacity = capacity;
        GradeId = gradeId;
        SubjectId = subjectId;
        TeacherId = teacherId;
        Status = status;
    }
    public int GroupId { get; private set; }
    public byte Capacity { get; private set; }
    public int GradeId { get; private set; }
    public int SubjectId { get; private set; }
    public int TeacherId { get; private set; }
    public Status Status { get; private set; }

    public Subject Subject { get; private set; } = default!;
    public Grade Grade { get; private set; } = default!;
    public ICollection<StudentEnrollment> Enrollments { get; private set; } = [];
    public ICollection<GroupSchedule> Schedules { get; private set; } = [];
    public Teacher Teacher { get; private set; } = default!;
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }

    public void MarkAsDeleted()
    {
        IsDeleted = true;
        DeletedAt = DateTimeOffset.UtcNow;
    }
}
