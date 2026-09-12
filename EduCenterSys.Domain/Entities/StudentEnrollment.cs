using EduCenterSys.Domain.Enums;

namespace EduCenterSys.Domain.Entities;

public class StudentEnrollment(DateOnly enrollmentDate, DateOnly inactivateDate, EntityStatus status, int studentId, int groupId)
{
    public int StudentEnrollmentId { get; private set; }
    public DateOnly EnrollmentDate { get; private set; } = enrollmentDate;
    public DateOnly InactivateDate { get; private set; } = inactivateDate;
    public EntityStatus Status { get; private set; } = status;

    public int StudentId { get; private set; } = studentId;
    public int GroupId { get; private set; } = groupId;

    public Student Student { get; private set; } = default!;
    public Group Group { get; private set; } = default!;

}
