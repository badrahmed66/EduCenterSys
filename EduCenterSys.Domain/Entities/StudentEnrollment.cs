using EduCenterSys.Domain.Enums;

namespace EduCenterSys.Domain.Entities;

public class StudentEnrollment
{
    private StudentEnrollment() { }
    public StudentEnrollment(DateOnly enrollmentDate, DateOnly inactivateDate, EntityStatus status, int studentId, int groupId)
    {
        EnrollmentDate = enrollmentDate;
        InactivateDate = inactivateDate;
        Status = status;
        StudentId = studentId;
        GroupId = groupId;
    }
    public int StudentEnrollmentId { get; private set; }
    public DateOnly EnrollmentDate { get; private set; }
    public DateOnly InactivateDate { get; private set; }
    public EntityStatus Status { get; private set; }

    public int StudentId { get; private set; }
    public int GroupId { get; private set; }

    public Student Student { get; private set; } = default!;
    public Group Group { get; private set; } = default!;

}
