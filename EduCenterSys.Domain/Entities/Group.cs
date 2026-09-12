using EduCenterSys.Domain.Enums;

namespace EduCenterSys.Domain.Entities;

public class Group(byte capacity, int gradeId, int subjectId, EntityStatus status, int teacherId)
{
    public int GroupId { get; private set; }
    public byte Capacity { get; private set; } = capacity;
    public int GradeId { get; private set; } = gradeId;
    public int SubjectId { get; private set; } = subjectId;
    public int TeacherId { get; private set; } = teacherId;
    public EntityStatus Status { get; private set; } = status;

    public Subject Subject { get; private set; } = default!;
    public Grade Grade { get; private set; } = default!;
    public ICollection<StudentEnrollment> Enrollments { get; private set; } = [];
    public ICollection<GroupSchedule> Schedules { get; private set; } = [];
    public Teacher Teacher { get; private set; } = default!;
}
